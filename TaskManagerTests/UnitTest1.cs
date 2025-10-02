using FluentAssertions;

namespace TaskManagerTests;

public class Tests
{
    private TaskManagerApp.TaskManager _taskManager;
    
    [SetUp]
    public void Setup()
    {
        _taskManager = new TaskManagerApp.TaskManager();
    }

    [Test]
    public void TestNumberOfTasksInList()
    {
        /*
         * Before running this test, need to make sure we don't have Console.ReadLine()
         * in the method.
         * Instead, overwrite the values for title, desc and date temporarily
         */
        
        _taskManager.TaskList.Should().BeEmpty();
        
        int numTasks = 100;
        for (int taskIndex = 1; taskIndex <= numTasks; taskIndex++)
        {
            _taskManager.AddTask();     
            _taskManager.TaskList.Should().HaveCount(taskIndex);
        }
    }

    [Test]
    public void TestListItemIsNotEmpty()
    {
        _taskManager.TaskList.Should().BeEmpty();
        _taskManager.AddTask();
        _taskManager.TaskList.Should().NotBeEmpty();
    }

    [Test]
    public void TestTaskDetails()
    {
        _taskManager.AddTask();
        _taskManager.TaskList[0].Title.Should().Be("Task 1");
        _taskManager.TaskList[0].Description.Should().Be("first task of the day");
        _taskManager.TaskList[0].Date.Should().Be("02/10/2025");
        _taskManager.TaskList[0].IsComplete.Should().Be(false);
        _taskManager.TaskList[0].TaskId.Should().Be(0);
    }

    [Test]
    public void TestTaskIsEquivalentTo()
    {
        // make a new user task
        var userTask = new TaskManagerApp.UserTask(
            "Task 1",
            "first task of the day",
            "02/10/2025",
            false,
            0
        );
        
        // add a task
        _taskManager.AddTask();

        // assert
        _taskManager.TaskList[0].Should().BeEquivalentTo(userTask);
    }

    [Test]
    public void TestIsCompletedChange()
    {
        for (int i = 0; i < 100; i++)
        {
            _taskManager.AddTask();
            _taskManager.CompleteTask(i + 1);
            _taskManager.TaskList[i].IsComplete.Should().Be(true);
        }
    }
}