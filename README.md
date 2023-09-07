# Fluxo De Caixa

<img src="./images/money.svg" width="250px"/>


## Documentação

### Diagramas - C4 Model

- [System Context](./Doc/SystemContex.md)
- [Container](./Doc/Container.md)

## Como Rodar o Projeto



## Criando nova migração

Visual Studio: `Add-Migration {Nome} -Context FluxoDeCaixaDataContext -StartupProject FluxoDeCaixa.Api -Project FluxoDeCaixa.Infra -OutputDir Migrations`

Dotnet cli: `dotnet ef migrations add {Nome} --context FluxoDeCaixaDataContext --startup-project FluxoDeCaixa.Api --project FluxoDeCaixa.Infra --output-dir Migrations`