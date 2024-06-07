using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KandM_Clothes.Models.EF
{
    [Table ("_tb_Order")]
    public class Order : CommonAbstract
    {
        public Order()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Code { get; set; }
        [Required(ErrorMessage ="Tên khách hàng không được để trống")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage ="Số điện thoại không được để trống")]
        public string Phone { get; set; }
        [Required(ErrorMessage ="Địa chỉ không được để trống")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Email không được để trống")]
        public string Email { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        public int TypePayment { get; set; }
        [Required]
        public int Quantity { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}