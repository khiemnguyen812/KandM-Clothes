using KandM_Clothes.Models;
using KandM_Clothes.Models.EF;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.UI.WebControls;

namespace KandM_Clothes.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ApplicationDbContext _dbContext = new ApplicationDbContext();
        public ActionResult Index()
        {
            ShoppingCart cart = Session["Cart"] as ShoppingCart;
            if (cart == null)
            {
                cart = new ShoppingCart();
                Session["Cart"] = cart;
            }
            return View(cart);
        }

        public ActionResult CheckOut()
        {
            ShoppingCart cart = Session["Cart"] as ShoppingCart;
            if (cart == null)
            {
                cart = new ShoppingCart();
                Session["Cart"] = cart;
            }
            ViewBag.Cart = cart;
            return View(new Order());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(CustomerInfo cusInfo)
        {
            var code = new { success = false, msg = "", code = -1 };
            if(ModelState.IsValid)
            {
                ShoppingCart cart = Session["Cart"] as ShoppingCart;
                if (cart != null && cart.items.Count > 0)
                {
                    Order order = new Order();
                    order.CustomerName = cusInfo.CustomerName;
                    order.Phone = cusInfo.Phone;
                    order.Address = cusInfo.Address;
                    order.Email = cusInfo.Email;
                    order.TypePayment = cusInfo.TypePayment;
                    order.TotalPrice = cart.GetTotalPrice();
                    order.Quantity = cart.GetTotalQuantity();
                    order.Code = "DH" + DateTime.Now.ToString("ddMMyyyyHHmmss");
                    order.CreatedDate = DateTime.Now;
                    order.Code = Models.Common.Common.CreateCode();
                    var ProductList = "";
                    foreach (var item in cart.items)
                    {
                        OrderDetail orderDetail = new OrderDetail();
                        orderDetail.ProductId = item.Product.Id;
                        orderDetail.Quantity = item.Quantity;
                        orderDetail.Price = item.Product.Price;
                        order.OrderDetails.Add(orderDetail);
                        ProductList += "<tr>";
                        ProductList += "<td style='color:#636363;border:1px solid #e5e5e5;padding:12px;text-align:left;vertical-align:middle;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;word-wrap:break-word'>"+ item.Product.Title +"</td>";
                        ProductList += "<td style='color:#636363;border:1px solid #e5e5e5;padding:12px;text-align:left;vertical-align:middle;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif'>" + item.Quantity + "</td>";
                        ProductList += "<td style='color:#636363;border:1px solid #e5e5e5;padding:12px;text-align:left;vertical-align:middle;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif'><span><span>$ </span>" + item.Product.Price+ "&nbsp;</span></td>";
                        ProductList += "</tr>";
                    }
                    _dbContext.Orders.Add(order);
                    _dbContext.SaveChanges();
                    string MailContent = System.IO.File.ReadAllText(Server.MapPath("~/Content/template/send2.html"));

                    MailContent = MailContent.Replace("{{CustomerName}}", order.CustomerName);
                    MailContent = MailContent.Replace("{{CustomerPhone}}", order.Phone);
                    MailContent = MailContent.Replace("{{CustomerAddress}}", order.Address);
                    MailContent = MailContent.Replace("{{CustomerEmail}}", order.Email);
                    MailContent = MailContent.Replace("{{ProductList}}", ProductList);
                    MailContent = MailContent.Replace("{{TotalPrice}}", order.TotalPrice.ToString());
                    MailContent = MailContent.Replace("{{OrderCode}}", order.Code);
                    MailContent = MailContent.Replace("{{OrderDate}}", order.CreatedDate.ToString());
                    MailContent = MailContent.Replace("{{PaymentMethod}}", order.TypePayment == 1 ? "Thanh toán khi nhận hàng" : "Thanh toán qua thẻ");
                    MailContent = MailContent.Replace("{{OrderQuantity}}", order.Quantity.ToString());
                    KandM_Clothes.Models.Common.Common.SendMail("Đơn hàng mới từ K&M Clothes","Đơn hàng "+ order.Code, MailContent, order.Email);
                    cart.ClearCart();
                    code = new { success = true, msg = "Đặt hàng thành công", code = 1 };
                }
                else
                {
                    code = new { success = false, msg = "Giỏ hàng trống", code = 0 };
                }
            }
            else
            {
                code = new { success = false, msg = "Dữ liệu không hợp lệ", code = -1 };
            }
            return Json(code, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShowCount()
        {
            ShoppingCart cart = Session["Cart"] as ShoppingCart;
            int count = 0;
            if (cart != null)
            {
                count = cart.items.Count();
            }
            return Json(new { count = count }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddToCart(int id, int quantity)
        {
            var code = new { success = false, msg = "", code = -1, count = 0 };
            var product = _dbContext.Products.Find(id);
            if (product != null)
            {
                ShoppingCart cart = Session["Cart"] as ShoppingCart;
                if (cart == null)
                {
                    cart = new ShoppingCart();
                    Session["Cart"] = cart;
                }
                else
                {
                    cart.AddToCart(product.Id, quantity);
                }
                code = new { success = true, msg = "Thêm vào giỏ hàng thành công", code = 1, count = cart.items.Count() };
            }
            return Json(code, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            ShoppingCart cart = Session["Cart"] as ShoppingCart;
            cart.RemoveFromCart((int)id);
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult IncreaseQuantity(int? id)
        {
            ShoppingCart cart = Session["Cart"] as ShoppingCart;
            cart.IncreaseQuantity((int)id);
            return Json(new { success = true });
        }
        [HttpPost]
        public ActionResult DecreaseQuantity(int? id)
        {
            ShoppingCart cart = Session["Cart"] as ShoppingCart;
            cart.DecreaseQuantity((int)id);
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult DeleteAll()
        {
            ShoppingCart cart = Session["Cart"] as ShoppingCart;
            cart.ClearCart();
            return Json(new { success = true });
        }
    }
}