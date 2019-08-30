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
            if (string.IsNullOrWhiteSpace(taskName)) throw new ArgumentNullException("Название не может быть пустым", nameof(taskName));
            if (string.IsNullOrWhiteSpace(taskDescription)) throw new ArgumentNullException("Описание не может быть пустым", nameof(taskDescription));
            if (timeStart < DateTime.Now) throw new ArgumentException("Введена некорректная дата начала выполнения задачи", nameof(timeStart));
            if (timeFinish <= timeStart) throw new ArgumentException("Введена некорректная дата завершения выполнения задачи",nameof(timeFinish));
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
                $"Дата старта: {timeStart.Day}.{timeStart.Month}.{timeStart.Year}\nДата завершения: {timeFinish.Day}.{timeFinish.Month}.{timeFinish.Year}";
        }
    }
}
