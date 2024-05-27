using KandM_Clothes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KandM_Clothes.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ApplicationDbContext _dbContext = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MenuTop()
        {
            var items = _dbContext.Categories.OrderBy(x=>x.Position).ToList();
            return PartialView("_MenuTop",items);
        }

        public ActionResult MenuProductCategory()
        {
            var items = _dbContext.ProductCategories.ToList();
            return PartialView("_MenuProductCategory", items);
        }

        public ActionResult MenuArrivals()
        {
            var items = _dbContext.ProductCategories.ToList();
            return PartialView("_MenuArrivals", items);
        }
    }
}