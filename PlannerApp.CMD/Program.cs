using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlannerApp.BL;
using PlannerApp.BL.Controllers;

namespace PlannerApp.CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Планировщик";
            TaskController TManager = new TaskController();
            string listenerKey = default; int numEdit;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Вас приветствует приложение планировщик!");
            Console.WriteLine("Нажмите на любую клавишу для продолжения работы...");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Что хотите сделать?");
                Console.WriteLine("+++++++++++++++++++++++++++++");
                Console.WriteLine("1 - Просмотреть задачи");
                Console.WriteLine("2 - Добавить задачу");
                Console.WriteLine("3 - Удалить задачу");
                Console.WriteLine("4 - Редактировать задачу");
                Console.WriteLine("5 - Отсортировать список по истечении срока");
                Console.WriteLine("6 - Сохранить задачи");
                Console.WriteLine("7 - Загрузить задачи");
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
                            Console.Clear();
                            if (TManager.tasks.Count == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Список задач пуст!");
                            }
                            else ShowTasks(TManager.tasks);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.ReadKey();
                            Console.Clear();
                        }
                        break;
                    case "2": Console.Clear(); TManager.AddTask(); break;
                    case "3":
                        {
                            Console.Clear();
                            if (TManager.tasks.Count != 0)
                            {
                                Console.WriteLine("Введите id задачи, которую хотите удалить");
                                KeyValidation(out numEdit);
                                Console.ForegroundColor = ConsoleColor.Red;
                                TManager.DeleteTask(numEdit);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Список задач пуст!");
                            }
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.ReadKey();
                            Console.Clear();
                        }
                        break;
                    case "4":
                        {
                            Console.Clear();
                            if (TManager.tasks.Count != 0)
                            {
                                Console.WriteLine("Введите id задачи, которую хотите редактировать");
                                KeyValidation(out numEdit);
                                TManager.EditTask(numEdit);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Список задач пуст!");
                            }
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Clear();
                        }
                        break;
                    case "5":
                        {
                            Console.Clear();
                            if (TManager.tasks.Count > 0)
                            {
                                TManager.tasks.Sort(0, TManager.tasks.Count, new TaskComparer());
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Список визуально отсортирован");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine("Список задач пуст!");
                            }
                            Console.ReadKey();
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        break;
                    case "6":
                        {
                            Console.Clear();
                            if (TManager.tasks.Count > 0)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                TManager.SaveToFile();
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Список задач пуст!\nДобавьте задачу прежде чем сохранить");
                            }
                            Console.ReadKey();
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        break;
                    case "7":
                        {
                            Console.Clear(); Console.ForegroundColor = ConsoleColor.DarkCyan;
                            TManager.LoadFromFile();
                            Console.ReadKey();
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.White; break;
                        }
                    case "0":
                        { if (ConfirmToExit()==true) return;}break;
                    default: Console.Clear(); Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Неверный формат ввода\nПожалуйста выберите функцию из списка");
                    Console.ForegroundColor = ConsoleColor.White; break;
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

        } //Корректность ввода ключа выбора
        private static bool ConfirmToExit()
        {
            string key = default;
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Вы действительно хотите выйти покинуть приложение?");
                Console.WriteLine("1 - Да"); Console.WriteLine("2 - Нет");
                key = Console.ReadLine();
                switch (key)
                {
                    case "1": return true;
                    case "2": Console.Clear(); Console.ForegroundColor = ConsoleColor.White; return false;
                }
            }
        }
        private static void ShowTasks(List<Task> tasks)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Красным цветом обозначены просроченные задачи!\n");
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++");
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].timeFinish < DateTime.Now) Console.ForegroundColor = ConsoleColor.Red;
                else Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(tasks[i].ToString()); Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
