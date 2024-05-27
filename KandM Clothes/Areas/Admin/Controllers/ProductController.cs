using KandM_Clothes.Models;
using KandM_Clothes.Models.EF;
using Microsoft.Ajax.Utilities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using System.Data.Entity;


namespace KandM_Clothes.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        ApplicationDbContext _dbContext = new ApplicationDbContext();
        public ActionResult Index(int? page, string searchTxt, int? categoryFilter)
        {
            if (page == null || page < 1)
            {
                page = 1;
            }

            var pageSize = 5;
            var pageIndex = page.HasValue ? Convert.ToInt32(page.Value) : 1;
            var products = _dbContext.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchTxt))
            {
                products = products.Where(n => n.Title.Contains(searchTxt));
            }
            else
            {
                searchTxt = "";
            }
            if (categoryFilter != null)
            {
                products = products.Where(n => n.ProductCategoryId == categoryFilter);
            }
            var pageProducts = products.OrderByDescending(x => x.Id).ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            ViewBag.SearchTxt = searchTxt;
            ViewBag.CategoryFilter = categoryFilter;
            ViewBag.ProcuctCategories = new SelectList(_dbContext.ProductCategories.ToList(), "Id", "Title");
            return View(pageProducts);
        }

        public ActionResult Add()
        {
            ViewBag.ProcuctCategories = new SelectList(_dbContext.ProductCategories.ToList(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product product, List<string> Images, List<int> rDefault)
        {
            if (ModelState.IsValid)
            {
                if (Images != null)
                {
                    int index = 1;
                    foreach (var image in Images)
                    {
                        product.ProductImages.Add(new ProductImage
                        {
                            ProductId = product.Id,
                            Image = image,
                            IsDefault = index == rDefault[0]
                        });

                        if (index == rDefault[0])
                        {
                            product.Image = image;
                        }

                        index++;
                    }
                }
                product.ProductCategory = _dbContext.ProductCategories.Find(product.ProductCategoryId);
                product.CreatedDate = DateTime.Now;
                product.ModifiedDate = DateTime.Now;
                product.IsNew = true;
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProcuctCategories = new SelectList(_dbContext.ProductCategories.ToList(), "Id", "Title");
            return View();
        }

        public ActionResult Edit(int id)
        {
            var product = _dbContext.Products.Include(p => p.ProductImages).SingleOrDefault(p => p.Id == id);
            ViewBag.ProcuctCategories = new SelectList(_dbContext.ProductCategories.ToList(), "Id", "Title");
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var productInDb = _dbContext.Products
                    .Include(p => p.ProductImages)
                    .SingleOrDefault(p => p.Id == product.Id);
                product.ProductImages = productInDb.ProductImages;
                product.Image = productInDb.Image;
                product.ProductCategory = _dbContext.ProductCategories.Find(product.ProductCategoryId);
                product.ModifiedDate = DateTime.Now;
                _dbContext.Entry(productInDb).CurrentValues.SetValues(product);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProcuctCategories = new SelectList(_dbContext.ProductCategories.ToList(), "Id", "Title");
            return View(product);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            var product = _dbContext.Products.Find(id);
            if (product == null)
            {
                return Json(new { success = false });
            }
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult DeleteAll(List<int> ids)
        {
            if(ids==null)
            {
                return Json(new { success = false });
            }
            foreach(var id in ids)
            {
                var model = _dbContext.Products.Find(id);
                if (model == null)
                {
                    return Json(new { success = false });
                }
                _dbContext.Products.Remove(model);
            }
            _dbContext.SaveChanges();
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var product = _dbContext.Products.Find(id);
            if (product == null)
            {
                return Json(new { success = false });
            }
            product.isActive = !product.isActive;
            _dbContext.SaveChanges();
            return Json(new { success = true, active = product.isActive });
        }

        [HttpPost]
        public ActionResult IsHome(int id)
        {
            var product = _dbContext.Products.Find(id);
            if (product == null)
            {
                return Json(new { success = false });
            }
            product.IsHome = !product.IsHome;
            _dbContext.SaveChanges();
            return Json(new { success = true, isHome = product.IsHome });
        }
        [HttpPost]
        public ActionResult IsHot(int id)
        {
            var product = _dbContext.Products.Find(id);
            if (product == null)
            {
                return Json(new { success = false });
            }
            product.IsHot = !product.IsHot;
            _dbContext.SaveChanges();
            return Json(new { success = true, isHot = product.IsHot });
        }
        [HttpPost]
        public ActionResult IsNew(int id)
        {
            var product = _dbContext.Products.Find(id);
            if (product == null)
            {
                return Json(new { success = false });
            }
            product.IsNew = !product.IsNew;
            _dbContext.SaveChanges();
            return Json(new { success = true, isNew = product.IsNew });
        }
    }

}