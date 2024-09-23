using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_03.DAL.Models;
using MVC_03.PL.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_03.PL.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public async Task<IActionResult> Index(string email)
		{
			if (string.IsNullOrEmpty(email))
			{
				var user =await _userManager.Users.Select(U => new UserViewModel()
				{
					Id=U.Id,
					FName=U.FName,
					LName=U.LName,
					Email=U.Email,
					PhoneNumber= U.PhoneNumber,
					Roles=_userManager.GetRolesAsync(U).Result	
				}).ToListAsync();
			}
            else
            {
				var user = await _userManager.FindByEmailAsync(email);
                if (user!=null)
                {
					var mapedUser = new UserViewModel
					{
						Id = user.Id,
						FName = user.FName,
						LName = user.LName,
						Email = user.Email,
						PhoneNumber = user.PhoneNumber,
						Roles = _userManager.GetRolesAsync(user).Result
					};
					return View(new List<UserViewModel> { mapedUser });
				}
            }
            return View(Enumerable.Empty<UserViewModel>());

		}
	}
}
