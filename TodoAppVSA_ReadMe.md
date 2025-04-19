# TodoAppVSA - API de Lista de Tarefas (Implementação Básica)

Esta é uma aplicação **básica** de exemplo de API para uma lista de tarefas (To-Do List), construída com ASP.NET Core. Seu objetivo principal é demonstrar uma arquitetura limpa com foco em Vertical Slices, utilizando o padrão CQRS (Command Query Responsibility Segregation) com a biblioteca MediatR e o Entity Framework Core com um banco de dados em memória. **A implementação é rudimentar e focada nos conceitos arquiteturais, não em ser uma aplicação completa.**

## Tecnologias Utilizadas

*   **.NET 9.0**
*   **ASP.NET Core**
*   **C# 13.0**
*   **Entity Framework Core** com provedor **In-Memory Database**
*   **MediatR** (para implementação do padrão Mediator e CQRS)
*   **OpenAPI** (para descrição da API)
*   **Scalar** (para visualização e teste da API em ambiente de desenvolvimento)

## Arquitetura

O projeto adota uma abordagem de **Vertical Slice Architecture (VSA)** combinada com **CQRS**:

*   **Vertical Slices:** Em vez de organizar o código por camadas técnicas (Controllers, Services, Repositories), a estrutura é organizada por funcionalidade ou *feature* (ex: `Features/Tasks`). Cada *slice* vertical contém toda a lógica necessária para uma funcionalidade específica (Commands, Queries, Handlers, Models, etc.), promovendo alta coesão e baixo acoplamento entre as funcionalidades.
*   **CQRS (Command Query Responsibility Segregation):** A separação clara entre operações que alteram o estado (Commands) e operações que leem o estado (Queries) é implementada usando a biblioteca MediatR.
    *   **Commands:** Representam a intenção de mudar o estado (ex: `CreateTaskCommand`). São processados por `IRequestHandler<TCommand, TResponse>`.
    *   **Queries:** Representam a intenção de buscar dados sem efeitos colaterais (ex: `GetTasksQuery`). São processadas por `IRequestHandler<TQuery, TResponse>`.
*   **MediatR:** Atua como um mediador no processo, desacoplando quem envia a solicitação (ex: Controller) de quem a processa (Handler). O Controller envia um Command ou Query para o MediatR, que localiza e executa o Handler correspondente.

## Funcionalidades (Básicas)

O conjunto atual de funcionalidades é **mínimo**, servindo principalmente para ilustrar o fluxo de CQRS e a estrutura VSA:

*   **Criação de Tarefas:** Permite adicionar novas tarefas com título e descrição (`CreateTaskCommand`).
*   **Listagem de Tarefas:** Permite obter a lista de todas as tarefas existentes (`GetTasksQuery`).
*   *(Funcionalidades mais avançadas como atualização, exclusão, marcação de status, tratamento de erros robusto, validação, etc., não estão implementadas nesta versão básica).*

## Estrutura do Projeto

O projeto segue a organização por *Vertical Slices*:

*   **`Features/`**: Contém as pastas para cada funcionalidade principal (slice).
    *   **`Tasks/`**: Agrupa tudo relacionado à funcionalidade de Tarefas.
        *   **`Commands/`**: Contém os Commands e seus Handlers (ex: `CreateTaskCommand`, `CreateTaskHandler`).
        *   **`Queries/`**: Contém as Queries e seus Handlers (ex: `GetTasksQuery`, `GetTaskHandler`).
        *   `TaskItem.cs`: O modelo de domínio/entidade para Tarefa.
*   **`Infrastructure/`**: Contém classes de infraestrutura, como o `DbContext`.
    *   `AppDbContext.cs`: Configuração do EF Core, usando o provedor **In-Memory**. O `DbSet<TaskItem>` mapeia a entidade para a "tabela" em memória.
*   **`Controllers/`**: Controladores da API que recebem requisições HTTP e enviam Commands/Queries via MediatR.
    *   `TaskController.cs`: Endpoints para as funcionalidades de Tarefas.
*   **`Program.cs`**: Configuração da aplicação:
    *   Registro de serviços (Controllers, MediatR, EF Core In-Memory).
    *   Configuração do pipeline HTTP (roteamento, autorização, OpenAPI, Scalar).
*   **`Tests/`** (Implícito pelo `TaskTests.cs`): Projeto(s) de teste, utilizando também o EF Core In-Memory para testes de unidade/integração dos Handlers.

## Como Executar o Projeto

1.  **Pré-requisitos:**
    *   [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) ou superior instalado.
    *   Um IDE de sua preferência (Visual Studio, VS Code, JetBrains Rider).
    *   Git (opcional, para clonar).

2.  **Clonar o Repositório (se aplicável):**
    ```bash
    git clone <url-do-seu-repositorio>
    cd TodoAppVSA
    ```

3.  **Restaurar Dependências:**
    ```bash
    dotnet restore
    ```

4.  **Banco de Dados (Em Memória):**
    *   A aplicação está configurada para usar o banco de dados em memória do EF Core (`UseInMemoryDatabase("TodoAppVSA")`).
    *   **Nenhuma configuração adicional de banco de dados é necessária.** Os dados existirão apenas enquanto a aplicação estiver em execução e serão perdidos ao reiniciar. Isso é ideal para desenvolvimento rápido e demonstração da arquitetura.

5.  **Executar a Aplicação:**
    ```bash
    dotnet run
    ```
    (Ou execute diretamente pelo seu IDE).

6.  **Acessar a API:**
    *   Após a execução, a API estará disponível (por padrão, em `https://localhost:PORTA` ou `http://localhost:PORTA`).
    *   Em ambiente de desenvolvimento, acesse a interface **Scalar** na URL `http://localhost:<porta>/scalar/v1` (ou `https://localhost:<porta>/scalar/v1`) para visualizar a documentação OpenAPI e testar os endpoints disponíveis. *(Nota: `<porta>` deve ser substituído pela porta real em que a aplicação está rodando)*.

## Endpoints da API (via Scalar)

*   **`GET /api/Task`**
    *   Descrição: Retorna a lista de todas as tarefas armazenadas em memória.
    *   Respostas:
        *   `200 OK`: Retorna um array de objetos `TaskItem`.
*   **`POST /api/Task`**
    *   Descrição: Cria uma nova tarefa na memória.
    *   Corpo da Requisição (`application/json`):
        ```json
        {
          "title": "Título da Tarefa",
          "description": "Descrição da Tarefa."
        }
        ```
    *   Respostas:
        *   `200 OK`: Retorna o ID (`Guid`) da tarefa recém-criada.

---