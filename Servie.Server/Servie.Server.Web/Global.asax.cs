
﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SimpleInjector;
using Servie.Server.Services;
using Servie.Server.Repositories;

namespace Servie.Server.Web
{
	public class MvcApplication : System.Web.HttpApplication
	{
		public static Container IocContainer { get { return DIContainer.Current; } }

		public static void RegisterRoutes (RouteCollection routes)
		{
			routes.IgnoreRoute ("{resource}.axd/{*pathInfo}");
			
			routes.MapRoute (
				"v1/Authorization",
				"v1/Authorization/{action}",
				new { controller = "Authorization", action = "Index" }
			);
			
			routes.MapRoute (
				"Default",
				"{controller}/{action}/{id}",
				new { controller = "Authorization", action = "Index", id = "" }
			);

		}

		protected void Application_Start ()
		{
			AreaRegistration.RegisterAllAreas ();
			RegisterRoutes (RouteTable.Routes);

			ValueProviderFactories.Factories.Add(new JsonValueProvider());
			
			//repositories		
			IocContainer.RegisterSingle<IPersonsRepository, PersonsRepository>();
			
			//services
			IocContainer.RegisterSingle<IPersonService, PersonService>();
			
			IocContainer.RegisterAsMvcControllerFactory();
		}
	}

	public static class SimpleInjectorControllerFactoryExtension
	{
		public static void RegisterAsMvcControllerFactory(this Container container)
		{
			ControllerBuilder.Current.SetControllerFactory(new SimpleControllerFactory(container));
		}
		
		private sealed class SimpleControllerFactory : DefaultControllerFactory
		{
			public Container Container { get; set; }
			
			public SimpleControllerFactory(Container container)
			{
				Container = container;
			}
			
			protected override IController GetControllerInstance (RequestContext requestContext, Type controllerType)
			{
				if (controllerType == null || !typeof(IController).IsAssignableFrom (controllerType))
					return base.GetControllerInstance (requestContext, controllerType);
				
				return (IController)this.Container.GetInstance (controllerType);
			}
		}
	}
}

