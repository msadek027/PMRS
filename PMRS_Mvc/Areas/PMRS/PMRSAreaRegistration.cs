using System.Web.Mvc;

namespace PMRS_Mvc.Areas.PMRS
{
    public class PMRSAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PMRS";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PMRS_default",
                "PMRS/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}