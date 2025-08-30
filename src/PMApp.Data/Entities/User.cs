using Microsoft.AspNetCore.Identity;
using PMApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMApp.Data.Entities
{
    public class User : IdentityUser
    {
        public User() : base()
        {
            CreatedProjects = new List<Project>();
            ProjectTasks = new List<ProjectTask>();
            CreatedTasks = new List<ProjectTask>();
            Teams = new List<Team>();
            CreatedTeams = new List<Team>();
            WorkLogs = new List<WorkLog>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual List<Project> CreatedProjects { get; set; }
        public virtual List<Team> Teams { get; set; }
        public virtual List<Team> CreatedTeams { get; set; }
        public virtual List<ProjectTask> ProjectTasks { get; set; }
        public virtual List<ProjectTask> CreatedTasks { get; set; }
        public virtual List<WorkLog> WorkLogs { get; set; }
    }
}
