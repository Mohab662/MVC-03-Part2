using System.ComponentModel.DataAnnotations;

namespace MVC_03.PL.ViewModels
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage ="First Name Is Required")]
        public string FName { get; set; }

		[Required(ErrorMessage = "Last Name Is Required")]
		public string LName { get; set; }

		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress(ErrorMessage ="Email Is Not Valid")]
		public string Email { get; set; }

		[Required(ErrorMessage ="Password Is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirm Password Is Required")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password),ErrorMessage = "Confirm Password Is't Match Password")]
		public string ConfirmPassword { get; set; }

		[Required(ErrorMessage ="Required To Agree")]
		public bool IsAgree { get; set; }
	}
}
