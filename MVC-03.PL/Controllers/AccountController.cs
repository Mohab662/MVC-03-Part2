using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_03.DAL.Models;
using MVC_03.PL.Helpers;
using MVC_03.PL.ViewModels;
using System.Threading.Tasks;

namespace MVC_03.PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		#region SignUp
		public IActionResult SignUp()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				var User = new ApplicationUser()
				{
					UserName = viewModel.Email.Split("@")[0],
					Email = viewModel.Email,
					IsAgree = viewModel.IsAgree,
					FName = viewModel.FName,
					LName = viewModel.LName
				};
				var result = await _userManager.CreateAsync(User, viewModel.Password);
				if (result.Succeeded)
				{
					return RedirectToAction(nameof(SignIn));
				}
				foreach (var Error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, Error.Description);
				};
			}
			return View(viewModel);

		}
		#endregion

		#region SignIn
		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		// [Authorize]
		public async Task<IActionResult> SignIn(SignInViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(viewModel.Email);
				if (user is not null)
				{
					bool flag = await _userManager.CheckPasswordAsync(user, viewModel.Password);
					if (flag)
					{
						var result = await _signInManager.PasswordSignInAsync(user, viewModel.Password, viewModel.RememberMe, false);
						if (result.Succeeded)
						{
							return RedirectToAction(nameof(HomeController.Index), "Home");
						}
					}
				}
				ModelState.AddModelError(string.Empty, "Invalid Data");
			}
			return View(viewModel);
		}

		#endregion

		#region SignOut
		public async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(SignIn));
		}



		#endregion

		#region ForgetPassword
		public IActionResult ForgetPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(viewModel.Email);
				if (user is not null)
				{
					var token = _userManager.GeneratePasswordResetTokenAsync(user);
					var resetPassURL = Url.Action("ResetPassword", "Account", new { email = viewModel.Email ,token});
					var email = new Email()
					{
						Subject = "Reset Your Password",
						Body =resetPassURL,
						Receipints =viewModel.Email
					};
					EmailSettings.SendEmail(email);
					RedirectToAction(nameof(CheckInbox));
				}
				ModelState.AddModelError(string.Empty, "Invalid Eamil");
			}
			return View(viewModel);
		}

		#endregion

		public IActionResult CheckInbox() 
		{
			return View();
		}

		public IActionResult ResetPassword(string email , string token)
		{
			TempData["email"] = email;
			TempData["token"] = token;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel viewModel)
		{
            if (ModelState.IsValid)
            {
				var email = TempData["email"] as string;
				var token = TempData["token"] as string;
				var user=await _userManager.FindByEmailAsync(email);
				var result = await _userManager.ResetPasswordAsync(user, token, viewModel.Password);
                if (result.Succeeded)
                {
					return RedirectToAction(nameof(SignIn));
                }
				foreach (var Error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, Error.Description);
				}
			}
			return View(viewModel);
        }
	}
}
