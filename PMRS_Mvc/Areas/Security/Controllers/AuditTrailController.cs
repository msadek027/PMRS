using PMRS_Mvc.Areas.Security.DAO;
using PMRS_Mvc.Common;
using System;
using System.Web.Mvc;

namespace PMRS_Mvc.Areas.Security.Controllers
{
    [LoginChecker]
    public class AuditTrailController : Controller
    {
        private readonly AuditTrailDAO _primaryDao = new AuditTrailDAO();

        // GET: Security/AuditTrail
        public ActionResult frmRptAuditTrail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetAuditTrail(string FromDate, string ToDate, string Action_By, string Action_Type)
        {
            try
            {
                var data = _primaryDao.GetAuditTrail(FromDate, ToDate, Action_By, Action_Type);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                //ex.Message.ToString();
                return View("frmRptAuditTrail");
            }
        }

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }
    }
}