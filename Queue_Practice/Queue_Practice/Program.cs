using System.Text;

namespace Queue_Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Queue<Task> taskQueue = new Queue<Task>();

            Task task1 = new Task("Task 1", "Practice Queue<T> type", "Coding", false);

            AddTask(taskQueue, task1);
            taskQueue.Enqueue(new Task { taskName = "Task 2", taskDescription = "More practice", taskType = "Coding", taskComplete = false });

            Console.WriteLine($"Current number of tasks: {taskQueue.Count}");

            do 
            {
                ViewTask(taskQueue);
                RemoveTask(taskQueue);
            } while (taskQueue.Count > 0);

            ViewTask(taskQueue);
            RemoveTask(taskQueue);
        }

        static void AddTask(Queue<Task> taskQueue, Task task)
        {
            taskQueue.Enqueue(task);
        }

        static void ViewTask(Queue<Task> taskQueue)
        {
            Console.WriteLine("Viewing current task in queue:");
            string currentTask = taskQueue.Count > 0 ? taskQueue.Peek().ToString() : "No tasks to complete";
            Console.WriteLine($"{currentTask}");
        }

        static void RemoveTask(Queue<Task> taskQueue)
        {
            if (taskQueue.Count > 0)
            {
                Task currentTask = taskQueue.Dequeue();
                Console.WriteLine($"Removed {currentTask.taskName} ({currentTask.taskNum})\n");
            }
            else
            {
                Console.WriteLine("No tasks to remove");
            }
        }
    }

    public class Task
    {
        internal Guid taskNum { get; set; } = Guid.NewGuid();
        internal string taskName {  get; set; }
        internal string taskDescription { get; set; }
        internal string taskType { get; set; }
        internal bool taskComplete { get; set; }

        public Task() : this("Unknown", "Unknown", "Unknown", false) { }

        public Task(string taskName, string taskDescription, string taskType, bool taskComplete)
        {
            this.taskName = taskName;
            this.taskDescription = taskDescription;
            this.taskType = taskType;
            this.taskComplete = taskComplete;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Task Number: {taskNum}");
            sb.AppendLine($"Task Name: {taskName}");
            sb.AppendLine($"Description: {taskDescription}");
            sb.AppendLine($"Type: {taskType}");
            sb.AppendLine($"Complete: {taskComplete}");

            return sb.ToString();
        }
    }
}