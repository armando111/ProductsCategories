namespace ProductsCategories.Service
{
    using System.Web.Http;
    using System.Web.Http.Cors;
    using System.Web.OData.Extensions;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            //CORS CONFIGURATION
            var cors = new EnableCorsAttribute("*", "*", "GET,POST,PUT,DELETE");
            config.EnableCors(cors);

            //ODATA configuration
            config.AddODataQueryFilter();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //seting JSON as comunication format
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
