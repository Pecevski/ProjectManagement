using PMApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMApp.Models.DTO.Responses
{
    public class ProjectTaskResponse
    {
        public ProjectTaskResponse()
        {
            WorkLogs = new List<WorkLogResponse>();
        }
        public string Id { get; set; }
        public string Title { get; set; }
        public Status Status { get; set; }
        public ProjectResponse Project { get; set; }
        public UserResponse User { get; set; }
        public List<WorkLogResponse> WorkLogs { get; set; }
    }
}
