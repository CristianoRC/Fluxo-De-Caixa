## Cache nos relatórios

### Context

Os relatórios não mudam toda hora, principalmente os dos dias que já passaram, uma solução para reduzir custo é adicionar cache.

### Decision

Não foi adicionado cache no fluxo por prioridade e tempo disponível
### Consequences

Teremos mais gastos com processamento, mas o ideal no futuro é criar um cache nem que seja para os dias que já passaram, e com o tempo fazer alguma estratégia para cache e invalidação dos relatórios do dia atual.