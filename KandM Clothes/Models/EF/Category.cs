using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

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
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public ICollection<New> News { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}