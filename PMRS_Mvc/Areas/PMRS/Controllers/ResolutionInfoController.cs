using Newtonsoft.Json;
using PMRS_Mvc.Areas.PMRS.DAO;
using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Mvc;

namespace PMRS_Mvc.Areas.PMRS.Controllers
{
    [LoginChecker]
    public class ResolutionInfoController : Controller
    {
        private ResolutionInfoDAO primaryDAO = new ResolutionInfoDAO();
        private ResolutionApprovalDAO primaryApprDAO = new ResolutionApprovalDAO();
        private PMRS_BcEntities _db = new PMRS_BcEntities();

        // GET: PMRS/ResolutionInfo
        public ActionResult frmResolutionInfo()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetWorkflow()
        {
            var data = primaryDAO.GetWorkflow();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetResolutionList()
        {
            var data = primaryDAO.GetResolutionList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetDraftResolutionList()
        {
            var data = primaryDAO.GetDraftResolutionList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetPostedHistoryList()
        {
            var data = primaryDAO.GetSentResolutionList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

      
     
        [HttpPost]
        public ActionResult InsertResolution(MemberResolutionInfo master)
        {
            try
            {
                if (primaryDAO.InsertResolution(master))
                {

                    List<SqlParameter> parms = new List<SqlParameter>
                    {
                        new SqlParameter("@EmpID", master.UserID),
                        new SqlParameter("@Today", master.MemberResolutionDate),
                        new SqlParameter("@r", SqlDbType.VarChar,200){ Direction = ParameterDirection.Output }
                    };

                    var ret = _db.Database.ExecuteSqlCommand("EXEC [ResolutionCountForMP] @EmpID, @Today, @r OUTPUT", parms.ToArray());

                    int cnt = Convert.ToInt32(parms[2].Value.ToString());

                    if (cnt < 25)
                    {
                        return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                    }
                    else
                    {
                        return Json(new { Code = 0, Mode = "Count Exceed", Status = "No", ID = 0 });
                    }


                }
                return View("frmResolutionInfo");
            }
            catch (Exception ex)
            {
                //string filePath = Server.MapPath("~/App_Data/Errorfile.txt"); 
                //using (StreamWriter writer = new StreamWriter(filePath, true))
                //{
                //    writer.WriteLine(ex.Message.ToString());
                //}
                //string s = ex.Message.ToString();
                //return View("frmResolutionInfo");

                ViewBag.Error = ex.Message.ToString();
                Session["ErrorMessage"] = ex.Message.ToString();
                return View("frmResolutionInfo");
            }
        }

        [HttpPost]
        public ActionResult UpdateResolution(MemberResolutionInfo master)
        {
            try
            {
                if(!string.IsNullOrEmpty( master.SendTo))
                {
                    ResolutionApproval approval = new ResolutionApproval();
                    approval.MemberResolutionID = master.MemberResolutionID;
                 

                    if (master.SendTo == "10")
                    {
                        approval.AdministrativeOfcDetail = master.MemberResolutionDetail;
                        approval.RDNo = master.RDNo;
                        approval.ParlSessID = master.ParlSessID;
                        approval.AdministrativeOfcApproveStatus = "0";
                        //primaryApprDAO.SaveAdministrativeApproval(approval);
                    }

                    if (master.SendTo == "9")
                    {
                        approval.AssitantSccDetail = master.MemberResolutionDetail;
                        approval.RDNo = master.RDNo;
                        approval.ParlSessID = master.ParlSessID;
                        approval.AssitantSccApproveStatus = "0";
                        primaryApprDAO.SaveAssistantApproval(approval);
                    }
                    if (master.SendTo == "6")
                    {
                        approval.SrAssitantSccDetail = master.MemberResolutionDetail;
                        approval.RDNo = master.RDNo;
                        approval.ParlSessID = master.ParlSessID;
                        approval.SrAssitantSccApproveStatus = "0";
                        primaryApprDAO.SaveSrAssistantSecApproval(approval);
                    }


                    if (master.SendTo == "3")
                    {
                        approval.DeputySecApproveDetail = master.MemberResolutionDetail;
                        approval.RDNo = master.RDNo;
                        approval.ParlSessID = master.ParlSessID;
                        approval.DeputySecApproveStatus = "0";
                        primaryApprDAO.SaveDeputySecretaryApproval(approval);
                    }
                 
                    if (master.SendTo == "2")
                    {
                        approval.AddSecApproveDetail = master.MemberResolutionDetail;
                        approval.RDNo = master.RDNo;
                        approval.ParlSessID = master.ParlSessID;
                    
                        approval.AddSecApproveStatus = "0";
                          primaryApprDAO.SaveAdditionalSecApproval(approval);
                    }
                    if (master.SendTo == "1")
                    {
                        approval.SecApproveDetail = master.MemberResolutionDetail;
                        approval.RDNo = master.RDNo;
                        approval.ParlSessID = master.ParlSessID;
                        approval.SecApproveDate = DateTime.Now;
                        approval.SecApproveStatus = "0";
                        primaryApprDAO.SaveSecApproval(approval);

                    }
                    if (master.SendTo == "0")
                    {
                        approval.SpeakerApproveDetail = master.MemberResolutionDetail;
                        approval.RDNo = master.RDNo;
                        approval.ParlSessID = master.ParlSessID;
                        approval.SpeakerApproveDate = DateTime.Now;

                        approval.SpeakerApproveStatus = "0";
                        primaryApprDAO.SaveSpeakerApproval(approval);
                    }
                   
                }
       
                if (primaryDAO.UpdateResolution(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmResolutionInfo");
            }
            catch (Exception)
            {
                return View("frmResolutionInfo");
            }
        }
        [HttpPost]
        public ActionResult DraftResolution(MemberResolutionInfo master)
        {
            try
            {
               
                master.IsDraft = true;
                if (primaryDAO.DraftResolution(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmResolutionInfo");
            }
            catch (Exception)
            {
                return View("frmResolutionInfo");
            }
        }
        [HttpPost]
        public ActionResult UpdateUpload(string ID, string filepath)
        {
            try
            {
                if (primaryDAO.UpdateUpload(Convert.ToInt32(ID), filepath))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmResolutionInfo");
            }
            catch (Exception)
            {
                return View("frmResolutionInfo");
            }
        }
    }
}