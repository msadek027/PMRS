using PMRS_Mvc.Areas.Security.DAO;
using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Web.Mvc;


namespace PMRS_Mvc.Areas.Security.Controllers
{
    [LoginChecker]
    public class RLController : Controller
    {
        private RLDAO primaryDAO = new RLDAO();

        //
        // GET: /Security/RL/
        public ActionResult frmRL()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetRoleList()
        {
            var data = primaryDAO.GetRoleList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertRL(RL master)
        {
            try
            {
                if (primaryDAO.InsertRL(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes" });
                }
                return View("frmRL");
            }
            catch (Exception)
            {
                return View("frmRL");
            }
        }

        [HttpPost]
        public ActionResult UpdateRL(RL master)
        {
            try
            {
                if (primaryDAO.UpdateRL(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes" });
                }
                return View("frmRL");
            }
            catch (Exception)
            {
                return View("frmRL");
            }
        }
    }
}