using PMRS_Mvc.Areas.Transaction.DAO;
using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Web.Mvc;

namespace PMRS_Mvc.Areas.Transaction.Controllers
{
    [LoginChecker]
    public class DesignationInfoController : Controller
    {
        private DesignationInfoDAO primaryDAO = new DesignationInfoDAO();

        public ActionResult frmDesignationInfo()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetDesignationInfoList()
        {
            var data = primaryDAO.GetDesignationInfoList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetActiveDesignationInfoList()
        {
            var data = primaryDAO.GetActiveDesignationInfoList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertDesignation(DesignationInfo master)
        {
            try
            {
                if (primaryDAO.InsertDesignation(master) != 0)
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmDesignationInfo");
            }
            catch (Exception)
            {
                return View("frmDesignationInfo");
            }
        }

        [HttpPost]
        public ActionResult UpdateDesignation(DesignationInfo master)
        {
            try
            {
                if (primaryDAO.UpdateDesignation(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmDesignationInfo");
            }
            catch (Exception)
            {
                return View("frmDesignationInfo");
            }
        }
    }
}