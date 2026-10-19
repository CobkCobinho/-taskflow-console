var manager = new TaskManager();
var running = true;

while (running)
{
    ShowMenu();
    var option = Console.ReadLine()?.Trim();

    // ReadLine retorna null quando a entrada é encerrada (Ctrl+D / Ctrl+Z)
    if (option is null)
    {
        break;
    }

    Console.WriteLine();

    switch (option)
    {
        case "1":
            AddTask();
            break;
        case "2":
            ListTasks();
            break;
        case "3":
            CompleteTask();
            break;
        case "0":
            running = false;
            Console.WriteLine("Até logo!");
            break;
        default:
            Console.WriteLine("Opção inválida. Tente novamente.");
            break;
    }
}

void ShowMenu()
{
    Console.WriteLine();
    Console.WriteLine("=== TaskFlow ===");
    Console.WriteLine("1 - Adicionar tarefa");
    Console.WriteLine("2 - Listar tarefas");
    Console.WriteLine("3 - Concluir tarefa");
    Console.WriteLine("0 - Sair");
    Console.Write("Escolha uma opção: ");
}

void AddTask()
{
    Console.Write("Título da tarefa: ");
    var title = Console.ReadLine();

    try
    {
        var task = manager.AddTask(title ?? string.Empty);
        Console.WriteLine($"Tarefa adicionada: \"{task.Title}\"");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Erro: {ex.Message}");
    }
}

void ListTasks()
{
    if (manager.Tasks.Count == 0)
    {
        Console.WriteLine("Nenhuma tarefa cadastrada.");
        return;
    }

    var number = 1;
    foreach (var task in manager.Tasks)
    {
        var status = task.IsCompleted ? "[x]" : "[ ]";
        Console.WriteLine($"{number++}. {status} {task.Title}");
    }
}

void CompleteTask()
{
    if (manager.Tasks.Count == 0)
    {
        Console.WriteLine("Nenhuma tarefa cadastrada.");
        return;
    }

    ListTasks();
    Console.Write("Número da tarefa a concluir: ");

    if (!int.TryParse(Console.ReadLine(), out var number) || number < 1 || number > manager.Tasks.Count)
    {
        Console.WriteLine("Número inválido.");
        return;
    }

    var task = manager.Tasks.ElementAt(number - 1);
    manager.CompleteTask(task.Id);
    Console.WriteLine($"Tarefa concluída: \"{task.Title}\"");
}
