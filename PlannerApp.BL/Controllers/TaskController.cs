using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;
namespace PlannerApp.BL
{
    public partial class TaskController : ITaskController
    {
        private const string FILE_NAME = "SaveTasks.bin";
        private const int NAME_MAX_LENGHT = 35;
        private const int DESC_MAX_LENGHT = 120;
        public List<Task> tasks { get; set; }
        public TaskController(List<Task> tasks) => this.tasks = tasks ?? throw new ArgumentNullException("Список задач пуст!", nameof(tasks));
        public TaskController()
        {
            tasks = new List<Task>();
        }
        public int AddTask()
        {
            int id = default;
            string taskName, taskDescription;
            taskName = taskDescription = default;
            DateTime timeStart, timeFinish;
            timeStart = timeFinish = default;
            Console.WriteLine("Добавление задачи...");
            Console.WriteLine("Введите id номер задачи");
            string input;
            #region Ввод данных
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
            taskName = NameIsEnter(NAME_MAX_LENGHT);
            Console.WriteLine("Введите пожалуйста описание задачи");
            taskDescription = NameIsEnter(DESC_MAX_LENGHT);
            Console.WriteLine("Введите пожалуйста время начала выполнения задачи {дд мм гггг}");
            timeStart = DateStartIsEnter(timeFinish);
            Console.WriteLine("Введите пожалуйста время завершения выполнения задачи {дд мм гггг}");
            timeFinish = DateFinishIsEnter(timeStart);
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
        public int EditTask(int id)
        {
            string key = default;
            bool isEdit = false;
            for (int i =0; i<tasks.Count;i++)
            {
                if (tasks[i].id == id)
                {
                    isEdit = true;
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("Выберите поле для редактирования");
                        Console.WriteLine("+++++++++++++++++++++++++++++");
                        Console.WriteLine("1 - Название задачи");
                        Console.WriteLine("2 - Описание задачи");
                        Console.WriteLine("3 - Дата старта задачи");
                        Console.WriteLine("4 - Дата завершения задачи");
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
                                    tasks[i].taskName = NameIsEnter(NAME_MAX_LENGHT);
                                    Console.WriteLine("Имя отредактировано");
                                    Console.ReadKey();
                                }
                                break;
                            case "2":
                                {
                                    Console.WriteLine("Пожалуйста введите новое описание задачи");
                                    tasks[i].taskDescription = NameIsEnter(DESC_MAX_LENGHT);
                                    Console.WriteLine("Описание отредактировано");
                                    Console.ReadKey();
                                }
                                break;
                            case "3":
                                {
                                    Console.WriteLine("Введите пожалуйста новое время начала выполнения задачи {дд мм гггг}");
                                    tasks[i].timeStart = DateStartIsEnter(tasks[i].timeFinish,true);
                                    Console.WriteLine("Время начала выполнения отредактировано");
                                    Console.ReadKey();
                                }
                                break;
                            case "4":
                                {
                                    Console.WriteLine("Введите пожалуйста новое время завершения выполнения задачи {дд мм гггг}");
                                    tasks[i].timeFinish = DateFinishIsEnter(tasks[i].timeStart);
                                    Console.WriteLine("Время завершения выполнения отредактировано");
                                    Console.ReadKey();
                                }
                                break;
                            case "0":return 0;
                            default: Console.WriteLine("Неверный формат ввода"); Console.ReadKey(); break;
                        }
                    }
                }
            }
            if (isEdit == false) Console.WriteLine("Задачи с таким id не существует");
            return 0;
        }
        public int SortTask() { return 0; }
        public bool SaveToFile()
        {
            var binaryFormatter = new BinaryFormatter();
            using (var file = new FileStream(FILE_NAME, FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(file, tasks);
                Console.WriteLine("Список задач сохранён");
            }
            return true;
        }
        public void LoadFromFile()
        {
            var binaryFormatter = new BinaryFormatter();
            using (var file = new FileStream(FILE_NAME, FileMode.OpenOrCreate))
            {
                try
                {
                    tasks = binaryFormatter.Deserialize(file) as List<Task> ?? throw new Exception("Нет сохранененных задач!");
                    Console.WriteLine("Список задач загружен");
                }
                catch (Exception e) { Console.WriteLine($"Ошибка: {e.Message}"); }
            }
            return;
        }
        #region Методы ввода и валидации модели
        private string NameIsEnter(int maxLenght)
        {
            string changeTaskName;
            while (true)
            {
                changeTaskName = Console.ReadLine();
                try
                {
                    if (string.IsNullOrWhiteSpace(changeTaskName) || changeTaskName.Length > maxLenght) throw new Exception($"Поле не может быть пустым либо содержать больше {maxLenght} символов");
                    return changeTaskName;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка: {e.Message}");
                    Console.WriteLine("Пожалуйста введите поле заново");
                }
            }
        } 
        private DateTime DateStartIsEnter(DateTime dtFinish,bool compare = false)
        {
            string sec,hour,minute,day, year, month; day = year = month = default;
            DateTime dt;
            while (true)
            {
                try
                {
                    Console.Write("День:"); day = Console.ReadLine();
                    Console.Write("Месяц:"); month = Console.ReadLine();
                    Console.Write("Год:"); year = Console.ReadLine();
                    Console.Write("Часы:"); hour = Console.ReadLine();
                    Console.Write("Минуты:"); minute = Console.ReadLine();
                    Console.Write("Секунды:"); sec = Console.ReadLine();
                    dt = new DateTime(Convert.ToInt16(year), Convert.ToInt16(month), 
                                      Convert.ToInt16(day),Convert.ToInt16(hour),
                                      Convert.ToInt16(minute),Convert.ToInt16(sec));
                    if (int.Parse(day) < 0 || int.Parse(month) < 0 || int.Parse(year) < 0 || dt <= DateTime.Now) throw new Exception("Введена некорректная дата начала выполнения задачи");
                    if (compare) if (dt > dtFinish) throw new Exception("Введена некорректная дата начала выполнения задачи");

                    return dt;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка: {e.Message}");
                    Console.WriteLine("Пожалуйста введите время начала выполнения задачи заново");
                }
            }
        }
        private DateTime DateFinishIsEnter(DateTime dtStart)
        {
            string hour,minute,sec,day, year, month; day = year = month = default;
            DateTime dt;
            while (true)
            {
                try
                {
                    Console.Write("День:"); day = Console.ReadLine();
                    Console.Write("Месяц:"); month = Console.ReadLine();
                    Console.Write("Год:"); year = Console.ReadLine();
                    Console.Write("Часы:"); hour = Console.ReadLine();
                    Console.Write("Минуты:"); minute = Console.ReadLine();
                    Console.Write("Секунды:"); sec = Console.ReadLine();
                    dt = new DateTime(Convert.ToInt16(year), Convert.ToInt16(month),
                                      Convert.ToInt16(day), Convert.ToInt16(hour),
                                      Convert.ToInt16(minute), Convert.ToInt16(sec));
                    if (int.Parse(day) < 0 || int.Parse(month) < 0 || int.Parse(year) < 0 || dt <= dtStart)
                        throw new Exception("Введена некорректная дата завершения выполнения задачи");
                    return dt;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка: {e.Message}");
                    Console.WriteLine("Пожалуйста введите время завершения выполнения задачи заново");
                }
            }
        }
        #endregion
    }
}
    
