using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Web.Mvc;

namespace PMRS_Mvc.Areas.Security.DAO
{
    [LoginChecker]
    public class ROLConfController : Controller
    {
        private ROLConfDAO primaryDAO = new ROLConfDAO();

        //
        // GET: /Security/ROLConf/
        public ActionResult frmROLConf()
        {
            return View();
        }

        public ActionResult SaveRLConf(RLConf master)
        {
            try
            {
                if (primaryDAO.SaveRLConf(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes" });
                }
                return View("frmROLConf");
            }
            catch (Exception)
            {
                return View("frmROLConf");
            }
        }
    }
}