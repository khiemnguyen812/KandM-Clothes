using KandM_Clothes.Models;
using KandM_Clothes.Models.EF;
using System.Linq;
using System.Web.Mvc;

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
            return View();
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