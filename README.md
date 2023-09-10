# Fluxo De Caixa

<img src="./images/money.svg" width="250px"/>


## Documentação

### Diagramas - C4 Model

- [System Context](./Doc/SystemContex.md)
- [Container](./Doc/Container.md)

## Como Rodar o Projeto

Para rodar os projetos é necessário  ter Docker instalado, e rodar o seguinte comando no diretório principal: `docker-compose up`, e acessar o localhost na porta 80, onde você terá acesso ao web site: [http://localhost:80](http://localhost:80)

## Criando nova migração

Visual Studio: `Add-Migration {Nome} -Context FluxoDeCaixaDataContext -StartupProject FluxoDeCaixa.Api -Project FluxoDeCaixa.Infra -OutputDir Migrations`

Dotnet cli: `dotnet ef migrations add {Nome} --context FluxoDeCaixaDataContext --startup-project FluxoDeCaixa.Api --project FluxoDeCaixa.Infra --output-dir Migrations`