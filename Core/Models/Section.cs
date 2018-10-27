using System.ComponentModel.DataAnnotations;

namespace MyCVonline.Core.Models
{
    public class Section
    {
        public int SectionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string UserId { get; set; }
        public TemplatePosition Position { get; set; }
    }
}