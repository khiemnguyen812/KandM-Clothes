using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KandM_Clothes.Models; // Make sure to include the namespace for your models and DbContext
using System.Data.Entity; // Required for EF functionality
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
namespace KandM_Clothes.Areas.Admin.Controllers
{
    public class RoleController : Controller
    {
		public ApplicationDbContext db = new ApplicationDbContext();
		public ActionResult Index()
		{
			var items = db.Roles.ToList();
			return View(items);
		}


		[HttpPost]
		public ActionResult Create(string roleName) 
		{
			if (ModelState.IsValid)
			{
				var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

				// Check if the role already exists
				if (!roleManager.RoleExists(roleName))
				{
					var result = roleManager.Create(new IdentityRole(roleName));
					if (result.Succeeded)
					{
						return RedirectToAction("Index");
					}
					else
					{
						// Handle the error scenario, add errors to ModelState
						foreach (var error in result.Errors)
						{
							ModelState.AddModelError("", error);
						}
					}
				}
				else
				{
					ModelState.AddModelError("", "Role already exists.");
				}
			}

			return View();
		}

		[HttpPost]
		public ActionResult Delete(string id)
		{
			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
			var role = roleManager.FindById(id.ToString());
			if (role != null)
			{
				var result = roleManager.Delete(role);
				if (result.Succeeded)
				{
					return Json(new { success = true });
				}
				else
				{
					// Handle the error scenario, add errors to ModelState
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError("", error);
					}
				}
			}
			else
			{
				ModelState.AddModelError("", "Role not found.");
			}

			return View();
		}

		public ActionResult Edit(string id, string roleName)
		{
			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
			var role = roleManager.FindById(id.ToString());
			if (role != null)
			{
				role.Name = roleName;
				var result = roleManager.Update(role);
				if (result.Succeeded)
				{
					return Json(new { success = true });
				}
				else
				{
					// Handle the error scenario, add errors to ModelState
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError("", error);
					}
				}
			}
			else
			{
				ModelState.AddModelError("", "Role not found.");
			}
			return Json(new { success = false });

		}

	}
}