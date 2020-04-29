using System.ComponentModel.DataAnnotations;

namespace KrisG.TimeTracker.Models.Users
{
    public class RegisterModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}