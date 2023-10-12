# Fluxo De Caixa

<img src="./images/money.svg" width="250px"/>

![workflow-api](https://github.com/CristianoRC/Fluxo-De-Caixa/actions/workflows/docker-image-api.yaml/badge.svg)
![workflow-api-report](https://github.com/CristianoRC/Fluxo-De-Caixa/actions/workflows/docker-image-api-report.yaml/badge.svg)
![workflow-app](https://github.com/CristianoRC/Fluxo-De-Caixa/actions/workflows/docker-image-app.yaml/badge.svg)

# Documentação

Sistema criado para estudos, com base em um sistema de Fluxo de Caixa, onde é possível criar carteiras, fazer transações entre elas e gerar relatórios de fechamento diário.
Separei em um outro arquivo, uma documentação de como usar a aplicação, para mais detalhes acesse a [documentação de uso](./Doc/ComoUsar.md)

## Diagramas - C4 Model

- [System Context](./Doc/SystemContex.md)
- [Container](./Doc/Container.md)
- [Component](./Doc/Component.md)
- [Code](./Doc/Code.md)


## Cloud Diagram

- [Azure](./Doc/Cloud.md)

## ADR - Architectural Decision Records

Registros das decisões tomadas em relação à arquitetura

- [ADRs](./Doc/Adr.md)

# Como Rodar o Projeto

## Containers

Para rodar os projetos é necessário  ter Docker instalado, e rodar o seguinte comando no diretório principal: `docker-compose up`, e acessar o localhost na porta 80, onde você terá acesso ao web site: [http://localhost:80](http://localhost:80)

## Localmente

### Serviços necessários

- PostgreSQL, na porta 5432
- MongoDB, na porta 27017
- Gotenberg, na porta 3000
- Redis, na porta 6379

Ajustar usuários e senhas de acordo com os valores do local.settings.json(report api) e appsettings.Development.json(api). 

### APP - React
Para configurar o projeto de web app é necessário ter node instalado, e rodar os seguintes comandos no diretório principal: `npm i` `npm run start`

### API
Necessário ter o .NET 7 instalado. Restaure os pacotes e rode o seguinte comando dentro do diretório _Api_: `dotnet run`

### API Report

Necessário ter o .NET 7 instalado e as [ferramentas para Azure functions](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=macos%2Cisolated-process%2Cnode-v4%2Cpython-v2%2Chttp-trigger%2Ccontainer-apps&pivots=programming-language-csharp).
Para executar a function, restaure os pacites rode o seguite comando no diretório _Api.Report/FluxoDeCaixa.Api.Report_ : `func start`

# CI/CD

Foi feita a configuração de GitHub Actions para geração e upload das imagens docker do APP e da API para o DockerHub sempre que uma release for criada, a tag desta release será usada como tag na imagem do container.

- [API Docker Image](https://hub.docker.com/r/cristianorc/fluxo-de-caixa-api)
- [API - Report Docker Image](https://hub.docker.com/r/cristianorc/fluxo-de-caixa-api-report)
- [APP Docker Image](https://hub.docker.com/r/cristianorc/fluxo-de-caixa-app)