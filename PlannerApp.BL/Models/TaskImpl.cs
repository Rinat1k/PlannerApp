using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
//Реализация методов класса Task
namespace PlannerApp.BL
{
    public partial class Task
    {
        public Task()
        {
            id = default;
            taskName = default;
            taskDescription = default;
            timeStart = default;
            timeFinish = default;
        }
        public Task(string taskName,
                    string taskDescription,
                    DateTime timeStart,
                    DateTime timeFinish, 
                    int id)
        {
            #region Валидация
            try
            {
                if (string.IsNullOrWhiteSpace(taskName)) throw new ArgumentException("Название не может быть пустым", nameof(taskName));
                if (string.IsNullOrWhiteSpace(taskDescription)) throw new ArgumentException("Описание не может быть пустым", nameof(taskDescription));
                if (timeStart <= DateTime.Now) throw new ArgumentException("Введена некорректная дата начала выполнения задачи", nameof(timeStart));
                if (timeFinish <= timeStart) throw new ArgumentException("Введена некорректная дата завершения выполнения задачи", nameof(timeFinish));
            }
            catch(ArgumentException e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
            #endregion
            this.id = id;
            this.taskName = taskName;
            this.taskDescription = taskDescription;
            this.timeStart = timeStart;
            this.timeFinish = timeFinish;
        }
        public override string ToString()
        {
            return $"Номер задачи: {id}\nНазвание задачи: {taskName}\nОписание задачи: {taskDescription}\n" +
                $"Дата старта: {timeStart}\nДата завершения: {timeFinish}";
        }
    }
}
