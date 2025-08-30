using PMApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMApp.Data.Entities
{
    public class ProjectTask : Entity
    {
        public ProjectTask() : base()
        {
            WorkLogs = new List<WorkLog>();
        }
        public string Title { get; set; }
        public Status Status { get; set; }
        public virtual Project Project { get; set; }
        public virtual User TaskCreator { get; set; }
        public virtual User AssignedUser { get; set; }
        public virtual List<WorkLog> WorkLogs { get; set; }
        public string ProjectId { get; set; }
        public string TaskCreatorId { get; set; }
        public string AssignedUserId { get; set; }
    }
}
