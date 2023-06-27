using System.ComponentModel.DataAnnotations;

namespace jwt.Dto
{
    public class UserRegister
    {
        [Required]
        public string username { get; set; }


        [Required]
        [RegularExpression(@"[A-Za-z0-9._]+@gmail.com", ErrorMessage = "email is incorrect")]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}
