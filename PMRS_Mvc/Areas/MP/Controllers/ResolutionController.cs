using PMRS_Mvc.Areas.MP.DAO;
using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Web.Mvc;

namespace PMRS_Mvc.Areas.MP.Controllers
{
    [LoginChecker]
    public class ResolutionController : Controller
    {
        private ResolutionDAO primaryDAO = new ResolutionDAO();

        // GET: MP/Resolution
        public ActionResult frmResolutionEntry()
        {
            return View();
        }

    



        [HttpGet]
        public ActionResult GetResolutionList()
        {
            var data = primaryDAO.GetResolutionListForIndividualMP();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
       
        [HttpGet]
        public ActionResult GetSentResolutionList()
        {
            var data = primaryDAO.GetSentResolutionListForIndividualMP();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult InsertResolution(MemberResolutionInfo master)
        {
            try
            {
                if (primaryDAO.InsertResolution(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmResolutionEntry");
            }
            catch (Exception)
            {
                return View("frmResolutionEntry");
            }
        }

        [HttpPost]
        public ActionResult UpdateResolution(MemberResolutionInfo master)
        {
            try
            {
                if (primaryDAO.UpdateResolution(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmResolutionEntry");
            }
            catch (Exception)
            {
                return View("frmResolutionEntry");
            }
        }

    }
}