﻿using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KandM_Clothes.Models.EF;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KandM_Clothes.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
		public string RoleId { get; set; }

		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext()
			: base("DefaultConnection", throwIfV1Schema: false)
		{
		}

		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductCategory> ProductCategories { get; set; }
		public DbSet<Subscribe> Subscribes { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<Advertisement> Advertisements { get; set; }
		public DbSet<New> News { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<SystemSetting> SystemSettings { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }
		public DbSet<Statistic> Statistics { get; set; }
		public DbSet<ShoppingCart> ShoppingCarts { get; set; }
		public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}

		public int findCategory(string name)
		{
			var category = Categories.FirstOrDefault(x => x.Title == name);
			if (category != null)
			{
				return category.Id;
			}
			return 13;
		}
	}

}