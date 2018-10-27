namespace MyCVonline.Core.DTOs
{
    public class NewsDTO
    {
        public int NewsId { get; set; } //IN CASE THAT I WANT TO ADD A LINK IN THE NOTIFICATION

        public string Title { get; set; }

        public string Content { get; set; }

        public string Photo { get; set; }
    }
}