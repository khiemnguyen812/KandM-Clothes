using KandM_Clothes.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KandM_Clothes.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        ApplicationDbContext _dbContext = new ApplicationDbContext();
        public ActionResult Index(int? page, string searchTxt, int? categoryFilter, int? minPrice, int? maxPrice)
        {
            if (page == null || page < 1)
            {
                page = 1;
            }

            var pageSize = 20;
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
                ViewBag.CategoryFilter = _dbContext.ProductCategories.Find(categoryFilter).Title;
                products = products.Where(n => n.ProductCategoryId == categoryFilter);
            }

            if (minPrice != null)
            {
                products = products.Where(n => n.Price >= minPrice);
                ViewBag.MinPrice = minPrice;
            }
            if (maxPrice != null)
            {
                products = products.Where(n => n.Price <= maxPrice);
                ViewBag.MaxPrice = maxPrice;
            }

            var pageProducts = products.OrderByDescending(x => x.Id).ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            ViewBag.SearchTxt = searchTxt;
            ViewBag.ProcuctCategories = _dbContext.ProductCategories.ToList();
            return View(pageProducts);
        }

        public ActionResult Detail(string alias, int id)
        {
            var product = _dbContext.Products.Find(id);
            return View(product);
        }

        public ActionResult Partial_ProductByCategory()
        {
            var items = _dbContext.Products.Where(x => x.isActive && x.IsHome).ToList();
            return PartialView("_Partial_ProductByCategory", items);
        }

        public ActionResult Partial_BestSeller()
        {
            var items = _dbContext.Products.Where(x => x.isActive && x.IsHot).ToList();
            return PartialView("_Partial_BestSeller", items);
        }
    }
}