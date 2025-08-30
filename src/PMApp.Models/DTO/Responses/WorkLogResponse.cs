using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMApp.Models.DTO.Responses
{
    public class WorkLogResponse
    {
        public WorkLogResponse()
        {
            WorkingPeriod = DateTime.UtcNow;
        }
        public string Id { get; set; }
        public DateTime WorkingPeriod { get; set; }
        public float WorkingHours { get; set; }
        public ProjectTaskResponse ProjectTask { get; set; }
        public UserResponse User { get; set; }
    }
}
