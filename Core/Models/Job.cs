using System;
using System.ComponentModel.DataAnnotations;

namespace MyCVonline.Core.Models
{
    public class Job
    {
        public int JobId { get; set; }

        
        public ApplicationUser User { get; set; }

       
        public bool OnGoing { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        [Required]
        public string JobTitle { get; set; }

        [Required]
        public string JobDescription { get; set; }

        [Required]
        public string CompanyName { get; set; }

        public string Reference { get; set; }

        [Required]
        public bool Active { get; set; }


    }
}