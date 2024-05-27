using KandM_Clothes.Models;
using KandM_Clothes.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KandM_Clothes.Models.Common;
using System.Web.UI.WebControls;


namespace KandM_Clothes.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Admin/Category

        ApplicationDbContext _dbContext = new ApplicationDbContext();

        public ActionResult Index()
        {
            var categories = _dbContext.Categories.OrderBy(c => c.Position).ToList();
            return View(categories);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            category.CreatedDate = DateTime.Now;
            category.ModifiedDate = DateTime.Now;
            category.Alias = KandM_Clothes.Models.Common.Filter.FilterChar(category.Title);
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var category = _dbContext.Categories.Find(id);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _dbContext.Categories.Attach(category);
            category.Alias = KandM_Clothes.Models.Common.Filter.FilterChar(category.Title);
            category.ModifiedDate = DateTime.Now;
            _dbContext.Entry(category).Property(x => x.Title).IsModified = true;
            _dbContext.Entry(category).Property(x => x.Description).IsModified = true;
            _dbContext.Entry(category).Property(x => x.Position).IsModified = true;
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category == null)
            {
                return Json(new { success = false });
            }
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            return Json(new { success = true });
        }
    }

}