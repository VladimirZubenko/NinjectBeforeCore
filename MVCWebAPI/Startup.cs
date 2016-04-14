using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MVCWebAPI.Ninject;
using Newtonsoft.Json.Serialization;
using Owin;

namespace MVCWebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = StartupConfig.Config;

            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var routes = RouteTable.Routes;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );

            routes.LowercaseUrls = true;

            config.MapHttpAttributeRoutes();

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //Get a per-request scope into Owin pipeline.
            app.CreatePerOwinContext(() => NinjectCommon.ServciceLocator.BeginScope());

            app.UseWebApi(config);
        }
    }
}
