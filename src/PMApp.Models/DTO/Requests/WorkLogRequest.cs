using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMApp.Models.DTO.Requests
{
    public class WorkLogRequest
    {
        [Required]
        [Range(0.5, 24)]
        public float WorkingHours { get; set; }
        [Required]
        public DateTime WorkingPeriod { get; set; }
    }
}
