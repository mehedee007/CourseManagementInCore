using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace CourseManagementInCore.Models.ViewModels
{
    public class TrainerViewModelBase
    {
        [Key]
        public int TrainerID { get; set; }
        public string TrainerName { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string TrainerContact { get; set; }
        public string TrainerEmail { get; set; }

        public bool IsActive { get; set; }
        public string TrainerImage { get; set; }
        [NotMapped]
        public IFormFile ImageUrl { get; set; }

        public int CourseID { get; set; }
        public virtual Course Course { get; set; }

        public virtual IList<TrainerExperience> TrainerExperiences { get; set; } = new List<TrainerExperience>();
    }
}
