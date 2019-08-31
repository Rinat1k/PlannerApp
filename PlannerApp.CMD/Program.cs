using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlannerApp.BL;
namespace PlannerApp.CMD
{
    /// <summary>
    /// Регистрацию бы добавить тебе бро
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Планировщик";
            Task task = new Task();
            TaskController TManager = new TaskController();
            string listenerKey = default; int numEdit; 
            Console.WriteLine("Вас приветствует приложение планировщик!");
            while (true)
            {
                Console.WriteLine("Что хотите сделать?");
                Console.WriteLine("+++++++++++++++++++++++++++++");
                Console.WriteLine("1 - Просмотреть задачи");
                Console.WriteLine("2 - Добавить задачу");
                Console.WriteLine("3 - Удалить задачу");
                Console.WriteLine("4 - Редактировать задачу");
                Console.WriteLine("5 - Запись информации в файл");
                Console.WriteLine("6 - Загрузка информации из файла");
                Console.WriteLine("0 - Выход из программы");
                Console.WriteLine("+++++++++++++++++++++++++++++");
                listenerKey = (Console.ReadLine());
                try
                {  
                    if (string.IsNullOrWhiteSpace(listenerKey)) throw new Exception("Некорректный ввод");
                }
                catch(Exception e)
                {
                    Console.WriteLine($"{e.Message}");
                }
                switch (listenerKey)
                {
                    case "1":
                        {
                            if (TManager.tasks.Count == 0) Console.WriteLine("Список задач пуст!");
                            else foreach (Task t in TManager.tasks)
                                {
                                    Console.WriteLine(t.ToString());
                                }
                        }
                        break;
                    case "2": TManager.AddTask(); break;
                    case "3":
                        {
                            if (TManager.tasks.Count != 0)
                            {
                                Console.WriteLine("Введите id задачи, которую хотите удалить");
                                KeyValidation(out numEdit);
                                TManager.DeleteTask(numEdit);
                            }
                            else Console.WriteLine("Список задач пуст!");
                        }break;
                    case "4":
                        {
                            if (TManager.tasks.Count != 0)
                            {
                                Console.WriteLine("Введите id задачи, которую хотите редактировать");
                                KeyValidation(out numEdit);
                                TManager.EditTask(numEdit);
                            }
                            else Console.WriteLine("Список задач пуст!");
                        }break;
                    case "0":return;
                    default: Console.WriteLine("Неверный формат ввода");break;
                }
            }
        }
        private static void KeyValidation(out int numEdit)
        {
            string temp;
            while (true)
            {
                try
                {
                    temp = Console.ReadLine();
                    if (!int.TryParse(temp, out numEdit)) throw new Exception("Неверный формат ввода");
                    numEdit = Convert.ToInt32(temp);
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка: {e.Message}");
                    Console.WriteLine("Пожалуйста введите id заново");
                }
            }

        }
    }
}
