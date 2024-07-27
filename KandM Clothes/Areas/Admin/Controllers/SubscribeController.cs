using KandM_Clothes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KandM_Clothes.Areas.Admin.Controllers
{
    public class SubscribeController : Controller
    {
        // GET: Admin/Subscribe
        ApplicationDbContext _db_Context = new ApplicationDbContext();
		public ActionResult Index()
        {
			return View(_db_Context.Subscribes.ToList());
        }

    }
}