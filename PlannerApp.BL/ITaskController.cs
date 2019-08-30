using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PlannerApp.BL
{
    interface ITaskController
    {
        int DeleteTask(int id);
        int AddTask();
        int EditRecord(int id);
    }
}
