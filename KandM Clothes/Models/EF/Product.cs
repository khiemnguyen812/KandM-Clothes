using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KandM_Clothes.Models.EF
{
    [Table ("_tb_Product")]
    public class Product : CommonAbstract
    {
        public Product()
        {
            this.ProductImages = new HashSet<ProductImage>();
            this.OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(150, ErrorMessage = "Không được vượt quá 150 kí tự")]
        public string Title { get; set; }
        public int ProductCategoryId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Tên viết tắc không được để trống")]
        [StringLength(15, ErrorMessage = "Không được vượt quá 15 kí tự")]
        public string ProductCode { get; set; }
        public decimal PriceSale { get; set; }
        public int Quantity { get; set; }
        public string Detail { get; set; }
        public string Image { get; set; }
        public bool IsFeature { get; set; }
        public bool IsNew { get; set; }
        public bool IsSale { get; set; }
        public bool IsHot { get; set; }
        public bool IsHome { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}