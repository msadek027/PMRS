using System.Web.Mvc;

namespace PMRS_Mvc.Areas.MP
{
    public class MPAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MP";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MP_default",
                "MP/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}