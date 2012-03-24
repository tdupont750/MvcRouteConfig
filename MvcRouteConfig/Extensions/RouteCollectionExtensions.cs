using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using MvcRouteConfig.Configuration;
using RouteCollection = System.Web.Routing.RouteCollection;

namespace System.Web.Routing
{
    public static class RouteCollectionExtensions
    {
        public static void RegisterConfigurationBundles(this RouteCollection routes)
        {
            RouteConfigurationSection routesTableSection = GetRouteTableConfigurationSection();

            if (routesTableSection == null || routesTableSection.Routes.Count <= 0)
                return;

            for (int routeIndex = 0; routeIndex < routesTableSection.Routes.Count; routeIndex++)
            {
                var routeElement = routesTableSection.Routes[routeIndex];

                var route = new Route(
                    routeElement.Url,
                    GetDefaults(routeElement),
                    GetConstraints(routeElement),
                    GetDataTokens(routeElement),
                    GetInstanceOfRouteHandler(routeElement));

                routes.Add(routeElement.Name, route);
            }
        }

        private static RouteConfigurationSection GetRouteTableConfigurationSection()
        {
            try
            {
                return (RouteConfigurationSection)WebConfigurationManager.GetSection("routeTable");
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Can't find section <routeTable> in the configuration file", ex);
            }
        }

        private static RouteValueDictionary GetDefaults(RouteConfigurationElement route)
        {
            var dataTokensDictionary = new RouteValueDictionary();

            foreach (var dataToken in route.Defaults.Attributes)
                if (dataToken.Value.Equals("Optional", StringComparison.InvariantCultureIgnoreCase))
                    dataTokensDictionary.Add(dataToken.Key, UrlParameter.Optional);
                else
                    dataTokensDictionary.Add(dataToken.Key, dataToken.Value);

            return dataTokensDictionary;
        }

        private static RouteValueDictionary GetConstraints(RouteConfigurationElement route)
        {
            try
            {
                var dictionary = GetDictionaryFromAttributes(route.Constraints.Attributes);

                for (var i = 0; i < route.Constraints.Count; i++)
                {
                    var constraint = route.Constraints[i];
                    var routeConstraintType = Type.GetType(constraint.Type);

                    IRouteConstraint routeConstraint;
                    if (constraint.Params.Attributes.Count > 0)
                    {
                        var parameters = constraint.Params.Attributes.Values.ToArray();
                        routeConstraint = (IRouteConstraint)Activator.CreateInstance(routeConstraintType, parameters);
                    }
                    else
                        routeConstraint = (IRouteConstraint)Activator.CreateInstance(routeConstraintType);

                    dictionary.Add(constraint.Name, routeConstraint);
                }

                return dictionary;
            }
            catch (Exception ex)
            {
                var message = String.Format("Can't create an instance of IRouteHandler {0}", route.RouteHandlerType);
                throw new ApplicationException(message, ex);
            }
        }

        private static RouteValueDictionary GetDataTokens(RouteConfigurationElement route)
        {
            return GetDictionaryFromAttributes(route.DataTokens.Attributes);
        }

        private static IRouteHandler GetInstanceOfRouteHandler(RouteConfigurationElement route)
        {
            if (String.IsNullOrEmpty(route.RouteHandlerType))
                return new MvcRouteHandler();
            
            try
            {
                Type routeHandlerType = Type.GetType(route.RouteHandlerType);
                return Activator.CreateInstance(routeHandlerType) as IRouteHandler;
            }
            catch (Exception ex)
            {
                var message = String.Format("Can't create an instance of IRouteHandler {0}", route.RouteHandlerType);
                throw new ApplicationException(message, ex);
            }
        }

        private static RouteValueDictionary GetDictionaryFromAttributes(Dictionary<string, string> attributes)
        {
            var dataTokensDictionary = new RouteValueDictionary();

            foreach (var dataTokens in attributes)
                dataTokensDictionary.Add(dataTokens.Key, dataTokens.Value);

            return dataTokensDictionary;
        }
    }
}