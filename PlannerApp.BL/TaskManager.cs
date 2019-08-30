using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerApp.BL
{
    public partial class TaskManager:ITaskController
    {
        List<Task> tasks { get; set; }
        public TaskManager(List<Task> tasks) => this.tasks = tasks ?? throw new ArgumentNullException("Список пустой", nameof(tasks));
        public TaskManager() { }
        int ITaskController.AddTask()
        {

            return 0;
        }
        int ITaskController.DeleteTask(int id) { return 0; }
        int ITaskController.EditRecord(int id) { return 0; }
    }
}
