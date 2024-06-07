using KandM_Clothes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace KandM_Clothes.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Refesh()
        {
            var item = new StatisticViewModel();
            ViewBag.VisitorsOnline = HttpContext.Application["VisitorOnline"];
            item.Today = int.Parse(HttpContext.Application["Today"]?.ToString() ?? "0");
            item.Yesterday = int.Parse(HttpContext.Application["Yesterday"]?.ToString() ?? "0");
            item.Week = int.Parse(HttpContext.Application["Week"]?.ToString() ?? "0");
            item.LastWeek = int.Parse(HttpContext.Application["LastWeek"]?.ToString() ?? "0");
            item.Month = int.Parse(HttpContext.Application["Month"]?.ToString() ?? "0");
            item.LastMonth = int.Parse(HttpContext.Application["LastMonth"]?.ToString() ?? "0");
            item.TotalAccess = int.Parse(HttpContext.Application["TotalAccess"]?.ToString() ?? "0");
            return PartialView(item);
        }
    }
}