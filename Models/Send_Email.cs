using System.ComponentModel.DataAnnotations;

namespace portfolioApp.Models
{
    public class Send_Email
    {
        public string? name { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string? email { get; set; }

        public string? subject { get; set; }
        public string? message { get; set; }


    }
}
