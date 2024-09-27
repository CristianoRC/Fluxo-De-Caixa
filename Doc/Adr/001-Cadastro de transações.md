## Cadastro de transações

### Context

Precisamos cadastrar as transações em par(Book Entry), e para isso precisamos fazer alguns cálculos de dados como o valor do balance após a transação, valor usado em relatórios e telas, só que para isso funcionar precisamos fazer o cálculo no banco, ou na hora de busca, ou fazendo lock dos dois balances do Book Entry.

### Decision

Foi feito o lock dos dois balances até fazer todo o fluxo e os cálculos, para facilitar a manutenção e os testes

### Consequences

Podemos ter problemas na criação das transações, pois se tiver uma inserção em massa no mesmo balance, as requisições podem começar a dar timeout, se isso acontecer precisaremos criar um novo fluxo, onde na entrada apenas validamos se os balances existem e adicionamos em uma fila para a criação das transações no futuro.
