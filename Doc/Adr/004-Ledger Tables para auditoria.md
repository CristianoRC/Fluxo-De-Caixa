# 004 - Ledger Tables para Auditoria de Transações

## Contexto

O sistema de fluxo de caixa lida com dados financeiros sensíveis, onde a integridade e rastreabilidade das transações são fundamentais. Transações financeiras, uma vez registradas, não devem ser alteradas ou removidas, garantindo conformidade com requisitos de auditoria e prevenindo fraudes. Era necessário implementar um mecanismo que garantisse a imutabilidade dos registros financeiros de forma nativa no banco de dados.

## Decisão

Implementar Ledger Tables do SQL Server 2022 nas tabelas `Transactions` e `BookEntries` utilizando o modo `APPEND_ONLY`. Essa abordagem garante que os dados dessas tabelas sejam imutáveis no nível do banco de dados, permitindo apenas operações de INSERT.

## Detalhes de Implementação

1. As tabelas `Transactions` e `BookEntries` foram criadas como Ledger Append-Only usando SQL customizado nas migrações do Entity Framework Core.

2. A sintaxe utilizada foi `WITH (LEDGER = ON (APPEND_ONLY = ON))` que:
   - Permite apenas operações de INSERT (bloqueando UPDATE e DELETE)
   - Cria automaticamente uma tabela de histórico para auditoria
   - Gera hashes criptográficos para verificação de integridade dos dados

3. Foreign Keys foram configuradas com `ON DELETE NO ACTION` em vez de `CASCADE`, pois Ledger Tables não suportam exclusão em cascata.

4. As configurações do Entity Framework Core foram ajustadas para usar `DeleteBehavior.NoAction` nas relações:
   - `TransactionConfiguration`: Relação com `Balance`
   - `BookEntryConfiguration`: Relações com `Entry` e `Offset`

5. A criação das tabelas foi feita via `migrationBuilder.Sql()` com SQL raw, pois o EF Core não suporta nativamente a sintaxe de Ledger Tables.

## Justificativa

1. Imutabilidade garantida: O SQL Server impede fisicamente alterações ou exclusões nos dados, oferecendo uma garantia mais forte que validações em código.

2. Auditoria automática: O SQL Server mantém automaticamente o histórico de todas as inserções, eliminando a necessidade de implementar triggers ou tabelas de auditoria manualmente.

3. Verificação de integridade: Os hashes criptográficos permitem detectar qualquer tentativa de adulteração dos dados, mesmo por DBAs.

4. Conformidade regulatória: Atende requisitos de auditoria financeira que exigem rastreabilidade completa de transações.

5. Feature nativa do SQL Server 2022: Aproveita funcionalidades avançadas do banco de dados sem necessidade de ferramentas externas.

## Irreversibilidade da Configuração

**IMPORTANTE**: Uma vez que uma tabela é criada como Ledger Table, ela **NÃO pode ser revertida** para uma tabela normal, mesmo por administradores do banco de dados (DBAs), administradores de sistema ou administradores de nuvem.

Segundo a documentação oficial da Microsoft:

> *"After a table is created as a ledger table, it can't be reverted to a table that doesn't have ledger functionality. As a result, an attacker can't temporarily remove ledger capabilities, make changes to the table, and then reenable ledger functionality."*

Isso significa que:

1. **Não existe** comando `ALTER TABLE ... SET (LEDGER = OFF)` para desativar o Ledger.
2. **Nenhum nível de privilégio** permite desativar a proteção Ledger em uma tabela existente.
3. A única forma de ter os dados sem proteção Ledger é **criar um novo banco de dados** e migrar os dados para tabelas regulares.

Esta é uma **decisão permanente** e deve ser considerada cuidadosamente antes da implementação em produção.

## Consequências

### Positivas

1. Dados financeiros são verdadeiramente imutáveis no nível do banco de dados.
2. Auditoria completa e automática de todas as transações inseridas.
3. Detecção de adulteração através de verificação criptográfica.
4. Conformidade com requisitos regulatórios de auditoria financeira.
5. Redução de código customizado para auditoria e histórico.
6. Maior confiança e transparência nos dados financeiros.
7. Proteção contra ataques internos - nem DBAs conseguem desabilitar a proteção para adulterar dados.

### Negativas

1. Requer SQL Server 2022 ou superior (Azure SQL Database também suporta).
2. Impossibilidade de corrigir erros diretamente na tabela - correções devem ser feitas via transações compensatórias.
3. Crescimento contínuo das tabelas de histórico, exigindo planejamento de armazenamento.
4. Necessidade de usar SQL customizado nas migrações, pois o EF Core não suporta nativamente.
5. Restrição no uso de `CASCADE` em Foreign Keys, exigindo uso de `NO ACTION`.
6. Complexidade adicional para cenários de desenvolvimento/testes onde reset de dados é necessário.
7. **Decisão irreversível** - não é possível desativar o Ledger após a criação da tabela.

## Observações

1. Para ambientes de desenvolvimento e testes onde é necessário limpar dados, a única opção é **recriar o banco de dados** ou usar um banco separado sem Ledger Tables. Considerar manter scripts de seed para facilitar a recriação.

2. Monitorar o crescimento das tabelas de histórico (`*_History`) e planejar estratégias de arquivamento se necessário.

3. Implementar transações compensatórias (estorno) em vez de tentar corrigir ou excluir registros incorretos.

4. Documentar procedimentos de auditoria que aproveitem os recursos de verificação de integridade do Ledger.

5. Considerar usar Ledger Tables também para a tabela `Balances` no futuro, se houver requisitos de auditoria para alterações de saldo.

6. Em caso de migração para outro SGBD, será necessário implementar mecanismos alternativos de auditoria e imutabilidade.

7. Antes de aplicar em produção, garantir que todos os stakeholders entendam a natureza **permanente** e **irreversível** desta decisão.
