using PMRS_Mvc.Areas.Transaction.DAO;
using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Web.Mvc;


namespace PMRS_Mvc.Areas.Transaction
{
    [LoginChecker]
    public class ConstitutentMappingController : Controller
    {
        private ConstitutentMappingDAO primaryDAO = new ConstitutentMappingDAO();

        // GET: Transaction/ConstitutentMapping
        public ActionResult frmConstitutentMapping()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GetConstitutentMappingList()
        {
            var data = primaryDAO.GetConstitutentMappingList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult InsertConstitutentMapping(ConstitutentUserMappingInfo master)
        {
            try
            {
                if (primaryDAO.InsertConstitutentMapping(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmConstitutentMapping");
            }
            catch (Exception)
            {
                return View("frmConstitutentMapping");
            }
        }

        [HttpPost]
        public ActionResult UpdateConstitutentMapping(ConstitutentUserMappingInfo master)
        {
            try
            {
                if (primaryDAO.UpdateConstitutentMapping(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmConstitutentMapping");
            }
            catch (Exception)
            {
                return View("frmConstitutentMapping");
            }
        }
    }
}