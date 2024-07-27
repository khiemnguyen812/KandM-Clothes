using KandM_Clothes.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KandM_Clothes.Areas.Admin.Controllers
{
	public class AccountController : Controller
	{
		private ApplicationSignInManager _signInManager;
		private ApplicationUserManager _userManager;
		private ApplicationDbContext _dbContext = new ApplicationDbContext();
		private IAuthenticationManager AuthenticationManager
		{
			get
			{
				return HttpContext.GetOwinContext().Authentication;
			}
		}
		public AccountController()
		{
		}

		public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
		{
			UserManager = userManager;
			SignInManager = signInManager;
		}

		public ApplicationSignInManager SignInManager
		{
			get
			{
				return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
			}
			private set
			{
				_signInManager = value;
			}
		}

		public ApplicationUserManager UserManager
		{
			get
			{
				return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			private set
			{
				_userManager = value;
			}
		}

		[AllowAnonymous]
		public ActionResult Register()
		{
			ViewBag.Roles = new SelectList(_dbContext.Roles.ToList(), "Id", "Name");
			return View();
		}

		//
		// POST: /Account/Register
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FullName = model.FullName, Phone = model.Phone, RoleId = model.Role };
				var result = await UserManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					var roleId = model.Role; // Assuming model.Role is a string that contains the role ID
					var role = _dbContext.Roles.FirstOrDefault(r => r.Id == roleId);
					if (role == null)
					{
						// Handle the case where the role does not exist
						ModelState.AddModelError("", $"Role with ID {roleId} does not exist.");
						ViewBag.Roles = new SelectList(_dbContext.Roles.ToList(), "Id", "Name");
						return View(model);
					}

					// Add user to role
					UserManager.AddToRole(user.Id, role.Name);
					/*await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);*/

					// Redirect or other actions
					return RedirectToAction("Index", "Account");
				}
				AddErrors(result);
			}
			ViewBag.Roles = new SelectList(_dbContext.Roles.ToList(), "Id", "Name");
			// If we got this far, something failed, redisplay form
			return View(model);
		}



		// GET: Admin/Account
		public ActionResult Index()
		{
			var items = _dbContext.Users.ToList();
			return View(items);
		}

		private void AddErrors(IdentityResult result)
		{
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError("", error);
			}
		}

		// GET: /Account/Login
		[AllowAnonymous]
		public ActionResult Login(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			ViewBag.ResultLogin = null;
			return View();
		}

		//
		// POST: /Account/Login
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
		{
			ViewBag.ResultLogin = false;
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			// This doesn't count login failures towards account lockout
			// To enable password failures to trigger account lockout, change to shouldLockout: true
			var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
			switch (result)
			{
				case SignInStatus.Success:
					ViewBag.ResultLogin = true;
					return RedirectToLocal(returnUrl);
				case SignInStatus.LockedOut:
					return View("Lockout");
				case SignInStatus.RequiresVerification:
					return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
				case SignInStatus.Failure:
				default:
					ModelState.AddModelError("", "Invalid login attempt.");
					return View(model);
			}
		}
		private ActionResult RedirectToLocal(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			return RedirectToAction("Index", "Home");
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult LogOff()
		{
			AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
			return RedirectToAction("Index", "Home");
		}

		public ActionResult Edit(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ApplicationUser user = _dbContext.Users.Find(id);
			if (user == null)
			{
				return HttpNotFound();
			}

			// Lấy ID role hiện tại của user
			var userRoleId = user.Roles.FirstOrDefault()?.RoleId; // Giả sử mỗi user chỉ có một role

			// Tạo SelectList với role hiện tại được chọn
			ViewBag.Roles = new SelectList(_dbContext.Roles.ToList(), "Id", "Name", userRoleId);

			return View(user);
		}

		[HttpPost]
		// POST: Admin/Account/Edit
		public ActionResult Edit(ApplicationUser user)
		{
			if (ModelState.IsValid)
			{
				// Lấy user cũ
				var userOld = _dbContext.Users.Find(user.Id);

				// Cập nhật thông tin mới
				userOld.FullName = user.FullName;
				userOld.Phone = user.Phone;
				userOld.Email = user.Email;
				userOld.RoleId = user.RoleId;

				UserManager.RemoveFromRole(user.Id, _dbContext.Roles.Find(userOld.RoleId).Name);

				UserManager.AddToRole(user.Id, _dbContext.Roles.Find(user.RoleId).Name);

				_dbContext.SaveChanges();
				ViewBag.ResultEdit = true;
				return RedirectToAction("Index", "Account");
			}
			ViewBag.Roles = new SelectList(_dbContext.Roles.ToList(), "Id", "Name");
			return RedirectToAction("Index", "Account");
		}

		[HttpPost]
		public ActionResult Delete(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ApplicationUser user = _dbContext.Users.Find(id);
			if (user == null)
			{
				return HttpNotFound();
			}
			_dbContext.Users.Remove(user);
			_dbContext.SaveChanges();
			return Json(new { success = true });
		}
	}
}