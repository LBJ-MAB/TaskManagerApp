namespace TaskManagerApp
{
    class Task
    {
        public string Title { get; set; }           // Title property
        public string Description { get; set; }     // Description property
        // public 
    }
    
    class TaskManagerApp
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Task Manager App ---");
            DateTime test = DateTime.Parse("02/10/2000");
            Console.WriteLine(test.ToLongDateString());
        }
    }
}