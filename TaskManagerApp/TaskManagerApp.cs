using System.Text.Json;

namespace TaskManagerApp
{
    class UserTask      // class for a single task
    {
        // define properties for userTask
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Date { get; set; }
        public bool IsComplete { get; set; }
        public int TaskId { get; set; }
        
        // constructor for the Task class:
        public UserTask(string? title, string? description, string? date, bool isComplete, int taskId)
        {
             Title = title;                  // set Title
             Description = description;      // set Description
             Date = date;                    // set Date
             IsComplete = isComplete;        // set IsComplete
             TaskId = taskId;                // set task ID 
        }
    }

    class TaskManager       // class for task manager
    {
        // initialise a list for storing UserTasks
        public List<UserTask> TaskList = new List<UserTask>();
        
        // dictionary for storing textType : textColor pairs
        public Dictionary<string, ConsoleColor> TextColors = new Dictionary<string, ConsoleColor>{
            {"pending", ConsoleColor.DarkYellow },
            {"complete", ConsoleColor.Green},
            {"index", ConsoleColor.Blue},
            {"error", ConsoleColor.Red},
            {"update", ConsoleColor.Blue},
            {"empty", ConsoleColor.DarkGray}
        };
        
        // constructor with welcome message, read tasks from json and print current tasks
        public TaskManager()
        {
            Console.WriteLine("--- Welcome to Task Manager App ---");
            
            ReadTasksFromJson();    // read tasks from json file into TaskList
            
            PrintTasks();           // print tasks
        }
        
        // AddTask() method
        public void AddTask()       
        {
            /*
             * Method for adding a UserTask() to TaskList
             */
            
            // intro
            Console.WriteLine("\n - New Task Details -");
            
            // ask for the title
            Console.Write("Title : ");
            string? title = Console.ReadLine();         
            
            // ask for the desc
            Console.Write("Description : ");
            string? desc = Console.ReadLine();        
            
            // ask for the date
            Console.Write("Date : ");
            string? date = Console.ReadLine();        
            
            // create a UserTask() object using title, desc, date, isComplete
            UserTask newTask = new UserTask(title, desc, date, false, TaskList.Count);
            
            // add the UserTask() object to the TaskList
            TaskList.Add(newTask);
        }
        
        // CompleteTask( title ) method
        public void CompleteTask(int consoleId)
        {
            // id for TaskList
            int taskId = consoleId - 1;         
            
            // set task to true if not already
            if (consoleId <= TaskList.Count && !TaskList[taskId].IsComplete)
            {
                TaskList[taskId].IsComplete = true;
                PrintTextWithColor(
                    $"Task {consoleId} : '{TaskList[taskId].Title}' has been moved to completed\n", 
                    "update");
            }
            else
            {
                // tell user that no task was found with that ID
                PrintTextWithColor($"No pending task was found with ID '{consoleId}'\n", "error");
            }
        }

        // PrintTasks() method
        public void PrintTasks()
        {
            /*
             * Method for printing out all tasks in TaskList
             */
            
            // intro
            Console.WriteLine("\n -- Current Task List -- ");
            
            bool[] falseTrueArray = { false, true };

            // loop through false, true
            foreach (bool isComplete in falseTrueArray)
            {
                // print depending on isComplete value
                Console.WriteLine(" - {0} tasks -\n", isComplete ? "COMPLETED" : "PENDING");
                
                // filter tasks depending on isComplete value
                var filteredTasks = TaskList.Where(task => task.IsComplete == isComplete).ToList();

                // print tasks if there are any
                if (filteredTasks.Count > 0)
                {
                    foreach (var task in filteredTasks)
                    {
                        PrintTextWithColor($"{task.TaskId + 1}", "index");
                        Console.Write("  Title       : {0} --- ", task.Title);
                        PrintTextWithColor(task.IsComplete ? "COMPLETE" : "PENDING", task.IsComplete ? "complete" : "pending");
                        Console.WriteLine("");
                        Console.WriteLine("   Description : {0}", task.Description);
                        Console.WriteLine("   Deadline    : {0}\n", task.Date);
                    }
                }
                
                // print message if no tasks to display
                else
                {
                    if (isComplete)
                    {
                        PrintTextWithColor($"No completed tasks\n\n", "empty");
                    }
                    else
                    {
                        PrintTextWithColor($"No pending tasks\n\n", "empty");
                    }
                }
            }
        }
        
        // print with color method
        public void PrintTextWithColor(string text, string textType)
        {
            // change the text color for the console
            Console.ForegroundColor = TextColors[textType];

            // console.write(text)
            Console.Write(text);

            // change the text color back to Black
            Console.ForegroundColor = ConsoleColor.Black;
        }
        
        // read tasks from json file method
        public void ReadTasksFromJson()
        {
            // define url to retrieve from
            string jsonFilePath = "tasks.json";
            
            // define json string that we are reading in
            string jsonString = File.ReadAllText(jsonFilePath);
            
            // deserialize Json back into list of UserTask() objects if json string is usable
            if (!string.IsNullOrEmpty(jsonString))
            {
                TaskList = JsonSerializer.Deserialize<List<UserTask>>(jsonString)!;
            }
        }
        
        // write tasks to json file method
        public void WriteTasksToJson()
        {
            // turn whole TaskList into jsonString
            string jsonString = JsonSerializer.Serialize(TaskList);
            
            // write the json string to file
            File.WriteAllText("tasks.json", jsonString);
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            // make a new TaskManager object called MyTasks
            TaskManager myTasks = new TaskManager();
            
            // ask user to add, complete, view or close
            var isOpen = true;
            while (isOpen)
            {
                // intro
                Console.Write("\nType 'add' to add a task, 'complete' to change a pending task to completed, 'view' to " +
                              "view the task list, or 'close' to close the application: ");
                // response
                string? response = Console.ReadLine();

                // use different methods depending on response
                switch (response)
                {
                    // add a task to list
                    case "add":     
                        myTasks.AddTask();
                        break;
                    // change a task to completed
                    case "complete":
                        Console.Write("Please provide the index of the task to be completed: ");
                        try
                        {
                            int consoleId = Convert.ToInt32(Console.ReadLine());
                            myTasks.CompleteTask(consoleId);
                        }
                        catch
                        {
                            myTasks.PrintTextWithColor("ERROR: Please provide a valid integer value for the task index", "error");
                        }
                        break;
                    // print all tasks
                    case "view":
                        myTasks.PrintTasks();
                        break;
                    // close the app
                    case "close":
                        // write the first task in the list to json file
                        myTasks.WriteTasksToJson();
                        Console.WriteLine("Goodbye :)");
                        isOpen = false;         // set isOpen to false
                        break;
                    // none of the above
                    default:
                        myTasks.PrintTextWithColor("ERROR: response was not valid", "error");
                        break;
                }
            }
        }
    }
}