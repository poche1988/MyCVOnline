using System;
using System.ComponentModel.DataAnnotations;

namespace MyCVonline.Core.Models
{
    public class Qualification
    {
        public int QualificationId { get; set; }

        
        public ApplicationUser User { get; set; }

        [Required]
        public string UserId { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        [Required]
        [MaxLength(100)]
        public string QualificationTitle { get; set; }

        [Required]
        public string University { get; set; }

        public QualificationLevel level { get; set; }

        [Required]
        public int QualificationLevelId { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public bool Highlighted { get; set; }
    }
}