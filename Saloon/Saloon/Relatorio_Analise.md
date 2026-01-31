# Análise do Projeto Saloon

## Visão Geral para o Desenvolvedor Júnior

O projeto **Saloon** segue, em grande parte, uma estrutura moderna conhecida como **Onion Architecture** (ou Arquitetura Cebola/Limpa). O objetivo dessa estrutura é separar as responsabilidades, para que o código de "Negócio" (regras do sistema) não fique misturado com o código de "Tela" (Console ou Web) ou de "Banco de Dados".

### Estrutura Atual:
1.  **Saloon.Domain** (O Coração):
    *   Aqui vivem as **Entidades** (exemplos: Cliente, Usuario) e as **Interfaces** (contratos do que o sistema precisa salvar/ler, ex: `IClienteRepo`).
    *   *Bom:* Não depende de ninguém. É puro C#.
2.  **Saloon.Application** (A Lógica):
    *   Contém os **Services** (ex: `ClienteServ`) que orquestram o que acontece. Ex: "Para gravar um cliente, verifique se já existe, depois salve".
    *   *Atenção:* Atualmente está criando dependências manualmente (`new UsuarioRepo()`), o que precisa ser ajustado.
3.  **Saloon.Infrastructure** (O Trabalho Sujo):
    *   Implementa o acesso ao banco de dados real (SQL Server LocalDB) usando as interfaces do Domain.
4.  **Saloon.ConsoleApp** (A Cara do Sistema):
    *   É a aplicação que o usuário vê. Atualmente é um Console, mas queremos que possa ser Web também.

## Pontos de Atenção e Melhoria (Flexibilidade)

Para que este projeto funcione tanto em **Console** quanto em **Web** sem duplicar código, precisamos resolver alguns pontos de "Acoplamento" (quando uma parte do código sabe demais sobre a outra).

### 1. Injeção de Dependência (DI) Incompleta
No arquivo `ClienteServ.cs`, vemos isto:
```csharp
var usuarioRepo = new UsuarioRepo(); // ERRADO para arquitetura limpa
```
Isso "amarra" o seu Serviço a uma implementação específica de banco. Se quisermos mudar o banco ou testar, teremos problemas.
*   **Solução:** O `ClienteServ` deve pedir todas as suas dependências no construtor (como já faz com `IClienteRepo`, mas precisa fazer para todos).

### 2. Camada de Aplicação Dependendo da Infraestrutura
O projeto `Saloon.Application` tem uma referência para `Saloon.Infrastructure`.
*   **Ideal:** A Application só deve conhecer as **Interfaces** (do Domain). Quem decide qual "Infrastructure" usar é o projeto principal (Console ou Web).
*   **Ação:** Remover a referência de Infraestrutura da Application e usar apenas Interfaces.

### 3. Interface de Usuário (Console) com Lógica Estática
Os menus (ex: `Cliente.MenuCliente()`) são métodos `static`. Isso dificulta injetar os Serviços neles.
*   **Solução:** Transformar os Menus em classes comuns que recebem os Serviços no construtor. O "Programa Principal" cria o Menu e passa o Serviço.

## Caminho para a Solução (Roteiro)

1.  **Ajustar os Serviços (Application):**
    *   Garantir que eles recebam **tudo** via construtor (Interfaces).
    *   Remover "new Repo()" de dentro dos métodos.
2.  **Configurar Injeção de Dependência:**
    *   No `Program.cs` (Console) ou `Program.cs` (Web), configurar o container de DI para ligar `IClienteRepo` -> `ClienteRepo`.
3.  **Criar o Projeto Web (Futuro):**
    *   Como a regra de negócio está isolada na `Application`, basta criar um projeto API ou MVC que chame os mesmos serviços que o Console chama.

## Estado Atual das Funcionalidades
*   **Menu Principal:** Existe, mas incompleto.
*   **Sub-Menus:** Faltam implementações.
*   **Banco de Dados:** Configurado para MSSQLLocalDB.

Esta estrutura está no caminho certo! Com pequenos ajustes de arquitetura (focados em Injeção de Dependência), ela ficará profissional e pronta para múltiplas plataformas.
