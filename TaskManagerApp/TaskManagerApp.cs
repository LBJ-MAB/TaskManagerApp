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
    }

    class TaskManager       // class for task manager
    {
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
            
            Console.WriteLine("\n - Current Task List - \n");

            foreach (var task in TaskList)
            {
                Console.WriteLine("{0} --- {1}", task.Title, task.IsComplete ? "COMPLETE" : "PENDING");
                Console.WriteLine("Description : {0}", task.Description);
                Console.WriteLine("Deadline    : {0}\n", task.Date);
            }
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Task Manager App ---");
            
            // make a new TaskManager class called MyTasks
            TaskManager myTasks = new TaskManager();
            
            // add a new task to MyTasks
            myTasks.AddTask();
            myTasks.AddTask();
            
            // print out all tasks so far
            myTasks.PrintTasks();
        }
    }
}