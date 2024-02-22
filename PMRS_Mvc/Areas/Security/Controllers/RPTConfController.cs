using PMRS_Mvc.Areas.Security.DAO;
using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Web.Mvc;
namespace PMRS_Mvc.Areas.Security.Controllers
{
    [LoginChecker]
    // GET: Security/RPTConf
    public class RPTConfController : Controller
    {
        private readonly RPTConfDAO _dao = new RPTConfDAO();

        // GET: Security/RPTConf
        //public ActionResult frmRptConfig()
        //{
        //    return View();
        //}

        [HttpGet]
        public ActionResult GetReportFormList()
        {
            var data = _dao.GetReportFormList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetReportsByForm(string frmName)
        {
            var data = _dao.GetReportsByForm(frmName);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetRoleWiseReports(string frmName, string rlId)
        {
            var data = _dao.GetRoleWiseReports(frmName, Convert.ToInt32(rlId));
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertRptConf(RPTConf ast)
        {
            string result = _dao.InsertRptConf(ast);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ReportDel(string ID)
        {
            var data = _dao.ReportDel(Convert.ToInt32(ID));
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetReportByFormRole(string frmName)
        {
            var data = _dao.GetReportByFormRole(frmName);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
