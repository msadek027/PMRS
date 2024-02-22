using PMRS_Mvc.Areas.PMRS.DAO;
using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Web.Mvc;

namespace PMRS_Mvc.Areas.PMRS.Controllers
{
    [LoginChecker]
    public class BallotInfoController : Controller
    {
        private BallotInfoDAO primaryDAO = new BallotInfoDAO();
        // GET: PMRS/BallotInfo
        public ActionResult frmBalloting()
        {
            return View();
        }

        public ActionResult GetResolutionForBalloting(string session)
        {
            var data = primaryDAO.GetResolutionForBalloting(Convert.ToInt32(session));
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertBallot(BallotInfo master)
        {
            try
            {
                if (primaryDAO.InsertBallot(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmBalloting");
            }
            catch (Exception)
            {
                return View("frmBalloting");
            }
        }


    }
}