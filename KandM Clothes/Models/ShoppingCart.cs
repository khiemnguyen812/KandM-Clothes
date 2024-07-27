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
		public int Id { get; set; }
		public string UserId { get; set; }
		public virtual ApplicationUser User { get; set; }
		public virtual ICollection<ShoppingCartItem> items { get; set; }

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
				if (item.Product.IsSale == true)
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

		public void AddToCart(string userId, int productId, int quantity)
		{
			var cart = _dbContext.ShoppingCarts.Include("items").FirstOrDefault(c => c.UserId == userId);
			if (cart == null)
			{
				cart = new ShoppingCart { UserId = userId };
				_dbContext.ShoppingCarts.Add(cart);
			}

			var product = _dbContext.Products.Find(productId);
			if (product != null)
			{
				var item = cart.items.FirstOrDefault(i => i.ProductId == productId);
				if (item == null)
				{
					cart.items.Add(new ShoppingCartItem
					{
						Product = product,
						Quantity = quantity,
						ShoppingCart = cart
					});
				}
				else
				{
					item.Quantity += quantity;
				}
			}

			_dbContext.SaveChanges();
		}

		public void RemoveFromCart(string userId, int productId)
		{
			var cart = _dbContext.ShoppingCarts.Include("items").FirstOrDefault(c => c.UserId == userId);
			if (cart != null)
			{
				var item = cart.items.FirstOrDefault(i => i.ProductId == productId);
				if (item != null)
				{
					cart.items.Remove(item);
					_dbContext.SaveChanges();
				}
			}
		}

		public void UpdateQuantity(string userId, int productId, int quantity)
		{
			var cart = _dbContext.ShoppingCarts.Include("items").FirstOrDefault(c => c.UserId == userId);
			if (cart != null)
			{
				var item = cart.items.FirstOrDefault(i => i.ProductId == productId);
				if (item != null)
				{
					item.Quantity = quantity;
					_dbContext.SaveChanges();
				}
			}
		}

		public void ClearCart(string userId)
		{
			var cart = _dbContext.ShoppingCarts.Include("items").FirstOrDefault(c => c.UserId == userId);
			if (cart != null)
			{
				_dbContext.ShoppingCartItems.RemoveRange(cart.items);
				_dbContext.ShoppingCarts.Remove(cart);
				_dbContext.SaveChanges();
			}
		}

		public decimal GetTotalPrice(string userId)
		{
			var cart = _dbContext.ShoppingCarts.Include("items").FirstOrDefault(c => c.UserId == userId);
			if (cart != null)
			{
				decimal total = 0;
				foreach (var item in cart.items)
				{
					if (item.Product.IsSale)
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
			return 0;
		}

		public int GetTotalQuantity(string userId)
		{
			var cart = _dbContext.ShoppingCarts.Include("items").FirstOrDefault(c => c.UserId == userId);
			if (cart != null)
			{
				int total = 0;
				foreach (var item in cart.items)
				{
					total += item.Quantity;
				}
				return total;
			}
			return 0;
		}

		public void IncreaseQuantity(string userId, int productId)
		{
			var cart = _dbContext.ShoppingCarts.Include("items").FirstOrDefault(c => c.UserId == userId);
			if (cart != null)
			{
				var item = cart.items.FirstOrDefault(i => i.ProductId == productId);
				if (item != null)
				{
					item.Quantity++;
					_dbContext.SaveChanges();
				}
			}
		}

		public void DecreaseQuantity(string userId, int productId)
		{
			var cart = _dbContext.ShoppingCarts.Include("items").FirstOrDefault(c => c.UserId == userId);
			if (cart != null)
			{
				var item = cart.items.FirstOrDefault(i => i.ProductId == productId);
				if (item != null)
				{
					item.Quantity--;
					if (item.Quantity <= 0)
					{
						_dbContext.ShoppingCartItems.Remove(item); // Explicitly remove the item from the context
					}
					_dbContext.SaveChanges();
				}
			}
		}

	}

	public class ShoppingCartItem
	{
		public int Id { get; set; }
		public int ShoppingCartId { get; set; }
		public virtual ShoppingCart ShoppingCart { get; set; }
		public int ProductId { get; set; }
		public virtual Product Product { get; set; }
		public int Quantity { get; set; }
	}
}
