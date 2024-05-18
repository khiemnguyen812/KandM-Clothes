using KandM_Clothes.Models;
using KandM_Clothes.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KandM_Clothes.Areas.Admin.Controllers
{
    public class ProductCategoryController : Controller
    {
        // GET: Admin/ProductCategory
        ApplicationDbContext _dbContext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var productCategories = _dbContext.ProductCategories.ToList();
            return View(productCategories);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProductCategory model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            model.CreatedDate = DateTime.Now;
            model.ModifiedDate = DateTime.Now;
            _dbContext.ProductCategories.Add(model);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var productCategory = _dbContext.ProductCategories.Find(id);
            return View(productCategory);
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _dbContext.ProductCategories.Attach(model);
            model.ModifiedDate = DateTime.Now;
            _dbContext.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var model = _dbContext.ProductCategories.Find(id);
            if (model == null)
            {
                return Json(new { success = false });
            }
            _dbContext.ProductCategories.Remove(model);
            _dbContext.SaveChanges();
            return Json(new { success = true });
        }
        [HttpPost]
        public ActionResult DeleteAll(List<int> ids)
        {
            if (ids == null)
            {
                return Json(new { success = false });
            }
            foreach (var id in ids)
            {
                var model = _dbContext.ProductCategories.Find(id);
                if (model == null)
                {
                    return Json(new { success = false });
                }
                _dbContext.ProductCategories.Remove(model);
            }
            _dbContext.SaveChanges();
            return Json(new { success = true });
        }
    }
}