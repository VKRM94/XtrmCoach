using Microsoft.Practices.Unity;
using ProductStore.Resolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using XtrmCoachRESTServer.RepositoryInterface;

namespace XtrmCoachRESTServer
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			var enableCorsAttribute = new EnableCorsAttribute("*",
											   "Origin, Content-Type, Accept",
											   "GET, PUT, POST, DELETE, OPTIONS");
			config.EnableCors(enableCorsAttribute);

			var container = new UnityContainer();
			container.RegisterType<IUserRepository, UserRepository>(new HierarchicalLifetimeManager());
			container.RegisterType<ISportRepository, SportRespository>(new HierarchicalLifetimeManager());
			container.RegisterType<IPerformanceParameterRepository, PerformanceParameterRepository>(new HierarchicalLifetimeManager());
			container.RegisterType<IPerformanceParameterNameRepository, PerformanceParameterNameRepository>(new HierarchicalLifetimeManager());
			container.RegisterType<IPerformanceParameterTypeRepository, PerformanceParameterTypeRepository>(new HierarchicalLifetimeManager());
			container.RegisterType<IPerformanceParameterTypeGroupRepository, PerformanceParameterTypeGroupRepository>(new HierarchicalLifetimeManager());
			container.RegisterType<IPlayerRepository, PlayerRespository>(new HierarchicalLifetimeManager());
			container.RegisterType<IPlayerEvaluationRepository, PlayerEvaluationRepository>(new HierarchicalLifetimeManager());
			config.DependencyResolver = new UnityResolver(container);
		}
	}
}
