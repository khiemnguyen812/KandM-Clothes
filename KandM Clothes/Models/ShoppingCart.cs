using KandM_Clothes.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KandM_Clothes.Models
{
    public class ShoppingCart
    {
        public ApplicationDbContext _dbContext = new ApplicationDbContext();
        public List<ShoppingCartItem> items = null;
        public ShoppingCart()
        {
            items = new List<ShoppingCartItem>();
        }

        public void AddToCart(int productId, int quantity)
        {
            var product = _dbContext.Products.Find(productId);
            if (product != null)
            {
                var item = items.FirstOrDefault(i => i.Product.Id == productId);
                if (item == null)
                {
                    items.Add(new ShoppingCartItem
                    {
                        Product = product,
                        Quantity = quantity,
                    });
                }
                else
                {
                    item.Quantity += quantity;
                }
            }
        }

        public void RemoveFromCart(int productId)
        {
            var item = items.FirstOrDefault(i => i.Product.Id == productId);
            if (item != null)
            {
                items.Remove(item);
            }
        }
        public void UpdateQuantity(int productId, int quantity)
        {
            var item = items.FirstOrDefault(i => i.Product.Id == productId);
            if (item != null)
            {
                item.Quantity = quantity;
            }
        }

        public void ClearCart()
        {
            items.Clear();
        }

        public decimal GetTotalPrice()
        {
            decimal total = 0;
            foreach (var item in items)
            {
                if(item.Product.IsSale == true)
                {
                    total += item.Product.PriceSale * item.Quantity;
                }
                else
                {
                    total += item.Product.Price * item.Quantity;
                }
            }
            return total;
        }

        public int GetTotalQuantity()
        {
            int total = 0;
            foreach (var item in items)
            {
                total += item.Quantity;
            }
            return total;
        }

        public void IncreaseQuantity(int productId)
        {
            var item = items.FirstOrDefault(i => i.Product.Id == productId);
            if (item != null)
            {
                item.Quantity++;
            }
        }

        public void DecreaseQuantity(int productId)
        {
            var item = items.FirstOrDefault(i => i.Product.Id == productId);
            if (item != null)
            {
                item.Quantity--;
                if (item.Quantity <= 0)
                {
                    items.Remove(item);
                }
            }
        }
    }

    public class ShoppingCartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}