using System;
using System.Web;
using System.Web.Routing;

namespace MvcApplication
{
    public class CustomConstraint : IRouteConstraint
    {
        public readonly string Value;

        public CustomConstraint(string value)
        {
            Value = value;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            // Add a break point here and see that this executes.
            return true;
        }
    }
}