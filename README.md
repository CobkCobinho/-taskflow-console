# TaskFlow Console

Gerenciador de tarefas pelo terminal, desenvolvido em **C# e .NET 10**, com testes unitários em **xUnit**.

## Funcionalidades

- Adicionar tarefas.
- Listar tarefas pendentes e concluídas.
- Marcar uma tarefa como concluída.

As tarefas ficam em memória durante a execução; ao encerrar o programa, a lista não é salva.

## Como executar

Com o **.NET SDK 10** e o Git instalados:

```sh
git clone https://github.com/CobkCobinho/-taskflow-console.git taskflow
cd taskflow
dotnet run --project taskflow-console/TaskFlow.Console
```

## Testes

A partir da raiz do repositório clonado:

```sh
dotnet test taskflow-console/TaskFlow.Tests/TaskFlow.Tests.csproj
```

Veja a [documentação completa](taskflow-console/README.md) para consultar a estrutura, o menu e os comandos de compilação.
