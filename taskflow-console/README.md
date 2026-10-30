# TaskFlow Console

Gerenciador de tarefas (To-Do List) via console, feito em **.NET 10** com testes unitários em **xUnit**.

## Estrutura

```
taskflow-console/
├── TaskFlow.slnx
├── TaskFlow.Console/   # Aplicação console (TaskItem, TaskManager, Program)
└── TaskFlow.Tests/     # Testes unitários (xUnit)
```

## Pré-requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download)
- Git

## Clonar o repositório

```bash
git clone https://github.com/CobkCobinho/-taskflow-console.git taskflow
cd taskflow/taskflow-console
```

## Compilar

```bash
dotnet build
```

## Rodar a aplicação

```bash
dotnet run --project TaskFlow.Console
```

Menu disponível:

| Opção | Ação             |
|-------|------------------|
| 1     | Adicionar tarefa |
| 2     | Listar tarefas   |
| 3     | Concluir tarefa  |
| 0     | Sair             |

## Executar os testes unitários

```bash
dotnet test
```

## Padrão de commits

O projeto segue o padrão [Conventional Commits](https://www.conventionalcommits.org/):
`tipo(escopo): descrição curta` (ex.: `feat(domain): ...`, `fix(manager): ...`).

Para ver o histórico:

```bash
git log --oneline --graph
```
