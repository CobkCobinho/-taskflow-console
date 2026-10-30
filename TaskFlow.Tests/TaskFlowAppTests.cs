public class TaskFlowAppTests
{
    private static string Run(string input)
    {
        using var reader = new StringReader(input);
        using var writer = new StringWriter();
        new TaskFlowApp(reader, writer).Run();
        return writer.ToString();
    }

    [Fact]
    public void AddListAndComplete_UsesTheDisplayedTaskNumber()
    {
        var output = Run("1\nPrimeira\n1\n Segunda \n3\n2\n2\n0\n");
        Assert.Contains("1. [ ] Primeira", output);
        Assert.Contains("2. [x] Segunda", output);
        Assert.Contains("Tarefa concluída: \"Segunda\"", output);
        Assert.DoesNotContain("1. [x] Primeira", output);
        Assert.EndsWith("Até logo!" + Environment.NewLine, output);
    }

    [Theory]
    [InlineData("2\n0\n")]
    [InlineData("3\n0\n")]
    public void EmptyList_ShowsAnExplanation(string input)
    {
        Assert.Contains("Nenhuma tarefa cadastrada.", Run(input));
    }

    [Fact]
    public void InvalidOptionAndBlankTitle_AllowTheNextValidCommand()
    {
        var output = Run("abc\n1\n   \n1\nVálida\n2\n0\n");
        Assert.Contains("Opção inválida.", output);
        Assert.Contains("Erro:", output);
        Assert.Contains("1. [ ] Válida", output);
        Assert.DoesNotContain("2. [ ]", output);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("2")]
    [InlineData("abc")]
    [InlineData("2147483648")]
    [InlineData("")]
    public void InvalidTaskNumber_DoesNotCompleteAnyTask(string number)
    {
        var output = Run($"1\nPendente\n3\n{number}\n2\n0\n");
        Assert.Contains("Número inválido.", output);
        Assert.Contains("1. [ ] Pendente", output);
        Assert.DoesNotContain("[x]", output);
    }

    [Theory]
    [InlineData("", 1)]
    [InlineData("1\n", 1)]
    [InlineData("1\nTarefa\n3\n", 2)]
    public void EndOfInput_ExitsWithoutValidationErrorsOrAnotherMenu(string input, int menus)
    {
        var output = Run(input);
        Assert.DoesNotContain("Erro:", output);
        Assert.DoesNotContain("inválid", output);
        Assert.Equal(menus, output.Split("=== TaskFlow ===").Length - 1);
    }

    [Fact]
    public void SeparateSessions_DoNotShareTasks()
    {
        Run("1\nSomente nesta sessão\n0\n");
        Assert.Contains("Nenhuma tarefa cadastrada.", Run("2\n0\n"));
    }
}
