using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMApp.Data.Entities
{
    public class WorkLog : Entity
    {
        public WorkLog()
        {
            WorkingPeriod = DateTime.UtcNow;
        }
        public DateTime WorkingPeriod { get; set; }
        public float WorkingHours { get; set; }
        public virtual ProjectTask ProjectTask { get; set; }
        public virtual User User { get; set; }
        public string UserId { get; set; }
        public string TaskId { get; set; }
    }
}
