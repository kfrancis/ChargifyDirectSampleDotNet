using System;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Chargify2.Configuration;

namespace ChargifyDirectExample.MVC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["Chargify.v2.apiKey"]))
                throw new ArgumentNullException("Chargify.v2.apiKey", "This example requires the Chargify Direct API key to be added to web.config");

            if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["Chargify.v2.secret"]))
                throw new ArgumentNullException("Chargify.v2.secret", "This example requires the Chargify Direct API secret to be added to web.config");

            if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["Chargify.v2.apiPassword"]))
                throw new ArgumentNullException("Chargify.v2.apiPassword", "This example requires the Chargify Direct API password to be added to web.config");
        }
    }
}