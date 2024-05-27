using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebGrease.Css.Ast.Selectors;

namespace KandM_Clothes.Models.EF
{
    [Table("_tb_Category")]
    public class Category : CommonAbstract
    {
        public Category() { 
            this.News = new HashSet<New>();
            this.Posts = new HashSet<Post>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage ="Tên danh mục không được để trống")]  
        public string Title { get; set; }
        [Required(ErrorMessage = "Miêu tả không được để trống")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Vị trí danh mục không được để trống")]
        public int Position { get; set; }
        public string Alias { get; set; }
        public ICollection<New> News { get; set; }
        public ICollection<Post> Posts { get; set; }

    }
}