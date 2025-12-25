# Fluxo de Caixa API

## Migrations

### Criando nova migração

Visual Studio:
```powershell
Add-Migration {Nome} -Context FluxoDeCaixaDataContext -StartupProject FluxoDeCaixa.Api -Project FluxoDeCaixa.Infra -OutputDir Migrations
```

Dotnet CLI:
```bash
dotnet ef migrations add {Nome} \
  --context FluxoDeCaixaDataContext \
  --startup-project FluxoDeCaixa.Api/FluxoDeCaixa.Api.csproj \
  --project FluxoDeCaixa.Infra/FluxoDeCaixa.Infra.csproj \
  --output-dir Migrations
```

### Aplicando migrations ao banco de dados

Visual Studio:
```powershell
Update-Database -Context FluxoDeCaixaDataContext -StartupProject FluxoDeCaixa.Api -Project FluxoDeCaixa.Infra
```

Dotnet CLI:
```bash
dotnet ef database update \
  --context FluxoDeCaixaDataContext \
  --startup-project FluxoDeCaixa.Api/FluxoDeCaixa.Api.csproj \
  --project FluxoDeCaixa.Infra/FluxoDeCaixa.Infra.csproj
```

### Removendo última migration (se ainda não aplicada)

Dotnet CLI:
```bash
dotnet ef migrations remove \
  --context FluxoDeCaixaDataContext \
  --startup-project FluxoDeCaixa.Api/FluxoDeCaixa.Api.csproj \
  --project FluxoDeCaixa.Infra/FluxoDeCaixa.Infra.csproj
```