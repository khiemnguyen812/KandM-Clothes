using KandM_Clothes.Models;
using KandM_Clothes.Models.EF;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KandM_Clothes.Areas.Admin.Controllers
{
    public class ProductImageController : Controller
    {
        // GET: Admin/ProductImage
        ApplicationDbContext _dbContext = new ApplicationDbContext();
        public ActionResult Index(int id)
        {
            var items = _dbContext.ProductImages.Where(x => x.ProductId == id).ToList();
            ViewBag.ProductId = id;
            return View(items);
        }

        [HttpPost]
        public ActionResult AddImage(int productId, string url)
        {
            var item = new ProductImage();
            item.ProductId = productId;
            item.Image = url;
            item.IsDefault = false;
            _dbContext.ProductImages.Add(item);
            _dbContext.SaveChanges();
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = _dbContext.ProductImages.Find(id);
            _dbContext.ProductImages.Remove(item);
            _dbContext.SaveChanges();
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult DeleteAll(int productId)
        {
            var items = _dbContext.ProductImages.Where(x => x.ProductId == productId).ToList();
            foreach (var item in items)
            {
                _dbContext.ProductImages.Remove(item);
            }
            var product = _dbContext.Products.Find(productId);
            product.Image = "";
            _dbContext.SaveChanges();
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult SetDefault(int id, int productId)
        {
            var items = _dbContext.ProductImages.Where(x => x.ProductId == productId).ToList();
            foreach (var item in items)
            {
                item.IsDefault = false;
            }
            var itemDefault = _dbContext.ProductImages.Find(id);
            var Product = _dbContext.Products.Find(productId);
            Product.Image = itemDefault.Image;
            itemDefault.IsDefault = true;
            _dbContext.SaveChanges();
            return Json(new { success = true });
        }
    }
}