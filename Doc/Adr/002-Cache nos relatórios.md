# 002 - Cache nos relatórios

## Contexto
Os relatórios gerados pelo sistema não sofrem alterações frequentes, especialmente aqueles referentes a dias passados. Atualmente, não há implementação de cache, o que resulta em processamento desnecessário e custos elevados para a geração repetitiva de relatórios idênticos.

## Decisão
Devido a restrições de tempo e prioridades do projeto, decidiu-se não implementar um sistema de cache para os relatórios neste momento. No entanto, reconhece-se a necessidade de implementar essa funcionalidade no futuro para otimizar o desempenho e reduzir custos.

## Detalhes de Implementação
Embora a implementação não seja realizada no momento, a estratégia futura de cache deve considerar:

1. Cache de relatórios de dias passados:
   - Armazenar relatórios completos em um sistema de cache distribuído (ex: Redis).
   - Definir uma política de expiração longa para esses relatórios (ex: 30 dias).

2. Cache parcial para relatórios do dia atual:
   - Implementar um sistema de cache com invalidação seletiva.
   - Atualizar o cache em intervalos regulares (ex: a cada hora) ou quando ocorrerem mudanças significativas nos dados.

3. Mecanismo de invalidação de cache:
   - Desenvolver um sistema que permita a invalidação manual do cache quando necessário.
   - Implementar gatilhos automáticos para invalidar o cache em casos de atualizações críticas nos dados.

## Justificativa
A decisão de adiar a implementação do cache é baseada nas seguintes considerações:

1. Prioridades do projeto: Outras funcionalidades foram consideradas mais críticas no momento.
2. Restrições de tempo: O prazo atual não permite a implementação adequada de um sistema de cache robusto.
3. Complexidade: Um sistema de cache eficiente requer um design cuidadoso para evitar problemas de consistência de dados.

## Consequências

### Positivas
1. Foco em outras prioridades do projeto no curto prazo.
2. Evita-se a implementação apressada de um sistema de cache que poderia introduzir bugs ou inconsistências.

### Negativas
1. Maior consumo de recursos computacionais para geração repetitiva de relatórios.
2. Custos operacionais mais elevados devido ao processamento desnecessário.
3. Tempo de resposta potencialmente mais lento para os usuários ao acessar relatórios.

## Observações
1. É crucial revisar esta decisão em um futuro próximo, idealmente dentro de 3 a 6 meses.
2. Ao implementar o cache, considerar:
   - Uso de tecnologias como Redis ou Memcached para armazenamento distribuído.
   - Implementação de métricas para monitorar a eficácia do cache.
   - Testes de carga para garantir que o sistema de cache suporte picos de demanda.
3. Avaliar o impacto no consumo de recursos e custos operacionais regularmente para justificar a implementação do cache quando for viável.