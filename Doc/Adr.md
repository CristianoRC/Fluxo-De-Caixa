# ADR - Architectural Decision Records

## Cadastro de transações

### Context

Precisamos cadastrar as transações em par(Book Entry), e para isso precisamos fazer alguns cálculos de dados como o valor do balance após a transação, valor usado em relatórios e telas, só que para isso funcionar precisamos fazer o cálculo no banco, ou na hora de busca, ou fazendo lock dos dois balances do Book Entry.

### Decision

Foi feito o lock dos dois balances até fazer todo o fluxo e os cálculos, para facilitar a manutenção e os testes

### Consequences

Podemos ter problemas na criação das transações, pois se tiver uma inserção em messa no mesmo balance, as requisições podem começar a dar timeout, se isso acontecer precisaremos criar um novo fluxo, onde na entrada apenas validamos se os balances existem e adicionamos em uma fila para a criação das transações no futuro.

---

## Cache nos relatórios

### Context

Os relatórios não mudam toda hora, principalmente os dos dias que já passaram, uma solução para reduzir custo é adicionar cache.

### Decision

Não foi adicionado cache no fluxo por prioridade e tempo disponível
### Consequences

Teremos mais gastos com processamento, mas o ideal no futuro é criar um cache nem que seja para os dias que já passaram, e com o tempo fazer alguma estratégia para cache e invalidação dos relatórios do dia atual.

---


## Relatórios grandes ou em grandes quantidades

### Context

Em algumas situações podemos ter uma grande quantidade de relatórios ao mesmo tempo, como no final do dia, ou ter relatórios com muitas páginas para serem gerados.

### Decision

Por motivos de tempo e prioridade não foi desenvolvido um sistema de envio de relatório de for assíncrona, mas o fluxo está desenhado nos diagramas, seria necessário monitorar o uso do sistema no dia a dia para enter se a feature se torna muito necessária, e qual sua prioridade

### Consequences

Possíveis gargalos no sistema, tendo que escalar horizontalmente os serviços e o banco de dados para conseguir atender as requisições.
