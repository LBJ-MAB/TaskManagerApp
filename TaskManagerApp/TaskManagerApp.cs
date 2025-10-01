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
        public UserTask(string? title, string? description, string? date)
        {
             Title = title;                  // set Title
             Description = description;      // set Description
             Date = date;                    // set Date
             IsComplete = false;             // set IsComplete
        }

        // PrintTaskDetails() method
        public void PrintTaskDetails()
        {
            /*
             * Method that prints out the title, completed status, description and date of the task
             */
            
            Console.WriteLine("Title       : {0} --- {1}", Title, IsComplete ? "COMPLETE" : "PENDING");
            Console.WriteLine("Description : {0}", Description);
            Console.WriteLine("Deadline    : {0}\n", Date);
        }
    }

    class TaskManager       // class for task manager
    {
        // constructor with welcome message and print current tasks
        public TaskManager()
        {
            Console.WriteLine("--- Welcome to Task Manager App ---");
            PrintTasks();
        }
        
        // create list for storing UserTasks
        public List<UserTask> TaskList = new List<UserTask>();      
        
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
            
            // create a UserTask() object using title, desc, date
            UserTask newTask = new UserTask(title, desc, date);
            
            // add the UserTask() object to the TaskList
            TaskList.Add(newTask);
        }

        // PrintTasks() method
        public void PrintTasks()
        {
            /*
             * Method for printing out all tasks in TaskList
             */
            
            // intro
            Console.WriteLine("\n -- Current Task List -- ");

            // print pending tasks
            Console.WriteLine(" - PENDING tasks -\n");
            var pendingTasks = TaskList.Where(task => !task.IsComplete);
            foreach (var task in pendingTasks)
            {
                task.PrintTaskDetails();
            }

            // print completed tasks
            Console.WriteLine(" - COMPLETED tasks -\n");
            var completedTasks = TaskList.Where(task => task.IsComplete);
            foreach (var task in completedTasks)
            {
                task.PrintTaskDetails();
            }
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
                    case "add":
                        myTasks.AddTask();
                        break;
                    case "complete":
                        Console.Write("Please give the title of the task to be completed: ");
                        string? targetTitle = Console.ReadLine();
                        myTasks.CompleteTask(targetTitle!);
                        break;
                    case "view":
                        myTasks.PrintTasks();
                        break;
                    case "close":
                        Console.WriteLine("Goodbye");
                        isOpen = false;         // set isOpen to false
                        break;
                    default:
                        Console.WriteLine("ERROR: response was not valid");
                        break;
                }
            }
        }
    }
}