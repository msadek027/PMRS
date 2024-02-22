using PMRS_Mvc.Areas.Transaction.DAO;
using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Web.Mvc;

namespace PMRS_Mvc.Areas.Transaction.Controllers
{
    [LoginChecker]
    public class ParliamentSessionInfoController : Controller
    {
        ParliamentSessionInfoDAO primaryDAO = new ParliamentSessionInfoDAO();
        // GET: Transaction/ParliamentSessionInfo
        public ActionResult frmParliamentSessionInfo()
        {
            return View();
        }

        public ActionResult GetSession()
        {
            var data = primaryDAO.GetSession();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetActiveSession()
        {
            var data = primaryDAO.GetActiveSession();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertSession(ParliamentSessionInfo master)
        {
            try
            {
                if (primaryDAO.InsertSession(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmParliamentSessionInfo");
            }
            catch (Exception)
            {
                return View("frmParliamentSessionInfo");
            }
        }

        [HttpPost]
        public ActionResult UpdateSession(ParliamentSessionInfo master)
        {
            try
            {
                if (primaryDAO.UpdateSession(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmParliamentSessionInfo");
            }
            catch (Exception)
            {
                return View("frmParliamentSessionInfo");
            }
        }
    }
}