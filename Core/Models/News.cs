using System;
using System.ComponentModel.DataAnnotations;

namespace MyCVonline.Core.Models
{
    public class News
    {
        public int NewsId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public  DateTime Date { get; set; }

        public string Photo { get; set; }

        public bool IsCanceled { get; set; }

       
    }
}