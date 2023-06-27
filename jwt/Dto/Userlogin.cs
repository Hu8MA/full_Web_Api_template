using System.ComponentModel.DataAnnotations;

namespace jwt.Dto
{
    public class Userlogin
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
