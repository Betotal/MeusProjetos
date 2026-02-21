---
description: Como resetar (apagar e recriar) o banco de dados do projeto
---

Para limpar todos os dados e recriar o banco do zero (útil para rodar o `DbInitializer` novamente):

1. **Parar a aplicação** (se estiver rodando).
2. Abrir o terminal na raiz do projeto (`d:\Curso\MeusProjetos\Estoque`).
3. Executar o comando:

```powershell
dotnet ef database drop --project src/Shared/ERPModular.Shared.Infrastructure --startup-project src/Presentation/ERPModular.Web --force
```

4. Rodar o projeto novamente:
```powershell
dotnet run --project src/Presentation/ERPModular.Web/ERPModular.Web.csproj
```

O sistema aplicará as migrações e o `SeedData` automaticamente ao iniciar.
