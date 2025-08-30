using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMApp.Models.DTO.Requests
{
    public class TeamRequest
    {
        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string TeamName { get; set; }
    }
}
