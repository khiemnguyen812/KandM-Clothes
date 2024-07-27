using KandM_Clothes.Models;
using KandM_Clothes.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KandM_Clothes.Controllers
{
    public class SubscribeController : Controller
    {
        ApplicationDbContext _dbContext = new ApplicationDbContext();
		public ActionResult _Partial_Subscribe()
		{
			return PartialView("_Partial_Subscribe");
		}
		[HttpPost]
        public ActionResult Subscribe(string email)
        {
			var sub = _dbContext.Subscribes.FirstOrDefault(n => n.Email == email);
			if (sub != null)
			{
				return Json(new { success = false, message = "Email is already subscribed" });
			}
			_dbContext.Subscribes.Add(new Subscribe { Email = email, CreatedDate = DateTime.Now });
			_dbContext.SaveChanges();
			return Json(new { success = true, message = "Subscribed successfully" });
		}
    }
}