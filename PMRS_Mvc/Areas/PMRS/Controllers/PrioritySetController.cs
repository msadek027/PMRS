using PMRS_Mvc.Areas.PMRS.DAO;
using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Web.Mvc;

namespace PMRS_Mvc.Areas.PMRS.Controllers
{
    [LoginChecker]
    public class PrioritySetController : Controller
    {
        private PrioritySetDAO primaryDAO = new PrioritySetDAO();

        // GET: PMRS/PrioritySet
        public ActionResult frmPrioritySet()
        {
            return View();
        }

        public ActionResult GetResolutionBySession(string session)
        {
            var data = primaryDAO.GetResolutionBySession(Convert.ToInt32(session));
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSentResolutionBySession(string session)
        {
            var data = primaryDAO.GetSentResolutionBySession(Convert.ToInt32(session));
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdatePriority(ResolutionApproval master)
        {
            try
            {
                if (primaryDAO.UpdatePriority(Convert.ToInt32(master.ResolutionApproveID), Convert.ToInt32(master.MemberResPriority)))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmResolutionApproval");
            }
            catch (Exception)
            {
                return View("frmResolutionApproval");
            }
        }

    }
}