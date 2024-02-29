﻿using PMRS_Mvc.Areas.PMRS.DAO;
using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Web.Mvc;

namespace PMRS_Mvc.Areas.PMRS.Controllers
{
    [LoginChecker]
    public class ResolutionApprovalController : Controller
    {
        private ResolutionApprovalDAO primaryDAO = new ResolutionApprovalDAO();

        // GET: PMRS/ResolutionApproval

        #region All Waiting List
        [HttpPost]
        public ActionResult GetWaitingListForAdministrative(string session, string DataMode)
        {
            var data = primaryDAO.GetWaitingListForAdministrative(Convert.ToInt32(session), DataMode);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetWaitingListForAssistantSecretary(string session,string DataMode)
        {
            var data = primaryDAO.GetWaitingListForAssistantSecretary(Convert.ToInt32(session), DataMode);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetWatingListForSrAssistantSecretary(string session, string DataMode)
        {
            var data = primaryDAO.GetWatingListForSrAssistantSecretary(Convert.ToInt32(session), DataMode);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetWaitingListForDeputySecretary(string session, string DataMode)
        {
            var data = primaryDAO.GetWaitingListForDeputySecretary(Convert.ToInt32(session), DataMode);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetWaitingListForAdditionalSecretary(string session, string DataMode)
        {
            var data = primaryDAO.GetWaitingListForAdditionalSecretary(Convert.ToInt32(session), DataMode);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetWaitingListForSecretary(string session, string DataMode)
        {
            var data = primaryDAO.GetWaitingListForSecretary(Convert.ToInt32(session), DataMode);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

      
        [HttpPost]
        public ActionResult GetWaitingListForSpeaker(string session)
        {
            var data = primaryDAO.GetWaitingListForSpeaker(Convert.ToInt32(session));
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region All Posted History List
        [HttpGet]
        public ActionResult GetPostedHistoryListByAdministrativeOfficer()
        {
            var data = primaryDAO.GetPostedHistoryListByAdministrativeOfficer();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetPostedHistoryListByAssistantSecretary()
        {
            var data = primaryDAO.GetPostedHistoryListByAssistantSecretary();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetPostedHistoryListBySrAssistantSecretary()
        {
            var data = primaryDAO.GetPostedHistoryListBySrAssistantSecretary();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetPostedHistoryListByDeputySecretary()
        {
            var data = primaryDAO.GetPostedHistoryListByDeputySecretary();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetPostedHistoryListByAdditionalSecretary()
        {
            var data = primaryDAO.GetPostedHistoryListByAdditionalSecretary();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetPostedHistoryListBySecretary()
        {
            var data = primaryDAO.GetPostedHistoryListBySecretary();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetPostedHistoryListBySpeaker()
        {
            var data = primaryDAO.GetPostedHistoryListBySpeaker();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Search For Edit All
        [HttpGet]
        public ActionResult GetSearchForEditAdministrativeOfficer()
        {
            var data = primaryDAO.GetSearchForEditAdministrativeOfficer();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
     
        [HttpGet]
        public ActionResult GetSearchForEditAssistantSecretary()
        {
            var data = primaryDAO.GetSearchForEditAssistantSecretary();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetSearchForEditSrAssistantSecretary()
        {
            var data = primaryDAO.GetSearchForEditSrAssistantSecretary();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetSearchForEditDeputySecretary()
        {
            var data = primaryDAO.GetSearchForEditDeputySecretary();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetSearchForEditAdditionalSecretary()
        {
            var data = primaryDAO.GetSearchForEditAdditionalSecretary();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetSearchForEditSecretary()
        {
            var data = primaryDAO.GetSearchForEditSecretary();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetSearchForEditSpeaker()
        {
            var data = primaryDAO.GetSearchForEditSpeaker();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Draft Administrative Off
        public ActionResult DraftAdministrativeOfficer(ResolutionApproval master)
        {
            try
            {
                master.AdministrativeOfcDetail = master.AdministrativeOfcDetail;
                master.RDNo = master.RDNo;
                master.ParlSessID = master.ParlSessID;
                master.AdministrativeOfcApproveStatus = "31";

                if (primaryDAO.UpdateAdministrativeApproval(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmResolutionApproval");
            }
            catch (Exception)
            {
                return View("frmResolutionApproval");
            }
        }
        public ActionResult DraftAssistantSecretary(ResolutionApproval master)
        {
            try
            {
                master.AssitantSccDetail = master.AssitantSccDetail;
                master.RDNo = master.RDNo;
                master.ParlSessID = master.ParlSessID;
                master.AssitantSccApproveStatus = "31";

                if (primaryDAO.UpdateAssistantSecApproval(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmResolutionApproval");
            }
            catch (Exception)
            {
                return View("frmResolutionApproval");
            }
        }
        public ActionResult DraftSrAssistantSecretary(ResolutionApproval master)
        {
            try
            {
                master.SrAssitantSccDetail = master.SrAssitantSccDetail;
                master.RDNo = master.RDNo;
                master.ParlSessID = master.ParlSessID;
                master.SrAssitantSccApproveStatus = "31";

                if (primaryDAO.UpdateSrAssistantSecApproval(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmResolutionApproval");
            }
            catch (Exception)
            {
                return View("frmResolutionApproval");
            }
        }
        public ActionResult DraftDeputySecretary(ResolutionApproval master)
        {
            try
            {
                master.DeputySecApproveDetail = master.DeputySecApproveDetail;
                master.RDNo = master.RDNo;
                master.ParlSessID = master.ParlSessID;
                master.DeputySecApproveStatus = "31";

                if (primaryDAO.UpdateDeputySecretaryApproval(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmResolutionApproval");
            }
            catch (Exception)
            {
                return View("frmResolutionApproval");
            }
        }
        public ActionResult DraftAddSecretary(ResolutionApproval master)
        {
            try
            {
                master.AddSecApproveDetail = master.AddSecApproveDetail;
                master.RDNo = master.RDNo;
                master.ParlSessID = master.ParlSessID;
                master.AddSecApproveStatus = "31";

                if (primaryDAO.UpdateAdditionalSecretaryApproval(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmResolutionApproval");
            }
            catch (Exception)
            {
                return View("frmResolutionApproval");
            }
        }
        public ActionResult DraftSecretary(ResolutionApproval master)
        {
            try
            {
                master.SecApproveDetail = master.SecApproveDetail;
                master.RDNo = master.RDNo;
                master.ParlSessID = master.ParlSessID;
                master.SecApproveStatus = "31";

                if (primaryDAO.UpdateSecretaryApproval(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmResolutionApproval");
            }
            catch (Exception)
            {
                return View("frmResolutionApproval");
            }
        }
        #endregion

        #region Administrative Off
        public ActionResult frmAdministrativeOfficerApproval()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult SaveAdministrativeApproval(ResolutionApproval master)
        {
            try
            {
                if(Session["Signature"] != null)
                {
                    master.AdministrativeOfcSignature = Session["Signature"].ToString();
                }
             
                if (primaryDAO.SaveAdministrativeApproval(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmResolutionApproval");
            }
            catch (Exception)
            {
                return View("frmResolutionApproval");
            }
        }
        public ActionResult UpdateAdministrativeApproval(ResolutionApproval master)
        {
            try
            {
                string resolutionDetail = master.AdministrativeOfcDetail;
                if (Session["Signature"] != null)
                {
                    master.AdministrativeOfcSignature = Session["Signature"].ToString();
                }
                if (master.AdministrativeOfcApproveStatus == "1" || master.AdministrativeOfcApproveStatus == "7" || master.AdministrativeOfcApproveStatus == "8" || master.AdministrativeOfcApproveStatus == "10")
                {
                    UpdateResolutionStatus(resolutionDetail,master);
                }
                if (primaryDAO.UpdateAdministrativeApproval(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmResolutionApproval");
            }
            catch (Exception)
            {
                return View("frmResolutionApproval");
            }
        }
        //[HttpGet]
        //public ActionResult GetDraftAdministrativeOfficer()
        //{
        //    var data = primaryDAO.GetDraftAdministrativeOfficer();
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
       
        //[HttpPost]
        //public ActionResult SaveDraftAdministrativeOfficer(ResolutionApproval master)
        //{
        //    try
        //    {
        //        if (primaryDAO.UpdateDraftAdministrativeOfficer(master))
        //        {
        //            return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
        //        }
        //        return View("frmResolutionApproval");
        //    }
        //    catch (Exception)
        //    {
        //        return View("frmResolutionApproval");
        //    }
        //}
        #endregion
        #region Assistant Secretary
        public ActionResult frmAssistantSecretaryApproval()
        {
            return View();
        }
       
        //[HttpPost]
        //public ActionResult SaveAssistantSecApproval(ResolutionApproval master)
        //{
        //    try
        //    {
        //        if (Session["Signature"] != null)
        //        {
        //            master.AssitantSccSignature= Session["Signature"].ToString();
        //        }

        //        if (primaryDAO.SaveAdministrativeApproval(master))
        //        {
        //            return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
        //        }
        //        return View("frmResolutionApproval");
        //    }
        //    catch (Exception)
        //    {
        //        return View("frmResolutionApproval");
        //    }
        //}
        public ActionResult UpdateAssistantSecApproval(ResolutionApproval master)
        {
            try
            {
                string resolutionDetail = master.AssitantSccDetail;
                if (Session["Signature"] != null)
                {
                    master.AssitantSccSignature = Session["Signature"].ToString();
                }
                if (master.AssitantSccApproveStatus == "1" || master.AssitantSccApproveStatus == "7" || master.AssitantSccApproveStatus == "8" || master.AssitantSccApproveStatus == "10")
                {
                    UpdateResolutionStatus(resolutionDetail,master);
                }
                if (primaryDAO.UpdateAssistantSecApproval(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmAssistantSecApproval");
            }
            catch (Exception)
            {
                return View("frmAssistantSecApproval");
            }
        }
     
        //[HttpGet]
        //public ActionResult GetDraftAssistantSecretry()
        //{
        //    var data = primaryDAO.GetDraftAssistantSecretary();
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
        #endregion
        #region Sr. Assistant Secretary
        public ActionResult frmSrAssistantSecretaryApproval()
        {
            return View();
        }

      
       
        //[HttpPost]
        //public ActionResult SaveSrAssistantSecApproval(ResolutionApproval master)
        //{
        //    try
        //    {
        //        if (Session["Signature"] != null)
        //        {
        //            master.SrAssitantSccSignature = Session["Signature"].ToString();
        //        }
            
        //        if (primaryDAO.SaveSrAssistantSecApproval(master))
        //        {
        //            return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
        //        }
        //        return View("frmResolutionApproval");
        //    }
        //    catch (Exception)
        //    {
        //        return View("frmResolutionApproval");
        //    }
        //}
        public ActionResult UpdateSrAssistantSecApproval(ResolutionApproval master)
        {
            try
            {
                string resolutionDetail = master.SrAssitantSccDetail;
                if (Session["Signature"] != null)
                {
                    master.SrAssitantSccSignature = Session["Signature"].ToString();
                }
                if (master.SrAssitantSccApproveStatus == "1" || master.SrAssitantSccApproveStatus == "7" || master.SrAssitantSccApproveStatus == "8" || master.SrAssitantSccApproveStatus == "10")
                {
                    UpdateResolutionStatus(resolutionDetail,master);
                }
                if (primaryDAO.UpdateSrAssistantSecApproval(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmAssistantSecApproval");
            }
            catch (Exception)
            {
                return View("frmAssistantSecApproval");
            }
        }
        
      
        //[HttpGet]
        //public ActionResult GetDraftSrAssistantSecretry()
        //{
        //    var data = primaryDAO.GetDraftSrAsistintSecretary();
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        #region Deputy Secretary
        public ActionResult frmDeputySecretaryApproval()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult GetResolutionLog(string ResolutionID)
        {
            var data = primaryDAO.GetResolutionLog(Convert.ToInt32(ResolutionID));
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public ActionResult GetApprovalListByDS()
        //{
        //    var data = primaryDAO.GetApprovalListByDS();
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //public ActionResult GetSentApprovalListByDS()
        //{
        //    var data = primaryDAO.GetSentApprovalListByDS();
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        public ActionResult GetApprovalStatus()
        {
            var data = primaryDAO.GetApprovalStatus();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public ActionResult GetApprovalListForDS(string session)
        //{
        //    var data = primaryDAO.GetApprovalListForDS(Convert.ToInt32(session));
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //public ActionResult GetDraftDeputySecretary()
        //{
        //    var data = primaryDAO.GetDraftDeputySecretary();
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public ActionResult SaveDSApproval(ResolutionApproval master)
        //{
        //    try
        //    {
        //        if(Session["Signature"] != null)
        //        {
        //            master.DeputySecSignature = Session["Signature"].ToString();
        //        }
                
        //        if (primaryDAO.SaveDeputySecretaryApproval(master))
        //        {
        //            return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
        //        }
        //        return View("frmResolutionApproval");
        //    }
        //    catch (Exception)
        //    {
        //        return View("frmResolutionApproval");
        //    }
        //}

        [HttpPost]
        public ActionResult UpdateDSApproval(ResolutionApproval master)
        {
            try
            {
                string resolutionDetail = master.DeputySecApproveDetail;
                if (Session["Signature"] != null)
                {
                    master.DeputySecSignature = Session["Signature"].ToString();
                }
                if (master.DeputySecApproveStatus == "1" || master.DeputySecApproveStatus == "7" || master.DeputySecApproveStatus == "8" || master.DeputySecApproveStatus == "10")
                {
                    UpdateResolutionStatus(resolutionDetail,master);
                }
                if (primaryDAO.UpdateDeputySecretaryApproval(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmResolutionApproval");
            }
            catch (Exception)
            {
                return View("frmResolutionApproval");
            }
        }

        //[HttpPost]
        //public ActionResult SaveDSDraft(ResolutionApproval master)
        //{
        //    try
        //    {
        //        if (primaryDAO.UpdateDSDraft(master))
        //        {
        //            return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
        //        }
        //        return View("frmResolutionApproval");
        //    }
        //    catch (Exception)
        //    {
        //        return View("frmResolutionApproval");
        //    }
        //}

        #endregion





        #region Additional Secretary
        public ActionResult frmAdditionalSecretaryApproval()
        {
            return View();
        }
       
        //[HttpGet]
        //public ActionResult GetDraftAdditionalSecretary()
        //{
        //    var data = primaryDAO.GetDraftAdditionalSecretary();
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
        //[HttpGet]
        //public ActionResult GetApprovalListByAS()
        //{
        //    var data = primaryDAO.GetApprovalListByAS();
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
        //[HttpGet]
        //public ActionResult GetSentApprovalListByAS()
        //{
        //    var data = primaryDAO.GetSentApprovalListByAS();
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
 

        [HttpPost]
        public ActionResult UpdateASApproval(ResolutionApproval master)
        {
            try
            {
                string resolutionDetail = master.AddSecApproveDetail;
                if (Session["Signature"] != null)
                {
                    master.AddSecSignature = Session["Signature"].ToString();
                }
                if (master.AddSecApproveStatus == "1" || master.AddSecApproveStatus == "7" || master.AddSecApproveStatus == "8" || master.AddSecApproveStatus == "10")
                {
                    UpdateResolutionStatus(resolutionDetail,master);
                }
               
       
                if (primaryDAO.UpdateAdditionalSecretaryApproval(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmResolutionASApproval");
            }
            catch (Exception)
            {
                return View("frmResolutionASApproval");
            }
        }

        //[HttpPost]
        //public ActionResult SaveASDraft(ResolutionApproval master)
        //{
        //    try
        //    {
        //        if (primaryDAO.UpdateASDraft(master))
        //        {
        //            return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
        //        }
        //        return View("frmResolutionASApproval");
        //    }
        //    catch (Exception)
        //    {
        //        return View("frmResolutionASApproval");
        //    }
        //}
        

        #endregion
        #region Secretary

        public ActionResult frmSecretaryApproval()
        {
            return View();
        }

        //[HttpGet]
        //public ActionResult GetDraftSecretary()
        //{
        //    var data = primaryDAO.GetDraftSecretary();
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //public ActionResult GetApprovalListBySec()
        //{
        //    var data = primaryDAO.GetApprovalListBySec();
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
   

        [HttpPost]
        public ActionResult UpdateSecApproval(ResolutionApproval master)
        {
            try
            {
                string resolutionDetail = master.SecApproveDetail;
                if (Session["Signature"] != null)
                {
                    master.SecSignature = Session["Signature"].ToString();
                }
                if ( master.SecApproveStatus == "1" || master.SecApproveStatus == "7" || master.SecApproveStatus == "8" || master.SecApproveStatus == "10")
                {
                    UpdateResolutionStatus(resolutionDetail,master);
                }
                if (primaryDAO.UpdateSecretaryApproval(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmResolutionSecApproval");
            }
            catch (Exception)
            {
                return View("frmResolutionSecApproval");
            }
        }

        //[HttpPost]
        //public ActionResult SaveSecDraft(ResolutionApproval master)
        //{
        //    try
        //    {
        //        if (primaryDAO.UpdateSecDraft(master))
        //        {
        //            return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
        //        }
        //        return View("frmResolutionSecApproval");
        //    }
        //    catch (Exception)
        //    {
        //        return View("frmResolutionSecApproval");
        //    }
        //}

        #endregion

        #region Speaker
        public ActionResult frmSpeakerApproval()
        {
            return View();
        }
        //[HttpGet]
        //public ActionResult GetApprovalListBySpeaker()
        //{
        //    var data = primaryDAO.GetApprovalListBySpeaker();
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //public ActionResult GetSpeakerDraft()
        //{
        //    var data = primaryDAO.GetSpeakerDraft();
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}



        [HttpPost]
        public ActionResult UpdateSpeakerApproval(ResolutionApproval master)
        {
            try
            {
                string resolutionDetail = master.SpeakerApproveDetail;
                master.DataMode = "Backward";
                if(Session["Signature"] != null)
                {
                    master.SpeakerSignature = Session["Signature"].ToString();
                }
                if (master.SpeakerApproveStatus == "1" || master.SpeakerApproveStatus == "7" || master.SpeakerApproveStatus == "8" || master.SpeakerApproveStatus == "10")
                {
                    UpdateResolutionStatus(resolutionDetail,master);
                }
                if (primaryDAO.UpdateSpeakerApproval(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmResolutionSpeakerApproval");
            }
            catch (Exception)
            {
                return View("frmResolutionSpeakerApproval");
            }
        }


        //[HttpPost]
        //public ActionResult SaveSpeakerDraft(ResolutionApproval master)
        //{
        //    try
        //    {
        //        if (primaryDAO.UpdateSpeakerDraft(master))
        //        {
        //            return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
        //        }
        //        return View("frmResolutionSpeakerApproval");
        //    }
        //    catch (Exception)
        //    {
        //        return View("frmResolutionSpeakerApproval");
        //    }
        //}

        #endregion

        public void UpdateResolutionStatus(string resolutionDetail,ResolutionApproval master)
        {
            ResolutionApproval approval = new ResolutionApproval();
            approval.ResolutionApproveID = master.ResolutionApproveID;
            approval.MemberResolutionID = master.MemberResolutionID;

            if (master.DataMode == "Forward")
            {
                if (master.SendTo == "10")
                {

                    approval.AdministrativeOfcDetail = resolutionDetail;
                    approval.RDNo = master.RDNo;
                    approval.ParlSessID = master.ParlSessID;
                    approval.AdministrativeOfcApproveStatus = "0";
                    primaryDAO.UpdateAdministrativeApproval(approval);

                }

                if (master.SendTo == "9")
                {

                    approval.AssitantSccDetail = resolutionDetail;
                    approval.RDNo = master.RDNo;
                    approval.ParlSessID = master.ParlSessID;
                    approval.AssitantSccApproveStatus = "0";
                    primaryDAO.UpdateAssistantSecApproval(approval);

                }
                if (master.SendTo == "6")
                {

                    approval.SrAssitantSccDetail = resolutionDetail;
                    approval.RDNo = master.RDNo;
                    approval.ParlSessID = master.ParlSessID;
                    approval.SrAssitantSccApproveStatus = "0";
                    primaryDAO.UpdateSrAssistantSecApproval(approval);

                }


                if (master.SendTo == "3")
                {

                    approval.DeputySecApproveDetail = resolutionDetail;
                    approval.RDNo = master.RDNo;
                    approval.ParlSessID = master.ParlSessID;
                    approval.DeputySecApproveStatus = "0";
                    primaryDAO.UpdateDeputySecretaryApproval(approval);

                }

                if (master.SendTo == "2")
                {

                    approval.AddSecApproveDetail = resolutionDetail;
                    approval.RDNo = master.RDNo;
                    approval.ParlSessID = master.ParlSessID;

                    approval.AddSecApproveStatus = "0";
                    primaryDAO.UpdateAdditionalSecretaryApproval(approval);

                }
                if (master.SendTo == "1")
                {

                    approval.SecApproveDetail = resolutionDetail;
                    approval.RDNo = master.RDNo;
                    approval.ParlSessID = master.ParlSessID;
                    approval.SecApproveDate = DateTime.Now;
                    approval.SecApproveStatus = "0";
                    primaryDAO.UpdateSecretaryApproval(approval);


                }
                if (master.SendTo == "0")
                {

                    approval.SpeakerApproveDetail = resolutionDetail;
                    approval.RDNo = master.RDNo;
                    approval.ParlSessID = master.ParlSessID;
                    approval.SpeakerApproveDate = DateTime.Now;

                    approval.SpeakerApproveStatus = "0";
                    primaryDAO.UpdateSpeakerApproval(approval);

                }
            }
           else if (master.DataMode == "Backward")
            {
                if (master.SendTo == "10")
                {

                    approval.AdministrativeOfcDetail = resolutionDetail;
                    approval.RDNo = master.RDNo;
                    approval.ParlSessID = master.ParlSessID;
                    approval.AdministrativeOfcApproveStatus = "1";
                    approval.AdministrativeOfcBackStatus = "0";
                    primaryDAO.UpdateAdministrativeApproval(approval);

                }

                if (master.SendTo == "9")
                {

                    approval.AssitantSccDetail = resolutionDetail;
                    approval.RDNo = master.RDNo;
                    approval.ParlSessID = master.ParlSessID;
                    approval.AssitantSccApproveStatus = "1";
                    approval.AssitantSccBackStatus = "0";
                    primaryDAO.UpdateAssistantSecApproval(approval);

                }
                if (master.SendTo == "6")
                {

                    approval.SrAssitantSccDetail = resolutionDetail;
                    approval.RDNo = master.RDNo;
                    approval.ParlSessID = master.ParlSessID;
                    approval.SrAssitantSccApproveStatus = "1";
                    approval.SrAssitantSccBackStatus = "0";
                    primaryDAO.UpdateSrAssistantSecApproval(approval);

                }


                if (master.SendTo == "3")
                {

                    approval.DeputySecApproveDetail = resolutionDetail;
                    approval.RDNo = master.RDNo;
                    approval.ParlSessID = master.ParlSessID;
                    approval.DeputySecApproveStatus = "1";
                    approval.DeputySecBackStatus = "0";
                    primaryDAO.UpdateDeputySecretaryApproval(approval);

                }

                if (master.SendTo == "2")
                {

                    approval.AddSecApproveDetail = resolutionDetail;
                    approval.RDNo = master.RDNo;
                    approval.ParlSessID = master.ParlSessID;

                    approval.AddSecApproveStatus = "1";
                    approval.AddSecBackStatus = "0";
                    primaryDAO.UpdateAdditionalSecretaryApproval(approval);

                }
                if (master.SendTo == "1")
                {

                    approval.SecApproveDetail = resolutionDetail;
                    approval.RDNo = master.RDNo;
                    approval.ParlSessID = master.ParlSessID;
                    approval.SecApproveDate = DateTime.Now;
                    approval.SecApproveStatus = "1";
                    approval.SecBackStatus = "0";
                    primaryDAO.UpdateSecretaryApproval(approval);


                }
                if (master.SendTo == "0")
                {

                    approval.SpeakerApproveDetail = resolutionDetail;
                    approval.RDNo = master.RDNo;
                    approval.ParlSessID = master.ParlSessID;
                    approval.SpeakerApproveDate = DateTime.Now;

                    approval.SpeakerApproveStatus = "0";
                    approval.SpeakerBackStatus = "1";
                    primaryDAO.UpdateSpeakerApproval(approval);

                }
                if (master.SendTo == "100")
                { 
                    approval.NoticeBackStatus = "0";
                    primaryDAO.UpdateNofificationStatus(approval);

                }
               
            }
        }

    }
}