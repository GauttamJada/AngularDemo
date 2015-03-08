using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace MVCAngularWeb
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();


            config.Formatters.XmlFormatter.UseXmlSerializer = true;

            //  config.Formatters.JsonFormatter.SerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;

            //  config.Formatters.Remove(config.Formatters.JsonFormatter);

            //  config.Formatters.JsonFormatter.UseDataContractJsonSerializer = true;

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();


            //////config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new ComponentConverter());
            //////config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new GroupConverter());
            //////config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new MemberConverter());
            ////////            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new RoleConverter());
            //////config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new UserConverter());
            //config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new DocumentConverter());

            //      config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;

        }
    }
}