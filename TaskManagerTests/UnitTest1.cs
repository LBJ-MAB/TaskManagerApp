namespace TaskManagerTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        var TestTasks = new TaskManagerApp.TaskManager();
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}