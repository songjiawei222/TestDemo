using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCFrame
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }


        /// <summary>
        /// 错误日志
        /// </summary>
        protected void Application_Error(Object sender, EventArgs e)
        {
            //var lastError = Server.GetLastError().GetBaseException();
            //{
            //    var log = LogManager.GetLogger(typeof(MvcApplication));
            //    log.Error(lastError);
            //}
        }
    }
}