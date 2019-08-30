using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PlannerApp.BL
{
    public partial class TaskController : ITaskController
    {
        public List<Task> tasks { get; set; }
        public TaskController(List<Task> tasks) => this.tasks = tasks ?? throw new ArgumentNullException("Список задач пуст!", nameof(tasks));
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
            Console.WriteLine("Введите id номер задачи");
            string input; bool resultType;
            while (true)
            {
                try
                {
                    input = Console.ReadLine();
                    if (!int.TryParse(input, out id)||int.Parse(input)<0) throw new Exception("Неверный формат ввода");
                    if (tasks.Count != 0) foreach (Task t in tasks)
                        {
                            if (t.id == int.Parse(input)) throw new Exception("Поле id должно быть уникально");
                        }
                    id = Convert.ToInt32(input);
                    break;
                }
                catch (Exception e)
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
                while (true)
                {
                    try
                    {
                        Console.Write("День:");
                        day = Console.ReadLine();
                        Console.Write("Месяц:");
                        month = Console.ReadLine();
                        Console.Write("Год: ");
                        year = Console.ReadLine();
                        if (int.Parse(day) < 0 || int.Parse(month) < 0 || int.Parse(year) < 0) throw new Exception("Неверный формат ввода даты");
                        break;
                    }
                    catch (Exception e) { Console.WriteLine($"Ошибка: {e.Message}"); }
                }
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
                while (true)
                {
                    try
                    {
                        Console.Write("День:");
                        day = Console.ReadLine();
                        Console.Write("Месяц:");
                        month = Console.ReadLine();
                        Console.Write("Год: ");
                        year = Console.ReadLine();
                        if (int.Parse(day) < 0 || int.Parse(month) < 0 || int.Parse(year) < 0) throw new Exception("Неверный формат ввода даты");
                        break;
                    }
                    catch (Exception e) { Console.WriteLine($"Ошибка: {e.Message}"); }
                }
                timeFinish = new DateTime(Convert.ToInt16(year), Convert.ToInt16(month), Convert.ToInt16(day));
                try
                {
                    if (timeFinish <= timeStart) throw new Exception("Введена некорректная дата начала выполнения задачи");
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка: {e.Message}");
                    Console.WriteLine("Пожалуйста введите время завершения выполнения задачи заново");
                }
            }
            #endregion
            tasks.Add(new Task(taskName, taskDescription, timeStart, timeFinish, id));
            return 0;
        }
        public int DeleteTask(int id)
        {
            bool exist = false;
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
            string changeTaskName = default; string changeTaskDescr = default;
            string key = default;
            bool isEdit = false;
            string day, month, year;
            for (int i =0; i<tasks.Count;i++)
            {
                if (tasks[i].id == id)
                {
                    isEdit = true;
                    while (true)
                    {
                        Console.WriteLine("Выберите поле для редактирования");
                        Console.WriteLine("+++++++++++++++++++++++++++++");
                        Console.WriteLine("1 - Название задачи");
                        Console.WriteLine("2 - Описание задачи");
                        Console.WriteLine("3 - Дата старта и завершения задачи");
                        Console.WriteLine("0 - Выход из редактирования");
                        Console.WriteLine("+++++++++++++++++++++++++++++");
                        try
                        {
                            key = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(key)) throw new Exception("Некорректный ввод");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"{e.Message}");
                        }
                        switch (key)
                        {
                            case "1":
                                {
                                    Console.WriteLine("Пожалуйста введите новое имя задачи");
                                    changeTaskName = Console.ReadLine();
                                    try
                                    {
                                        if (string.IsNullOrWhiteSpace(changeTaskName) || changeTaskName.Length > 20) throw new Exception("Название не может быть пустым либо содержать больше 20 символов");
                                        tasks[i].taskName = changeTaskName;
                                        break;
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine($"Ошибка: {e.Message}");
                                        Console.WriteLine("Пожалуйста введите название задачи заново");
                                    }
                                }
                                break;
                            case "2":
                                {
                                    Console.WriteLine("Пожалуйста введите новое описание задачи");
                                    while (true)
                                    {
                                        changeTaskDescr = Console.ReadLine();
                                        try
                                        {
                                            if (string.IsNullOrWhiteSpace(changeTaskDescr) || changeTaskDescr.Length > 50) throw new Exception("Описание не может быть пустым либо содержать больше 50 символов");
                                            tasks[i].taskDescription = changeTaskDescr;
                                            break;
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine($"Ошибка: {e.Message}");
                                            Console.WriteLine("Пожалуйста введите описание задачи заново");
                                        }
                                    }
                                }
                                break;
                            case "3":
                                {
                                    Console.WriteLine("Введите пожалуйста время начала выполнения задачи {дд мм гггг}");
                                    while (true)
                                    {
                                        while (true)
                                        {
                                            try
                                            {
                                                Console.Write("День:");
                                                day = Console.ReadLine();
                                                Console.Write("Месяц:");
                                                month = Console.ReadLine();
                                                Console.Write("Год: ");
                                                year = Console.ReadLine();
                                                if (int.Parse(day) < 0 || int.Parse(month) < 0 || int.Parse(year) < 0) throw new Exception("Неверный формат ввода даты");
                                                break;
                                            }
                                            catch (Exception e) { Console.WriteLine($"Ошибка: {e.Message}"); }
                                        }
                                        tasks[i].timeStart = new DateTime(Convert.ToInt16(year), Convert.ToInt16(month), Convert.ToInt16(day));
                                        try
                                        {
                                            if ((tasks[i].timeStart < DateTime.Now)) throw new Exception("Введена некорректная дата начала выполнения задачи");
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
                                        while (true)
                                        {
                                            try
                                            {
                                                Console.Write("День:");
                                                day = Console.ReadLine();
                                                Console.Write("Месяц:");
                                                month = Console.ReadLine();
                                                Console.Write("Год: ");
                                                year = Console.ReadLine();
                                                if (int.Parse(day) < 0 || int.Parse(month) < 0 || int.Parse(year) < 0) throw new Exception("Неверный формат ввода даты");
                                                break;
                                            }
                                            catch (Exception e) { Console.WriteLine($"Ошибка: {e.Message}"); }
                                        }
                                        tasks[i].timeFinish = new DateTime(Convert.ToInt16(year), Convert.ToInt16(month), Convert.ToInt16(day));
                                        try
                                        {
                                            if (tasks[i].timeFinish <= tasks[i].timeStart) throw new Exception("Введена некорректная дата начала выполнения задачи");
                                            break;
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine($"Ошибка: {e.Message}");
                                            Console.WriteLine("Пожалуйста введите время завершения выполнения задачи заново");
                                        }
                                    }
                                }break;
                            case "0":return 0;
                            default: Console.WriteLine("Неверный формат ввода");break;
                        }
                    }
                }
            }
            if (isEdit == false) Console.WriteLine("Задачи с таким id не существует");
            return 0;
        }
    }
}
    
