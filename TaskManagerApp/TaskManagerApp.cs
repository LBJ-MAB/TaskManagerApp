namespace TaskManagerApp
{
    class UserTask      // class for a single task
    {
        public string? Title { get; set; }           // Title property
        public string? Description { get; set; }     // Description property
        public string? Date { get; set; }            // Date property
        public bool IsComplete { get; set; }         // IsComplete property
        
        // constructor for the Task class:
        public UserTask(string? Title, string? Description, string? Date)
        {
            IsComplete = false;         // false on creation
        }
    }

    class TaskManager       // class for task manager
    {
        public List<UserTask> TaskList = new List<UserTask>();      // create list for storing UserTasks
        public void AddTask()       // method for adding a task to TaskList
        {
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
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Task Manager App ---");
            
            // DateTime test = DateTime.Parse("02/10/2000");
            // Console.WriteLine(test.ToLongDateString());
        }
    }
}