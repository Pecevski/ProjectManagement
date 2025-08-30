using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMApp.Models.DTO.Responses
{
    public class ProjectResponse
    {
        public ProjectResponse()
        {
            Teams = new List<TeamResponse>();
            ProjectTasks = new List<ProjectTaskResponse>();
        }
        public string Id { get; set; }
        public string Title { get; set; }
        public UserResponse Owner { get; set; }
        public List<ProjectTaskResponse> ProjectTasks { get; set; }
        public List<TeamResponse> Teams { get; set; }

    }
}
