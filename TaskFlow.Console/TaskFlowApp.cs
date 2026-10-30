public sealed class TaskFlowApp
{
    private readonly TextReader _input;
    private readonly TextWriter _output;
    private readonly TaskManager _manager = new();

    public TaskFlowApp(TextReader input, TextWriter output)
    {
        ArgumentNullException.ThrowIfNull(input);
        ArgumentNullException.ThrowIfNull(output);
        _input = input;
        _output = output;
    }

    public void Run()
    {
        while (true)
        {
            ShowMenu();
            var option = _input.ReadLine()?.Trim();
            if (option is null) return;

            _output.WriteLine();
            switch (option)
            {
                case "1":
                    if (!AddTask()) return;
                    break;
                case "2":
                    ListTasks();
                    break;
                case "3":
                    if (!CompleteTask()) return;
                    break;
                case "0":
                    _output.WriteLine("Até logo!");
                    return;
                default:
                    _output.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    private void ShowMenu()
    {
        _output.WriteLine();
        _output.WriteLine("=== TaskFlow ===");
        _output.WriteLine("1 - Adicionar tarefa");
        _output.WriteLine("2 - Listar tarefas");
        _output.WriteLine("3 - Concluir tarefa");
        _output.WriteLine("0 - Sair");
        _output.Write("Escolha uma opção: ");
    }

    private bool AddTask()
    {
        _output.Write("Título da tarefa: ");
        var title = _input.ReadLine();
        if (title is null) return false;

        try
        {
            var task = _manager.AddTask(title);
            _output.WriteLine($"Tarefa adicionada: \"{task.Title}\"");
        }
        catch (ArgumentException ex)
        {
            _output.WriteLine($"Erro: {ex.Message}");
        }
        return true;
    }

    private void ListTasks()
    {
        if (_manager.Tasks.Count == 0)
        {
            _output.WriteLine("Nenhuma tarefa cadastrada.");
            return;
        }
        var number = 1;
        foreach (var task in _manager.Tasks)
        {
            var status = task.IsCompleted ? "[x]" : "[ ]";
            _output.WriteLine($"{number++}. {status} {task.Title}");
        }
    }

    private bool CompleteTask()
    {
        if (_manager.Tasks.Count == 0)
        {
            _output.WriteLine("Nenhuma tarefa cadastrada.");
            return true;
        }
        ListTasks();
        _output.Write("Número da tarefa a concluir: ");
        var input = _input.ReadLine();
        if (input is null) return false;

        if (!int.TryParse(input, out var number) || number < 1 || number > _manager.Tasks.Count)
        {
            _output.WriteLine("Número inválido.");
            return true;
        }
        var task = _manager.Tasks.ElementAt(number - 1);
        _manager.CompleteTask(task.Id);
        _output.WriteLine($"Tarefa concluída: \"{task.Title}\"");
        return true;
    }
}
