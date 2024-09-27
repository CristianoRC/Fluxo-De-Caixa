# 001 - Cadastro de transações em par (Book Entry)

## Contexto

O sistema precisa cadastrar transações em pares, conhecidas como Book Entry. Isso envolve cálculos complexos, como o valor do saldo após a transação, que são utilizados em relatórios e interfaces do usuário. É necessário garantir a integridade dos dados, considerando a concorrência e a consistência das operações.

## Decisão

Implementar um mecanismo de bloqueio (lock) nos dois saldos envolvidos na transação durante todo o fluxo de cálculo e registro.

## Detalhes de Implementação

1. Ao iniciar uma transação, o sistema adquirirá um lock exclusivo nos saldos de origem e destino.
2. O lock será mantido durante todo o processo de cálculo e registro da transação.
3. Após a conclusão bem-sucedida da transação, os locks serão liberados.
4. Em caso de falha, os locks serão liberados e a transação será revertida.

## Justificação

1. Consistência de dados: O bloqueio garante que nenhuma outra operação interfira nos saldos durante o processamento da transação.
2. Simplicidade: Esta abordagem simplifica a lógica de negócios e reduz a complexidade do código.
3. Manutenibilidade: Facilita a depuração e os testes, pois o estado do sistema é mais previsível durante as transações.
4. Integridade transacional: Garante que as operações de débito e crédito sejam atômicas e consistentes.

## Consequências

### Positivas

1. Maior integridade dos dados financeiros.
2. Redução de condições de corrida e inconsistências de saldo.
3. Simplificação da lógica de negócios e do código de manipulação de transações.
4. Facilidade na implementação de auditorias e rastreamento de transações.

### Negativas

1. Potencial gargalo de desempenho em cenários de alta concorrência.
2. Risco de deadlock se não for implementado corretamente.
3. Possível aumento no tempo de resposta para transações individuais.
4. Limitação na escalabilidade horizontal do sistema.

## Observações

1. Implementar monitoramento de desempenho para identificar possíveis gargalos.
2. Estabelecer um limite de tempo para a conclusão das transações para evitar locks indefinidos.
3. Implementar uma lógica de retry para transações que falham devido a timeouts ou conflitos de lock.
4. Considerar a implementação de um sistema de filas para processar transações em lote durante períodos de pico de carga.