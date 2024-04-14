using System.ComponentModel.DataAnnotations;

namespace impar_back_end.Models.User.Entity
{
    public class User
    {
        [Key]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
