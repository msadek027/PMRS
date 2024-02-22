using PMRS_Mvc.Areas.Security.DAO;
using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Web.Mvc;

namespace PMRS_Mvc.Areas.Security.Controllers
{
    [LoginChecker]
    public class MNConfController : Controller
    {
        private MNConfDAO _dao = new MNConfDAO();

        //
        // GET: /Security/MNConf/
        public ActionResult frmMNConf()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetMNConfListByMHRL(string RL_ID, string MH_ID)
        {
            var data = _dao.GetMNConfListByMHRL(Convert.ToInt32(RL_ID), Convert.ToInt32(MH_ID));
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InsertMNConf(MNConf ast)
        {
            if (_dao.InsertMNConf(ast))
            {
                return Json(new { Code = _dao.MaxCode, ID = _dao.MaxID, Mode = _dao.IUMode, Status = "Yes" });
            }

            return Json(new { Status = "No" });
        }

        public ActionResult UpdateMNConf(MNConf ast)
        {
            if (_dao.UpdateMNConf(ast))
            {
                return Json(new { Code = _dao.MaxCode, ID = _dao.MaxID, Mode = _dao.IUMode, Status = "Yes" });
            }

            return Json(new { Status = "No" });
        }

        public ActionResult DeleteMNConf(int ID)
        {
            if (_dao.DeleteMNConf(ID))
            {
                return Json(new { Code = _dao.MaxCode, ID = _dao.MaxID, Mode = _dao.IUMode, Status = "Yes" });
            }

            return Json(new { Status = "No" });
        }
    }
}