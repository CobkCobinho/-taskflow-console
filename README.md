# TaskFlow Console

Gerenciador de tarefas pelo terminal em **C# e .NET 10**, com validação de entrada, testes automatizados em **xUnit** e integração contínua no **GitHub Actions**.

## Funcionalidades

- Adicionar tarefas com título obrigatório; espaços nas pontas são removidos.
- Listar tarefas pendentes (`[ ]`) e concluídas (`[x]`) na ordem do cadastro.
- Concluir uma tarefa pelo número mostrado na lista.
- Tratar opções e números inválidos sem encerrar a aplicação.
- Encerrar normalmente com a opção `0` ou quando a entrada termina, inclusive durante o cadastro e a conclusão.

As tarefas ficam **em memória** e não são salvas ao sair. Títulos repetidos são permitidos: cada tarefa recebe seu próprio identificador. Concluir novamente uma tarefa mantém seu estado de concluída.

## Executar

Requisitos: **.NET SDK 10** e Git.

```sh
git clone https://github.com/CobkCobinho/-taskflow-console.git taskflow
cd taskflow
dotnet run --project TaskFlow.Console
```

Execute os comandos a partir da raiz do repositório. A solução e as pastas da aplicação e dos testes estão diretamente nessa raiz.

| Opção | Ação |
| --- | --- |
| `1` | Adicionar tarefa |
| `2` | Listar tarefas |
| `3` | Concluir tarefa |
| `0` | Sair |

Exemplo da listagem:

```text
1. [ ] Estudar C#
2. [x] Escrever testes
```

Ao concluir uma tarefa, informe o número exibido na lista, como `1`. O identificador interno é um `Guid` e não precisa ser digitado.

## Compilar e testar

```sh
dotnet restore TaskFlow.slnx
dotnet build TaskFlow.slnx -c Release --no-restore
dotnet test TaskFlow.slnx -c Release --no-build --no-restore
```

Os testes cobrem criação e validação, identidade das tarefas, consulta por identificador, conclusão apenas da tarefa escolhida, tarefas inexistentes, menu, números fora dos limites, fluxo completo e fim da entrada. O menu recebe leitores e escritores de texto para ser testado sem um terminal interativo.

O GitHub Actions compila com avisos tratados como erros, executa os testes em pushes e pull requests para `main` e guarda o resultado dos testes como artefato da execução.

## Estrutura

```text
├── TaskFlow.slnx
├── TaskFlow.Console/
│   ├── Program.cs                  # Inicialização
│   ├── TaskFlowApp.cs              # Menu e interação com o usuário
│   ├── Models/TaskItem.cs          # Identidade, título e estado da tarefa
│   └── Services/TaskManager.cs     # Cadastro, consulta e conclusão
├── TaskFlow.Tests/
│   ├── TaskManagerTests.cs
│   ├── TaskItemTests.cs
│   └── TaskFlowAppTests.cs
└── .github/workflows/ci.yml
```

`bin/`, `obj/` e resultados de testes são gerados localmente e ignorados pelo Git. A licença do projeto é [MIT](LICENSE).

## Histórico

As alterações são agrupadas por assunto em commits com descrições como `fix(console): ...`, `test(domain): ...` e `docs: ...`.

```sh
git log --oneline --graph
```
