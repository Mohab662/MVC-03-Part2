using System.ComponentModel.DataAnnotations;

namespace MVC_03.PL.ViewModels
{
    public class ForgetPasswordViewModel
    {
		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress(ErrorMessage = "Email Is Not Valid")]
		public string Email { get; set; }
	}
}
