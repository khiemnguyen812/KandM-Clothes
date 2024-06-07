using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KandM_Clothes.Models
{
    public class StatisticViewModel
    {
        public int Today { get; set; }
        public int Yesterday { get; set; }
        public int Week { get; set; }
        public int LastWeek { get; set; }
        public int Month { get; set; }
        public int LastMonth { get; set; }
        public int TotalAccess { get; set; }

    }
}