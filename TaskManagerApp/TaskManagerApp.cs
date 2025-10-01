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
        
        // constructor for the Task class:
        public UserTask(string? title, string? description, string? date, bool isComplete)
        {
             Title = title;                  // set Title
             Description = description;      // set Description
             Date = date;                    // set Date
             IsComplete = isComplete;        // set IsComplete
             // ID = length of TaskList , and use this for when task is completed - display ID on the list
        }
    }

    class TaskManager       // class for task manager
    {
        // initialise a list for storing UserTasks
        public List<UserTask> TaskList = new List<UserTask>();
        
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
            UserTask newTask = new UserTask(title, desc, date, false);
            
            // add the UserTask() object to the TaskList
            TaskList.Add(newTask);
        }
        
        // CompleteTask( title ) method
        public void CompleteTask(string title)
        {
            // iterate through list
            foreach (var task in TaskList)
            {
                // find task that matches the title
                if (task.Title == title && !task.IsComplete)
                {
                    // change the IsComplete status to true
                    task.IsComplete = true;
                    Console.WriteLine("Pending task '{0}' completed", title);
                    return;
                }
            }
            
            // tell user that no task was found with that title
            Console.WriteLine("No pending task found with title '{0}'", title);
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
                        Console.WriteLine("Title       : {0} --- {1}", task.Title, task.IsComplete ? "COMPLETE" : "PENDING");
                        Console.WriteLine("Description : {0}", task.Description);
                        Console.WriteLine("Deadline    : {0}\n", task.Date);
                    }
                }
                // print message if no tasks to display
                else
                {
                    Console.WriteLine("No {0} tasks\n", isComplete ? "completed" : "pending");
                }
            }
        }
        
        // read tasks from json file method
        public void ReadTasksFromJson()
        {
            // define url to retrieve from
            string jsonFilePath = "tasks.json";
            
            // define json string that we are reading in
            string jsonString = File.ReadAllText(jsonFilePath);
            
            // deserialize Json back into list of UserTask() objects
            TaskList = JsonSerializer.Deserialize<List<UserTask>>(jsonString)!;
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
                        Console.Write("Please give the title of the task to be completed: ");
                        string? targetTitle = Console.ReadLine();
                        myTasks.CompleteTask(targetTitle!);
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
                        Console.WriteLine("ERROR: response was not valid");
                        break;
                }
            }
        }
    }
}