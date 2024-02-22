using PMRS_Mvc.Areas.Security.DAO;
using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System.Web.Mvc;

namespace PMRS_Mvc.Areas.Security.Controllers
{
    [LoginChecker]
    public class MHController : Controller
    {
        private readonly MHDAO _dao = new MHDAO();

        public ActionResult frmMHConfig()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetHeadMenuList()
        {
            var data = _dao.GetHeadMenuList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertMh(SecMH ast)
        {
            string result = _dao.InsertMh(ast);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateMh(SecMH ast)
        {
            string result = _dao.UpdateMh(ast);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}