using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KandM_Clothes.Models.EF
{
    [Table("_tb_Account")]
    public class Account : CommonAbstract
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public string Avatar { get; set; }
    }
}