public class TaskItemTests
{
    [Fact]
    public void Create_TrimsTitleAndAssignsDistinctNonEmptyIds()
    {
        var first = new TaskItem("  Estudar C#  ");
        var second = new TaskItem("Estudar C#");
        Assert.Equal("Estudar C#", first.Title);
        Assert.NotEqual(Guid.Empty, first.Id);
        Assert.NotEqual(first.Id, second.Id);
        Assert.False(first.IsCompleted);
    }

    [Fact]
    public void Complete_IsIdempotentAndPreservesIdentityAndTitle()
    {
        var task = new TaskItem("Estudar");
        var id = task.Id;
        task.Complete();
        task.Complete();
        Assert.True(task.IsCompleted);
        Assert.Equal(id, task.Id);
        Assert.Equal("Estudar", task.Title);
    }

    [Fact]
    public void CompleteTask_ChangesOnlyTheRequestedTask()
    {
        var manager = new TaskManager();
        var first = manager.AddTask("Primeira");
        var second = manager.AddTask("Segunda");
        Assert.True(manager.CompleteTask(second.Id));
        Assert.False(first.IsCompleted);
        Assert.True(second.IsCompleted);
        Assert.Same(second, manager.GetById(second.Id));
        Assert.Equal(2, manager.Tasks.Count);
    }
}
