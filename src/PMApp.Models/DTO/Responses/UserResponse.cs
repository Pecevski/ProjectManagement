using PMApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMApp.Models.DTO.Responses
{
    public class UserResponse
    {
        public UserResponse()
        {
            Teams = new List<TeamResponse>();
            CreatedProjects = new List<ProjectResponse>();
            ProjectTasks = new List<ProjectTaskResponse>();
            WorkLogs = new List<WorkLogResponse>();

        }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<ProjectResponse> CreatedProjects { get; set; }
        public List<TeamResponse> Teams { get; set; }
        public List<ProjectTaskResponse> ProjectTasks { get; set; }
        public List<WorkLogResponse> WorkLogs { get; set; }
    }
}
