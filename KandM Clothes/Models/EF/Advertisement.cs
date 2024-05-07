using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KandM_Clothes.Models.EF
{
    [Table("_tb_Advertisement")]
    public class Advertisement : CommonAbstract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage ="Tên không được để trống")]
        [StringLength(150, ErrorMessage ="Không được vượt quá 150 kí tự")]
        public string Title { get; set; }
        [StringLength(5000, ErrorMessage = "Không được vượt quá 5000 kí tự")]
        public string Description { get; set; }
        public string Image { get; set; }
        [StringLength(150, ErrorMessage = "Không được vượt quá 150 kí tự")]
        public string Type { get; set; }
        public string Link { get; set; }
    }
}