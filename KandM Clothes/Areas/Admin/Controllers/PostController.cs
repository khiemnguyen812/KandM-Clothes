﻿using KandM_Clothes.Models;
using KandM_Clothes.Models.EF;
using Microsoft.Ajax.Utilities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace KandM_Clothes.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
        ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Admin/Post
        public ActionResult Index(int? page, string searchTxt)
        {
            if (page == null || page < 1)
            {
                page = 1;
            }
            var pageSize = 5;
            var pageIndex = page.HasValue ? Convert.ToInt32(page.Value) : 1;
            var posts = _dbContext.Posts.AsQueryable();
            if (!string.IsNullOrEmpty(searchTxt))
            {
                posts = posts.Where(n => n.Title.Contains(searchTxt));
            }
            else
            {
                searchTxt = "";
            }
            var pagedPosts = posts.OrderByDescending(x => x.Id).ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            ViewBag.SearchTxt = searchTxt;
            return View(pagedPosts);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Post model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            model.CreatedDate = DateTime.Now;
            model.ModifiedDate = DateTime.Now;
            model.CategoryId = _dbContext.findCategory("Bài viết");
            _dbContext.Posts.Add(model);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var post = _dbContext.Posts.Find(id);
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _dbContext.Posts.Attach(model);
            model.ModifiedDate = DateTime.Now;
            _dbContext.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var model = _dbContext.Posts.Find(id);
            if (model == null)
            {
                return Json(new { success = false });
            }
            _dbContext.Posts.Remove(model);
            _dbContext.SaveChanges();
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var model = _dbContext.Posts.Find(id);
            if (model == null)
            {
                return Json(new { success = false });
            }
            model.isActive = !model.isActive;
            _dbContext.SaveChanges();
            return Json(new { success = true, active = model.isActive });
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
                var model = _dbContext.Posts.Find(id);
                if (model == null)
                {
                    return Json(new { success = false });
                }
                _dbContext.Posts.Remove(model);
            }
            _dbContext.SaveChanges();
            return Json(new { success = true });
        }
    }
}
