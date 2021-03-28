using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;

namespace TheCodeCamp
{
  public class WebApiApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {
      AreaRegistration.RegisterAllAreas();
      GlobalConfiguration.Configure(WebApiConfig.Register);
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
    }

      protected void Application_BeginRequest()
      {
         string[] allowedOrigin = new string[] { "*" };
         var origin = HttpContext.Current.Request.Headers["Origin"];
         if (origin != null && allowedOrigin.Contains(origin))
         {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", origin);
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET,POST");
         }
      }
   }
}
