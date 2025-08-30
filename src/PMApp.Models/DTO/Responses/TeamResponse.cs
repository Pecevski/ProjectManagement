using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMApp.Models.DTO.Responses
{
    public class TeamResponse
    {
        public TeamResponse()
        {
            TeamMembers = new List<UserResponse>();
        }
        public string Id { get; set; }
        public string TeamName { get; set; }
        public ProjectResponse Project { get; set; }
        public List<UserResponse> TeamMembers { get; set; }
    }
}
