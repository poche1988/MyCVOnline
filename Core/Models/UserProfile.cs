using System.ComponentModel.DataAnnotations;

namespace MyCVonline.Core.Models
{
    public class UserProfile
    {
        [Key]
        public int UserProfileId { get; set; }
        public string Summary { get; set; }

        [MaxLength(100)]
        public string Profession { get; set; }
        public string UserId { get; set; }

        public UserProfile()
        {
            // default constructor
        }
        public UserProfile(string userid, string summary, string proffession)
        {
            this.UserId = userid;
            this.Summary = summary;
            this.Profession = proffession;
        }
    }
}