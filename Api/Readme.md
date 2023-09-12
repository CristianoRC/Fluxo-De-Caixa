# Fluxo de Caixa API

## Criando nova migração

Visual Studio: `Add-Migration {Nome} -Context FluxoDeCaixaDataContext -StartupProject FluxoDeCaixa.Api -Project FluxoDeCaixa.Infra -OutputDir Migrations`

Dotnet cli: `dotnet ef migrations add {Nome} --context FluxoDeCaixaDataContext --startup-project FluxoDeCaixa.Api --project FluxoDeCaixa.Infra --output-dir Migrations`