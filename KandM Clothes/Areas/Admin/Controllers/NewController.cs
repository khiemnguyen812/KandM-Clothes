using KandM_Clothes.Models;
using KandM_Clothes.Models.EF;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KandM_Clothes.Areas.Admin.Controllers
{
    public class NewController : Controller
    {
        ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Admin/New
        public ActionResult Index()
        {
            var news = _dbContext.News.ToList();
            return View(news);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(New model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            model.CreatedDate = DateTime.Now;
            model.ModifiedDate = DateTime.Now;
            model.CategoryId = _dbContext.findCategory("Tin tức");
            _dbContext.News.Add(model);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var news = _dbContext.News.Find(id);
            return View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(New model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _dbContext.News.Attach(model);
            model.ModifiedDate = DateTime.Now;
            _dbContext.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var model = _dbContext.News.Find(id);
            if (model == null)
            {
                return Json(new { success = false });
            }
            _dbContext.News.Remove(model);
            _dbContext.SaveChanges();
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var model = _dbContext.News.Find(id);
            if(model == null)
            {
                return Json(new { success = false });
            }
            model.isActive = !model.isActive;
            _dbContext.SaveChanges();
            return Json(new { success = true , active = model.isActive});
        }

        [HttpPost] 
        public ActionResult DeleteAll(List<int> ids)
        {
            if(ids == null)
            {
                return Json(new { success = false });
            }
            foreach (var id in ids)
            {
                var model = _dbContext.News.Find(id);
                if (model == null)
                {
                    return Json(new { success = false });
                }
                _dbContext.News.Remove(model);
            }
            _dbContext.SaveChanges();
            return Json(new { success = true });
        }
    }
}