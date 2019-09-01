using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlannerApp;
namespace PlannerApp.BL.Controllers
{
    public class TaskComparer:IComparer<Task>
    {
        int IComparer<Task>.Compare(Task tempTask1, Task tempTask2)
        {
            if (tempTask1.timeFinish.Subtract(tempTask1.timeStart) < tempTask2.timeFinish.Subtract(tempTask2.timeStart))
                return -1;
            else if (tempTask1.timeFinish.Subtract(tempTask1.timeStart) == tempTask2.timeFinish.Subtract(tempTask2.timeStart))
                return 0;
            else return 1;
        }
    }
}
