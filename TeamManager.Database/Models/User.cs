using System.ComponentModel.DataAnnotations;

namespace TeamManager.Database.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        // Department One-One
        public virtual Department Department { get; set; }
    }
}
