using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KandM_Clothes.Models.EF
{
    public class CommonAbstract
    {
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}