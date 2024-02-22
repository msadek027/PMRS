using PMRS_Mvc.Areas.Transaction.DAO;
using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Web.Mvc;


namespace PMRS_Mvc.Areas.Transaction.Controllers
{
    [LoginChecker]
    public class DepartmentInfoController : Controller
    {
        private DepartmentInfoDAO primaryDAO = new DepartmentInfoDAO();

        public ActionResult frmDepartmentInfo()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetDepartmentInfoList()
        {
            var data = primaryDAO.GetDepartmentInfoList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetActiveDepartmentInfoList()
        {
            var data = primaryDAO.GetActiveDepartmentInfoList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertDepartment(DepartmentInfo master)
        {
            try
            {
                if (primaryDAO.InsertDepartment(master))
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
        public ActionResult UpdateDepartment(DepartmentInfo master)
        {
            try
            {
                if (primaryDAO.UpdateDepartment(master))
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