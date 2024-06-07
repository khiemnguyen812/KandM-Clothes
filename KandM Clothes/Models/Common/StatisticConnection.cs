using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data.Entity;
using KandM_Clothes.Models.EF;
namespace KandM_Clothes.Models.Common
{
    public class StatisticConnection
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public StatisticConnection(ApplicationDbContext context)
        {
            _context = context;
        }

        public StatisticViewModel GetAndUpdateStatistics()
        {
            DateTime today = DateTime.Today;
            DateTime yesterday = today.AddDays(-1);
            DateTime oneWeekAgo = today.AddDays(-7);
            DateTime twoWeeksAgo = today.AddDays(-14);
            DateTime oneMonthAgo = today.AddMonths(-1);
            DateTime twoMonthsAgo = today.AddMonths(-2);

            var todayStatistic = _context.Statistics.FirstOrDefault(s => DbFunctions.TruncateTime(s.Date) == today);

            if (todayStatistic == null)
            {
                // Insert new record for today
                todayStatistic = new Statistic { Date = today, ViewCount = 1 };
                _context.Statistics.Add(todayStatistic);
                _context.SaveChanges();
            }
            else
            {
                // Update view count for today
                todayStatistic.ViewCount++;
                _context.SaveChanges();
            }

            var yesterdayViews = _context.Statistics.Where(s => DbFunctions.TruncateTime(s.Date) == DbFunctions.TruncateTime(yesterday)).Sum(s => (long?)s.ViewCount) ?? 0;
            var weekViews = _context.Statistics.Where(s => DbFunctions.TruncateTime(s.Date) >= DbFunctions.TruncateTime(oneWeekAgo) && DbFunctions.TruncateTime(s.Date) < DbFunctions.TruncateTime(today)).Sum(s => (long?)s.ViewCount) ?? 0;
            var lastWeekViews = _context.Statistics.Where(s => DbFunctions.TruncateTime(s.Date) >= DbFunctions.TruncateTime(twoWeeksAgo) && DbFunctions.TruncateTime(s.Date) < DbFunctions.TruncateTime(oneWeekAgo)).Sum(s => (long?)s.ViewCount) ?? 0;
            var monthViews = _context.Statistics.Where(s => DbFunctions.TruncateTime(s.Date) >= DbFunctions.TruncateTime(oneMonthAgo) && DbFunctions.TruncateTime(s.Date) < DbFunctions.TruncateTime(today)).Sum(s => (long?)s.ViewCount) ?? 0;
            var lastMonthViews = _context.Statistics.Where(s => DbFunctions.TruncateTime(s.Date) >= DbFunctions.TruncateTime(twoMonthsAgo) && DbFunctions.TruncateTime(s.Date) < DbFunctions.TruncateTime(oneMonthAgo)).Sum(s => (long?)s.ViewCount) ?? 0;
            var totalAccess = _context.Statistics.Sum(s => (long?)s.ViewCount) ?? 0;

            return new StatisticViewModel
            {
                Today = (int)todayStatistic.ViewCount,
                Yesterday = (int)yesterdayViews,
                Week = (int)weekViews,
                LastWeek = (int)lastWeekViews,
                Month = (int)monthViews,
                LastMonth = (int)lastMonthViews,
                TotalAccess = (int)totalAccess
            };
        }

    }

}