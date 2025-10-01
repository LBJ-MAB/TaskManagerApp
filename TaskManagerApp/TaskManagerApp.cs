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
            
            Console.WriteLine("{0} --- {1}", Title, IsComplete ? "COMPLETE" : "PENDING");
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
            Console.WriteLine("\n - Current Task List - ");

            // print pending tasks
            Console.WriteLine("PENDING tasks:");
            var pendingTasks = TaskList.Where(task => !task.IsComplete);
            foreach (var task in pendingTasks)
            {
                task.PrintTaskDetails();
            }

            // print completed tasks
            Console.WriteLine("COMPLETED tasks:");
            var completedTasks = TaskList.Where(task => task.IsComplete);
            foreach (var task in completedTasks)
            {
                task.PrintTaskDetails();
            }
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            // make a new TaskManager class called MyTasks
            TaskManager myTasks = new TaskManager();
            
            // ask user if they wish to add a task, mark a task as complete, view all tasks or close the app
            var isOpen = true;
            while (isOpen)
            {
                Console.Write("\nType 'add' to add a task, 'mark' to change the complete status of a task, 'view' to" +
                              "view the task list, or 'close' to close the application");
            }

            // add a new task to MyTasks
            // myTasks.AddTask();
            // myTasks.AddTask();

            // print out all tasks so far
            // myTasks.PrintTasks();
        }
    }
}