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
        public int id { get; set; }
        public string taskName { get; set; }
        public string taskDescription { get; set; }
        public DateTime timeStart { get; set; }
        public DateTime timeFinish { get; set; }
    }
}
