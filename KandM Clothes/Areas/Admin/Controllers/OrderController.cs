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
    public class OrderController : Controller
    {
        public ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Admin/Order
        public ActionResult Index(int? page,string searchTxt)
        {
            if(page == null || page < 1)
            {
                page = 1;
            }

            var pageSize = 10;
            var pageIndex = page.HasValue ? Convert.ToInt32(page.Value) : 1;
            var orders = _dbContext.Orders.AsQueryable();

            if (!string.IsNullOrEmpty(searchTxt))
            {
                orders = orders.Where(o => o.CustomerName.Contains(searchTxt) || o.Phone.Contains(searchTxt) || o.Address.Contains(searchTxt));
            }

            else searchTxt = "";

            var pageOrders = orders.OrderByDescending(x => x.Id).ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            ViewBag.SearchTxt = searchTxt;
            return View(pageOrders);
        }

        [HttpPost]
        public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return HttpNotFound();
			}
			var order = _dbContext.Orders.Find(id);
			if (order == null)
			{
				return HttpNotFound();
			}
			_dbContext.Orders.Remove(order);
			_dbContext.SaveChanges();
            return Json(new { success = true });
		}

        public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return HttpNotFound();
			}
			var order = _dbContext.Orders.Find(id);
			if (order == null)
			{
				return HttpNotFound();
			}
            var OrderItems = _dbContext.OrderDetails.Where(x => x.OrderId == id).ToList();
            ViewBag.OrderItems = OrderItems;
			return View(order);
		}

		[HttpPost]
		public ActionResult Edit(Order order)
		{
			if (!ModelState.IsValid)
			{
				return View(order);
			}
			var orderInDb = _dbContext.Orders.Find(order.Id);
			if (orderInDb == null)
			{
				return HttpNotFound();
			}
			orderInDb.CustomerName = order.CustomerName;
			orderInDb.Phone = order.Phone;
			orderInDb.Address = order.Address;
			orderInDb.TypePayment = order.TypePayment;
			orderInDb.ModifiedDate = DateTime.Now;
			_dbContext.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}