using System;

namespace MyCVonline.Core.DTOs
{
    public class NotificationDTO
    {
       public DateTime Date { get; set; }

        public String Message { get; set; }
        public String Link { get; set; }

        public NewsDTO News { get; set; }
    }
}