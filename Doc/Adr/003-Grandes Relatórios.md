## Relatórios grandes ou em grandes quantidades

### Context

Em algumas situações podemos ter uma grande quantidade de relatórios ao mesmo tempo, como no final do dia, ou ter relatórios com muitas páginas para serem gerados.

### Decision

Por motivos de tempo e prioridade não foi desenvolvido um sistema de envio de relatório de for assíncrona, mas o fluxo está desenhado nos diagramas, seria necessário monitorar o uso do sistema no dia a dia para enter se a feature se torna muito necessária, e qual sua prioridade

### Consequences

Possíveis gargalos no sistema, tendo que escalar horizontalmente os serviços e o banco de dados para conseguir atender as requisições.
