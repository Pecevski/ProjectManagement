using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMApp.Data.Entities
{
    public class Project : Entity 
    {
        public Project() : base()
        {
            ProjectTasks = new List<ProjectTask>();
            Teams = new List<Team>();
        }
        public string Title { get; set; }
        public virtual User Owner { get; set; }
        public virtual List<ProjectTask> ProjectTasks { get; set; }
        public virtual List<Team> Teams { get; set; }
        public string OwnerId { get; set; }
    }
}
