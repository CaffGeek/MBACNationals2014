using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebFrontend.Attributes
{
    public class RestrictAccessByRouteId : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
                return false;

            if (httpContext.User.IsInRole("Admin"))
                return true;

            var handler = httpContext.Handler as MvcHandler;
            var contingent = handler.RequestContext.RouteData.Values["id"] as string;

            return httpContext.User.IsInRole(contingent);
        }
    }
}