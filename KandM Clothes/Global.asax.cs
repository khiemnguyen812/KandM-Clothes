using KandM_Clothes.Models;
using KandM_Clothes.Models.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace KandM_Clothes
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Application["Today"] = 0;
            Application["Yesterday"] = 0;
            Application["Week"] = 0;
            Application["LastWeek"] = 0;
            Application["Month"] = 0;
            Application["LastMonth"] = 0;
            Application["TotalAccess"] = 0;
            Application["VisitorOnline"] = 0; 

        }

        void Session_Start(object sender, EventArgs e)
        {
            ApplicationDbContext _dbContext = new ApplicationDbContext();
            Session.Timeout = 300;
            Application.Lock();
            Application["VisitorOnline"] = Convert.ToInt32(Application["VisitorOnline"]) + 1;

            try
            {
                var connection = new StatisticConnection(_dbContext);
                var item = connection.GetAndUpdateStatistics();

                if (item != null)
                {
                    Application["Today"] = (long)item.Today;
                    Application["Yesterday"] = (long)item.Yesterday;
                    Application["Week"] = (long)item.Week;
                    Application["LastWeek"] = (long)item.LastWeek;
                    Application["Month"] = (long)item.Month;
                    Application["LastMonth"] = (long)item.LastMonth;
                    Application["TotalAccess"] = (long)item.TotalAccess;
                }
            }
            catch (Exception)
            {
                Application["Today"] = 0L;
                Application["Yesterday"] = 0L;
                Application["Week"] = 0L;
                Application["LastWeek"] = 0L;
                Application["Month"] = 0L;
                Application["LastMonth"] = 0L;
                Application["TotalAccess"] = 0L;
            }

        }

        void Session_End(object sender, EventArgs e)
        {
            Application.Lock();
            Application["VisitorOnline"] = Convert.ToInt32(Application["VisitorOnline"]) - 1;
            Application.UnLock();
        }
    }
}
