using PMRS_Mvc.Areas.Security.DAO;
using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Web.Mvc;

namespace PMRS_Mvc.Areas.Security.Controllers
{
    [LoginChecker]
    public class UserLoginController : Controller
    {
        #region User Login

        private UserLoginDAO primaryDAO = new UserLoginDAO();

        //
        // GET: /Security/UserLogin/
        public ActionResult frmLoginCreate()
        {
            return View();
        }

        public ActionResult GetUserLoginList()
        {
            var data = primaryDAO.GetUserLoginList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetRemainingEmployee(string empType)
        {
            var data = primaryDAO.GetRemainingEmployee(empType);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertUser(UserLogin master)
        {
            try
            {
                if (primaryDAO.InsertUser(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes" });
                }
                return View("frmLoginCreate");
            }
            catch (Exception)
            {
                return View("frmLoginCreate");
            }
        }

        [HttpPost]
        public ActionResult UpdateUser(UserLogin master)
        {
            try
            {
                if (primaryDAO.UpdateUser(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes" });
                }
                return View("frmLoginCreate");
            }
            catch (Exception)
            {
                return View("frmLoginCreate");
            }
        }

        #endregion User Login

        public ActionResult frmChangePass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckCurrentPassword(string currentPassword)
        {
            bool status = primaryDAO.CheckCurrentPassword(currentPassword);
            return Json(new { Stat = status }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdatePassword(string Password)
        {
            try
            {
                if (primaryDAO.UpdatePassword(Password))
                {
                    return Json(new { Mode = primaryDAO.IUMode, Status = "Yes" });
                }
                return View("frmChangePass");
            }
            catch (Exception)
            {
                return View("frmChangePass");
            }
        }
    }
}