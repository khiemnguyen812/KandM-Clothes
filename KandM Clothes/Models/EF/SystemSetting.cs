using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KandM_Clothes.Models.EF
{
    [Table ("_tb_SystemSetting")]
    public class SystemSetting
    {
        [Key]
        [StringLength(100)]
        public string SettingKey { get; set; }
        [StringLength(1000)]

        public string SettingValue { get; set; }
        public string SettingDescription { get; set; }
    }
}