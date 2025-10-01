namespace TaskManagerApp
{
    class Task      // class for a single task
    {
        public string Title { get; set; }           // Title property
        public string Description { get; set; }     // Description property
        public string Date { get; set; }            // Date property
        public bool IsComplete { get; set; }        // IsComplete property
        
        // constructor for the Task class:
        public Task(string taskTitle, string taskDescription, string taskDate)
        {
            Title = taskTitle;              // set title
            Description = taskDescription;  // set description
            Date = taskDate;                // set date
            IsComplete = false;             // initialise IsComplete as false
        }
    }

    class TaskManager       // class for task manager
    {
        public List<Task> TaskList = new List<Task>();      // create list for storing Tasks
    }
    
    class TaskManagerApp
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Task Manager App ---");
            
            // DateTime test = DateTime.Parse("02/10/2000");
            // Console.WriteLine(test.ToLongDateString());
            
            // make an object of instance Task()
            Task firstTask = new Task("task 1", "first task of the day","11/10/2025");
        }
    }
}