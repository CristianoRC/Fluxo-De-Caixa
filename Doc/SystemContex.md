# System Context

```mermaid
C4Context
    title System Context Diagram - Fluxo de Caixa

    Enterprise_Boundary(boundary-fluxo-de-caixa, "Fluxo de caixa") {
      System(fluxo-de-caixa, "Fluxo de Caixa", "Sistema para gerenciamento de fluxo de caixa")
    }

    System_Ext(blob-storage, "Blob storage Sevice", "Sistema que oferece armazenamento de dados")
    System_Ext(mail-system, "Serviço de E-mail", "Sistema de envio de e-mail")
    Person(pessoa, "Usuário do sistema", "Gerencia fluxo de caixa")

    Rel(pessoa, fluxo-de-caixa, "Usa o sistema")
    Rel(fluxo-de-caixa, blob-storage, "Gerencia arquivos no")
    Rel(fluxo-de-caixa, mail-system, "Envia e-mail usando")
    Rel(mail-system, pessoa, "Envia e-mail para")
```