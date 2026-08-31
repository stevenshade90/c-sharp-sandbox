using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ToDoApp
{
    internal class Program
    {
        static void Main()
        {
            //User creation sequence
            Console.ForegroundColor = ConsoleColor.Cyan;
            UserAccount user1 = UserCreation();

            static UserAccount UserCreation()
            {
                String? username;
                object? logname;
                int? logId;

                do
                {
                    Console.Write("Enter your username: ");
                    username = Console.ReadLine();
                } while (String.IsNullOrWhiteSpace(username));

                do
                {
                    Console.Write("Enter your log name: ");
                    var nameOfLog = Console.ReadLine();
                    logname = int.TryParse(nameOfLog, out _) ? null : nameOfLog;

                    switch (logname)
                    {
                        case int i:
                        case null:
                            Console.WriteLine("Do not enter a number");
                            continue;

                        case String s:
                            s = (String)logname;
                            if (String.IsNullOrWhiteSpace(s))
                            {
                                continue;
                            }
                            else
                            {
                                break;
                            }

                        default:
                            Console.WriteLine("Invalid entry");
                            continue;
                    }

                    break;

                } while(true);

                do
                {
                    try
                    {
                        Console.Write("Enter log ID number: ");
                        logId = int.Parse(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Enter a valid number");
                    }
                } while (true);
                Console.WriteLine();

                return new UserAccount(username, (String)logname, logId);
            }


            //Interaction with task commands
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.Write("Enter your task, or type 'menu' to view options: ");
                String? response = Console.ReadLine();

                if (String.IsNullOrWhiteSpace(response))
                {
                    Console.WriteLine("You need to enter a value");
                    continue;
                }
                else if (int.TryParse(response, out _))
                {
                    Console.WriteLine("Can't enter a number");
                    continue;
                }
                else if (response.Equals("menu", StringComparison.OrdinalIgnoreCase))
                {
                    user1.AccessMenuChoice(user1.PrimaryUserTaskLog);
                    continue;
                }

                try
                {
                    String firstChar = Convert.ToString(response.ElementAt(0));
                    firstChar = firstChar.ToUpper();

                    String userResponse = String.Concat(firstChar, response.Substring(1));

                    Console.Write("Enter due month: ");
                    String userMonthDue = Console.ReadLine();

                    Console.Write("Enter due day: ");
                    String userDayDue = Console.ReadLine();

                    int.TryParse(userMonthDue, out int monthDue);
                    int.TryParse(userDayDue, out int dayDue);

                    TaskCreation newTask = CreateTask(userResponse, new DateOnly(2026, monthDue, dayDue));
                    user1.TaskAdder(newTask);

                    Console.WriteLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public static TaskCreation CreateTask(String task, DateOnly date)
        {
            return new TaskCreation(task, date);
        }
    }

    public partial class TaskCreation
    {
        public string TaskDescription {  get; set; }
        public DateOnly dueDate {  get; set; }
        public TaskCreation(String task, DateOnly date)
        {
            TaskDescription = task;
            dueDate = date;
        }

        public override string ToString()
        {
            return $"{TaskDescription} -- Due By: {dueDate}";
        }
    }

    public class UserAccount 
    {
        public List<String>[] UserTaskLogCollection = new List<String>[10];
        public List<TaskCreation> PrimaryUserTaskLog = new List<TaskCreation>();

        public String User { get; set; } = "None";
        public String TaskLogName { get; set; } = "None";
        public int? TaskLogId { get; set; } = 0;
        public DateTime LogCreationTime { get; set; } = DateTime.Now;


        public TaskLogManipulation TaskLogManipulator;

        private int _i = 0;
        public UserAccount() 
            : this ("", "", null) { }

        public UserAccount(String username, String logName, int? logId)
        {
            User = username;
            TaskLogName = logName;
            TaskLogId = logId;
            LogCreationTime = DateTime.Now;
            TaskLogManipulator = new TaskLogManipulation(User, TaskLogName, TaskLogId, DateTime.Now);
        }

        public void AddTaskLog(List<String> e)
        {
            UserTaskLogCollection[_i] = e;
            _i += 1;
        }

        public void AccessMenuChoice(List<TaskCreation> taskLog)
        {
            TaskLogManipulator.MenuChoice(taskLog);
        }

        public void TaskAdder(TaskCreation newTask)
        {
            DateOnly current = DateOnly.FromDateTime(DateTime.Now);

            if ((newTask.dueDate.Month - current.Month) > 2)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                PrimaryUserTaskLog.Add(newTask);
            }
            else if ((newTask.dueDate.Month - current.Month) > 1)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                PrimaryUserTaskLog.Add(newTask);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                PrimaryUserTaskLog.Add(newTask);
            }

            Console.WriteLine("Item added to list!");
        }
    }


    public class TaskLogManipulation
    {
        public String User { get; set; }
        public String TaskLogName { get; set; }
        public int? TaskLogId { get; set; }
        public DateTime LogCreationTime { get; set; } 

        public TaskSaver ts = new TaskSaver();

        public TaskLogManipulation(String username, String taskLogName, int? taskLogId, DateTime creationTime) 
        { 
            User = username;
            TaskLogName = taskLogName;
            TaskLogId = taskLogId;
            LogCreationTime = creationTime;
        }
        public void MenuChoice(List<TaskCreation> userPrimaryTaskLog)
        {
            Console.WriteLine("Select an option");
            Console.WriteLine("\t1. View list");
            Console.WriteLine("\t2. Remove task");
            Console.WriteLine("\t3. Save file");
            Console.WriteLine("\t4. Load file");
            Console.WriteLine("\t5. View user information");
            Console.WriteLine("\t6. Clear all tasks");
            Console.WriteLine("\t7. Exit");
            Console.Write("Selection: ");
            String? userSelection = Console.ReadLine();

            switch (userSelection?.Trim())
            {
                case String s when s.Equals("1"):
                    ToDoViewer(userPrimaryTaskLog);
                    break;
                case String s when s.Equals("2"):
                    ToDoTaskRemover(userPrimaryTaskLog);
                    break;
                case String s when s.Equals("3"):
                    ToDoSaver(userPrimaryTaskLog);
                    break;
                case String s when s.Equals("4"):
                    ToDoLoadFile(userPrimaryTaskLog);
                    break;
                case String s when s.Equals("5"):
                    DisplayUserInformation();
                    break;
                case String s when s.Equals("6"):
                    ClearAllTasks(userPrimaryTaskLog);
                    break;
                case String s when s.Equals("7"):
                    ToDoExit();
                    break;
                default:
                    Console.WriteLine("Not an option");
                    break;
            }
        }

        public void ToDoViewer(List<TaskCreation> primaryTaskLog) //1. View list
        {
            
            DateOnly current = DateOnly.FromDateTime(DateTime.Now);

            Console.WriteLine();
            Console.WriteLine("-------Your Tasks-------");

            int i = 1;

            if (primaryTaskLog.Count == 0)
            {
                Console.WriteLine("No tasks to complete!");
            }
            else
            {
                primaryTaskLog.ForEach(task =>
                {
                    if ((task.dueDate.Month - current.Month) > 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if ((task.dueDate.Month - current.Month) > 1)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;    
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }

                    Console.WriteLine($"{i}. {task.ToString()}");
                    i++;
                });
        }
        Console.WriteLine();
        }

        public void ToDoTaskRemover(List<TaskCreation> e) //2. Remove task
        {
            bool itemNotRemoved = true;
            do
            {
                Console.WriteLine();

                if (e.Count == 0)
                {
                    Console.WriteLine("There are no items to remove!\n");
                    itemNotRemoved = false;
                    break;
                }
                else
                {
                    int i = 1;
                    Console.WriteLine("There are this many item(s) in your list: {0}", e.Count);
                    Console.WriteLine("To remove an item, enter a number between 1 and {0}:", e.Count);
                    Console.WriteLine("Current Tasks:");

                    Action<int, String> DisplayTasks = (numberIndex, taskValue) =>
                    {
                        Console.WriteLine("\t{0}. {1}", numberIndex, taskValue);
                        i++;

                    };
                    e.ForEach(task => DisplayTasks(i, task.TaskDescription));

                    Console.Write("Item to remove: ");
                }

                try
                {
                    int userChoice = Convert.ToInt32(Console.ReadLine());
                    userChoice -= 1;
                    if (userChoice > e.Count)
                    {
                        Console.WriteLine("You cannot remove an item bigger than the size of the list");
                        throw new IndexOutOfRangeException();
                    }
                    else
                    {
                        Console.WriteLine("Removing item: {0}", e[userChoice]);
                        e.RemoveAt(userChoice);
                        itemNotRemoved = false;
                    }
                    Console.WriteLine();
                }
                catch (IndexOutOfRangeException em1)
                {
                    Console.WriteLine(em1.Message);
                }
                catch (InvalidDataException em2)
                {
                    Console.WriteLine(em2.Message);
                }
                catch (Exception em3)
                {
                    Console.WriteLine(em3);
                }
            } while (itemNotRemoved);
        }

        public void ToDoSaver(List<TaskCreation> primaryTaskLog) //3. Saving
        {
            ts.ToDoSaver(primaryTaskLog);
        }
        public void ToDoLoadFile(List<TaskCreation> primaryTaskLog) //4. Loading
        { 
            ts.ToDoLoadFile(primaryTaskLog);
        }

        public void DisplayUserInformation() //5. Display user info
        {
            Console.WriteLine("\nUser: {0}", User);
            Console.WriteLine("Log Name: {0}", TaskLogName);
            Console.WriteLine("Log ID: {0}", TaskLogId);
            Console.WriteLine("Time created: {0}", LogCreationTime);
            Console.WriteLine();
        }

        public void ClearAllTasks(List<TaskCreation> e)
        {
            String userChoice = String.Empty;
            do
            {
                Console.Write("\nClear all items in list? (Y/N): ");
                userChoice = Console.ReadLine();
                userChoice = userChoice.ToLower();

            } while (userChoice != "y" && userChoice != "n");

            switch (userChoice)
            {
                case "y":
                    try
                    {
                        e.Clear();
                        Console.WriteLine("List cleared!\n");
                    }
                    catch (Exception e2)
                    {
                        Console.WriteLine(e2);
                    }
                    break;
                case "n":
                    Console.WriteLine("Not clearing list\n");
                    break;
            }
        }

        public void ToDoExit() //7. Exit 
        {
            Console.WriteLine("Bye!");
            Environment.Exit(0);
        }
    }

    public class TaskSaver : ISaveAndLoadFile
    {
        public void ToDoSaver(List<TaskCreation> primaryTaskLog) //3. Save file
        {
            Console.WriteLine("\nSaving file...");

            try
            {
                using (StreamWriter writer = File.CreateText("MyToDo.txt"))
                {
                    writer.WriteLine("To Do List");
                    writer.WriteLine("--------------------------------------------");
                    writer.WriteLine();

                    int i = 1;
                    foreach (TaskCreation task in primaryTaskLog)
                    {
                        writer.WriteLine("{0}. {1}", i, task.ToString());
                        i++;
                    }
                }
                Console.WriteLine("File saved!\n");
            }
            catch (Exception error)
            {
                Console.WriteLine("File not saved");
                Console.WriteLine(error.Message);
            }
        }

        //this will need to be better updated to account for a split that will
        //need to happen on the DateOnly 
        public void ToDoLoadFile(List<TaskCreation> primaryTaskLog) //4. Load File
        {
            Console.Write("\nEnter 'Y' to load file: ");
            String fileLoadChoice = Console.ReadLine() ?? "";

            if (fileLoadChoice.Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                primaryTaskLog.Clear();
                using (StreamReader reader = new StreamReader("MyToDo.txt"))
                {
                    String? input = null;

                    while ((input = reader.ReadLine()) != null)
                    {
                        int periodIndex = input.IndexOf(". ");
                        if (periodIndex > 0)
                        {
                            input = input.Substring(periodIndex + 2);
                            primaryTaskLog.Add(new TaskCreation(input, DateOnly.FromDateTime(DateTime.Now)));
                        }
                    }
                }
                Console.WriteLine("File successfully loaded!\n");
            }
            else
            {
                Console.WriteLine("No file loaded");
                Console.WriteLine();
            }
        }
    }

    public interface ISaveAndLoadFile
    {
        void ToDoSaver(List<TaskCreation> e);
        void ToDoLoadFile(List<TaskCreation> e);
    }
}