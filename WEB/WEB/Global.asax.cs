using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using BLL.AutoMapperProfile;
using BLL.Services;
using System.Web.Routing;
using System.Configuration;
using System.Web.Http;

namespace WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            //---------------myconf---------------------
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            //----------------endmyconf--------------------

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //---------------myconf---------------------
            MapperProfile.InitAutoMapper();
            //----------------endmyconf--------------------
        }
    }
}
