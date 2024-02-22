using PMRS_Mvc.Areas.Transaction.DAO;
using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Web.Mvc;


namespace PMRS_Mvc.Areas.Transaction.Controllers
{
    [LoginChecker]
    public class ConstitutentInfoController : Controller
    {
        private ConstitutentInfoDAO primaryDAO = new ConstitutentInfoDAO();

        // GET: Transaction/ConstitutentInfo
        public ActionResult frmConstitutentInfo()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetConstitutentInfoList()
        {
            var data = primaryDAO.GetConstitutentInfoList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetActiveConstitutentInfoList()
        {
            var data = primaryDAO.GetActiveConstitutentInfoList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertConstitutent(ConstitutentInfo master)
        {
            try
            {
                if (primaryDAO.InsertConstitutent(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmDepartmentInfo");
            }
            catch (Exception)
            {
                return View("frmDepartmentInfo");
            }
        }

        [HttpPost]
        public ActionResult UpdateConstitutent(ConstitutentInfo master)
        {
            try
            {
                if (primaryDAO.UpdateConstitutent(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmDepartmentInfo");
            }
            catch (Exception)
            {
                return View("frmDepartmentInfo");
            }
        }
    }
}