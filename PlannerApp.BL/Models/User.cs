using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerApp.BL.Models
{
    public class User
    {
        public string Name { get; set;}
        public User() { Name = default; }
        public User(string Name)
        {
            int Num;
            try
            {
                if (string.IsNullOrWhiteSpace(Name)) throw new Exception("Имя не может быть пустым или содержать больше 20 символов");
                if (int.TryParse(Name, out Num)) throw new Exception("Имя не должно содержать цифр");
                this.Name = Name;
            }
            catch(Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
                return;
            }
        }
    }
}
