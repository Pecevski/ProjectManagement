using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMApp.Data.Entities
{
    public class Team : Entity
    {

        public Team() : base()
        {
            TeamMembers = new List<User>();
        }
        public string TeamName { get; set; }
        public virtual User TeamCreator { get; set; }
        public virtual List<User> TeamMembers { get; set; }
        public virtual Project AssignedProject { get; set; }
        public string ProjectId { get; set; }
        public string TeamCreatorId { get; set; }
    }
}
