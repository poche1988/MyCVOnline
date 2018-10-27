using System.Web.Http;

namespace MyCVonline
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //next 3 lines are to return the data in camelcase format, better for javascript
            //var settings = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings;
            //settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //settings.Formatting = Newtonsoft.Json.Formatting.Indented;

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{action}/{id}",
            defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
