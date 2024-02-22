using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PMRS_Mvc.Common
{
    public class LoginChecker : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            if (session != null && session["empID"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                        { "Controller", "Home" },
                        { "Action", "Login" },
                        { "Area", ""}
                    });
            }
        }
    }
}