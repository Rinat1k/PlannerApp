using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerApp.BL
{
    [Serializable]
    public partial class Task
    {
        int id { get; set; }
        string taskName { get; set; }
        string taskDescription { get; set; }
        DateTime timeStart { get; set; }
        DateTime timeFinish { get; set; }
    }
}
