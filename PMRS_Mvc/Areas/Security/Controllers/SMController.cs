using PMRS_Mvc.Areas.Security.DAO;
using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Web.Mvc;


namespace PMRS_Mvc.Areas.Security.Controllers
{
    [LoginChecker]
    public class SMController : Controller
    {
        private readonly SMDAO _dao = new SMDAO();

        //
        // GET: /Security/SM/
        public ActionResult frmSMConfig()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetSubMenuList()
        {
            var data = _dao.GetSubMenuList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertSm(SecSM ast)
        {
            string result = _dao.InsertSm(ast);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateSm(SecSM ast)
        {
            string result = _dao.UpdateSm(ast);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetSubMenuListNyMHID(string MH_ID)
        {
            var data = _dao.GetSubMenuListNyMHID(Convert.ToInt32(MH_ID));
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}