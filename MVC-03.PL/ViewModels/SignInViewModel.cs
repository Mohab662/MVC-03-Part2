using System.ComponentModel.DataAnnotations;

namespace MVC_03.PL.ViewModels
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Email Is Not Valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Required To Agree")]
        public bool RememberMe { get; set; }
    }
}
