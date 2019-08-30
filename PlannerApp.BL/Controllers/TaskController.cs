using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PlannerApp.BL
{
    public partial class TaskController:ITaskController
    {
        public List<Task> tasks { get; set; }
        public TaskController(List<Task> tasks) => this.tasks = tasks ?? throw new ArgumentNullException("Список пустой", nameof(tasks));
        public TaskController()
        {
            tasks = new List<Task>();
        }
        public int AddTask()
        {
            int id = default;
            string taskName, taskDescription; taskName = taskDescription = default;
            DateTime timeStart, timeFinish; timeStart = timeFinish = default;
            string day, month, year; day = month = year = default;
            Console.WriteLine("Добавление задачи...");
            #region Валидация
            Console.WriteLine("Введите ID номер задачи");
            string input;bool resultType;
            while (true)
            {
                try
                {
                    input= Console.ReadLine();
                    if (!int.TryParse(input, out id)) throw new Exception("Неверный формат ввода");
                    if (tasks.Count!=0) foreach(Task t in tasks)
                        {
                            if (t.id==int.Parse(input)) throw new Exception("Поле id должно быть уникально");
                        }
                    id = Convert.ToInt32(input);
                    break;
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Ошибка: {e.Message}");
                    Console.WriteLine("Пожалуйста введите id заново");
                }
            }
            Console.WriteLine("Введите пожалуйста название задачи");
            while (true)
            {
                taskName = Console.ReadLine();
                try
                {
                    if (string.IsNullOrWhiteSpace(taskName) || taskName.Length > 20) throw new Exception("Название не может быть пустым либо содержать больше 20 символов");
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка: {e.Message}");
                    Console.WriteLine("Пожалуйста введите название задачи заново");
                }
            }
            Console.WriteLine("Введите пожалуйста описание задачи");
            while (true)
            {
                taskDescription = Console.ReadLine();
                try
                {
                    if (string.IsNullOrWhiteSpace(taskDescription) || taskDescription.Length > 50) throw new Exception("Описание не может быть пустым либо содержать больше 50 символов");
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка: {e.Message}");
                    Console.WriteLine("Пожалуйста введите описание задачи заново");
                }
            }
            Console.WriteLine("Введите пожалуйста время начала выполнения задачи {дд мм гггг}");
            while (true)
            {
                Console.Write("День:"); day = Console.ReadLine();
                Console.Write("Месяц:"); month = Console.ReadLine();
                Console.Write("Год: "); year = Console.ReadLine();
                timeStart = new DateTime(Convert.ToInt16(year), Convert.ToInt16(month), Convert.ToInt16(day));
                try
                {
                    if ((timeStart < DateTime.Now)) throw new Exception("Введена некорректная дата начала выполнения задачи");
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка: {e.Message}");
                    Console.WriteLine("Пожалуйста введите время начала выполнения задачи заново");
                }
            }  
            Console.WriteLine("Введите пожалуйста время завершения выполнения задачи {дд мм гггг}");
            while (true)
            {
                Console.Write("День:"); day = Console.ReadLine();
                Console.Write("Месяц:"); month = Console.ReadLine();
                Console.Write("Год: "); year = Console.ReadLine();
                timeFinish = new DateTime(Convert.ToInt16(year), Convert.ToInt16(month),Convert.ToInt16(day));
                try
                {
                    if (timeFinish <= timeStart) throw new Exception("Введена некорректная дата начала выполнения задачи");
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка: {e.Message}");
                    Console.WriteLine("Пожалуйста введите время начала выполнения задачи заново");
                }
            }
            #endregion
            tasks.Add(new Task(taskName,taskDescription,timeStart,timeFinish,id));
            return 0;
        }
        public int DeleteTask(int id)
        {
                bool exist = false;
                Console.WriteLine("Введите ID задачи, которую хотите удалить");
                foreach (Task task in tasks)
                {
                    if (task.id == id)
                    {
                        tasks.Remove(task);
                        exist = true;
                        Console.WriteLine("Задача была удалена");
                        break;
                    }
                }
            if (!exist) Console.WriteLine("Задачи с таким id не существует");
            return 0;
        }
        public int EditRecord(int id)
        {
            /*string changeTask=default;
            Console.WriteLine("Введите ID задачи, которую хотите изменить");
            int id = int.Parse(Console.ReadLine());*/
            return 0;
        }
    }
}
