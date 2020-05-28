using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApiUsers
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ////Evito las referencias circulares al trabajar con Entity FrameWork         
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;

            ////Elimino que el sistema devuelva en XML, sólo trabajaremos con JSON
            //GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);


            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

            // REGISTRO AUTOFAC-- CONTENEDOR DE DEPENDENCIAS
            #region REGISTRO AUTOFAC
            var builder = new ContainerBuilder();

            // builder.RegisterType<PersonasController>().InstancePerRequest();

            // Get your HttpConfiguration.
            //var config = GlobalConfiguration.Configuration;

            var config = new HttpConfiguration();

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            builder.RegisterControllers(typeof(WebApiApplication).Assembly);

            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(typeof(WebApiApplication).Assembly);
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // OPTIONAL: Enable action method parameter injection (RARE).
            //  builder.InjectActionInvoker();

            //DEPENDENCIAS DE LA APLICACION
            // APPLICATION SERVICE
           // builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest();
         
                       

         //   builder.RegisterAssemblyTypes(typeof(Models.Command.Persona.PersonaCommand).GetTypeInfo().Assembly).AsImplementedInterfaces();
           // builder.RegisterAssemblyTypes(typeof(Models.Query.Persona.PersonaQuery).GetTypeInfo().Assembly).AsImplementedInterfaces();


            // Set the dependency resolver to be Autofac.
            var container = builder.Build();

            // GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container); //Set the WebApi DependencyResolver
            //  config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            // DependencyResolver.SetResolver(new AutofacDependencyResolver(container));


            //AQUI
            //// Set the dependency resolver for Web API.
            var webApiResolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;

            //// Set the dependency resolver for MVC.
            var mvcResolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(mvcResolver);
            //HASTA AQUI

            #endregion
        }
    }
}
