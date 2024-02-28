using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PMRS_Mvc.Areas.PMRS.DAO
{
    public class ResolutionApprovalDAO : ReturnData
    {
        private readonly AuditTrailLogger _adt = new AuditTrailLogger();
        int userID = Convert.ToInt32(HttpContext.Current.Session["empID"]);
        public object GetWaitingListForAdministrative(int sessionID, string DataMode)
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                IQueryable<object> query;

                if (DataMode == "Forward")
                {
                    query = (from t in db.MemberResolutionInfoes
                             join em in db.EmployeeInfoes on t.UserID equals em.UserID
                             join r in db.ResolutionApprovals on t.MemberResolutionID equals r.MemberResolutionID
                             join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                             join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                             join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                             where prl.ParliamentSessionID == sessionID && t.AcceptStatus == "true" && map.ParliamentNo == prl.ParliamentNo.ToString() && t.SendTo == "10"
                                   && new[] { "0" }.Contains(r.AdministrativeOfcApproveStatus)
                             select new
                             {
                                 t.MemberResolutionID,
                                 r.ResolutionApproveID,
                                 html = t.MemberResolutionDetail,
                                 t.MemberResolutionDetail,
                                 t.MemberResolutionDate,
                                 t.MemberResolutionFIleURL,
                                 t.ParlSessID,
                                 t.AcceptStatus,
                                 t.EntryType,
                                 cnt.ConstitutentBangla,
                                 prl.ParliamentNo,
                                 prl.SessionNo,
                                 t.UserID,
                                 t.RDNo,
                                 t.AcceptanceComment,
                                 em.UserName,
                                 em.BanglaName,
                                 t.Status,
                                 r.AdministrativeOfcSignature,
                                 r.AssitantSccSignature,
                                 r.SrAssitantSccSignature,
                                 r.DeputySecSignature,
                                 r.SecSignature,
                                 r.AddSecSignature,
                                 r.SpeakerSignature,
                             });
                }
                else if (DataMode == "Backward")
                {
                    query = (from t in db.MemberResolutionInfoes
                             join em in db.EmployeeInfoes on t.UserID equals em.UserID
                             join r in db.ResolutionApprovals on t.MemberResolutionID equals r.MemberResolutionID
                             join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                             join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                             join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                             where prl.ParliamentSessionID == sessionID && t.AcceptStatus == "true" && map.ParliamentNo == prl.ParliamentNo.ToString() && t.SendTo == "10"
                                   && new[] { "0" }.Contains(r.AdministrativeOfcBackStatus)
                             select new
                             {
                                 t.MemberResolutionID,
                                 r.ResolutionApproveID,
                                 html = t.MemberResolutionDetail,
                                 t.MemberResolutionDetail,
                                 t.MemberResolutionDate,
                                 t.MemberResolutionFIleURL,
                                 t.ParlSessID,
                                 t.AcceptStatus,
                                 t.EntryType,
                                 cnt.ConstitutentBangla,
                                 prl.ParliamentNo,
                                 prl.SessionNo,
                                 t.UserID,
                                 t.RDNo,
                                 t.AcceptanceComment,
                                 em.UserName,
                                 em.BanglaName,
                                 t.Status,
                                 r.AdministrativeOfcSignature,
                                 r.AssitantSccSignature,
                                 r.SrAssitantSccSignature,
                                 r.DeputySecSignature,
                                 r.SecSignature,
                                 r.AddSecSignature,
                                 r.SpeakerSignature,
                             });
                }
                else if (DataMode == "Draft")
                {
                    query = (from t in db.MemberResolutionInfoes
                             join em in db.EmployeeInfoes on t.UserID equals em.UserID
                             join r in db.ResolutionApprovals on t.MemberResolutionID equals r.MemberResolutionID
                             join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                             join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                             join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                             where /* prl.ParliamentSessionID == sessionID && */ t.AcceptStatus == "true" && map.ParliamentNo == prl.ParliamentNo.ToString() && t.SendTo == "10"
                                   && new[] { "31" }.Contains(r.AdministrativeOfcApproveStatus)
                             select new
                             {
                                 t.MemberResolutionID,
                                 r.ResolutionApproveID,
                                 html = t.MemberResolutionDetail,
                                 t.MemberResolutionDetail,
                                 t.MemberResolutionDate,
                                 t.MemberResolutionFIleURL,
                                 t.ParlSessID,
                                 t.AcceptStatus,
                                 t.EntryType,
                                 cnt.ConstitutentBangla,
                                 prl.ParliamentNo,
                                 prl.SessionNo,
                                 t.UserID,
                                 t.RDNo,
                                 t.AcceptanceComment,
                                 em.UserName,
                                 em.BanglaName,
                                 t.Status,
                                 r.AdministrativeOfcSignature,
                                 r.AssitantSccSignature,
                                 r.SrAssitantSccSignature,
                                 r.DeputySecSignature,
                                 r.SecSignature,
                                 r.AddSecSignature,
                                 r.SpeakerSignature,
                             });
                }
                else
                {
                    return null; // Handle other cases or return empty list
                }

                var thList = query.ToList();
                return thList;
            }
        }



        public object GetWaitingListForAssistantSecretary(int sessionID, string DataMode)
        {
          
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                if (DataMode == "Forward")
                {
                    var thList = (from t in db.MemberResolutionInfoes
                                  join em in db.EmployeeInfoes on t.UserID equals em.UserID
                                  join r in db.ResolutionApprovals on t.MemberResolutionID equals r.MemberResolutionID
                                  join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                                  join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                                  join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                           
                                  where t.AcceptStatus == "true" && map.ParliamentNo == prl.ParliamentNo.ToString() && r.ParlSessID == sessionID 
                                   && new[] { "0" }.Contains(r.AssitantSccApproveStatus)
                                  select new
                                  {
                                      t.MemberResolutionID,
                                      r.ResolutionApproveID,
                                      html = r.AdministrativeOfcDetail,
                                      t.MemberResolutionDetail,
                                      t.MemberResolutionDate,
                                      t.MemberResolutionFIleURL,
                                      t.ParlSessID,
                                      t.AcceptStatus,
                                      t.EntryType,
                                      cnt.ConstitutentBangla,
                                      prl.ParliamentNo,
                                      prl.SessionNo,
                                      t.UserID,
                                      t.RDNo,
                                      t.AcceptanceComment,
                                      em.UserName,
                                      em.BanglaName,
                                      t.Status,

                                      r.AdministrativeOfcSignature,
                                      r.AssitantSccSignature,
                                      r.SrAssitantSccSignature,
                                      r.DeputySecSignature,
                                      r.SecSignature,
                                      r.AddSecSignature,
                                      r.SpeakerSignature,
                                  }).ToList();
                    return thList;
                }
                else if (DataMode == "Backward")
                {
                    var thList = (from t in db.MemberResolutionInfoes
                                  join em in db.EmployeeInfoes on t.UserID equals em.UserID
                                  join r in db.ResolutionApprovals on t.MemberResolutionID equals r.MemberResolutionID
                                  join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                                  join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                                  join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                                  //join apr in db.ApprovalStatus on r.AssitantSccApproveStatus equals apr.Status
                                  where t.AcceptStatus == "true" && map.ParliamentNo == prl.ParliamentNo.ToString() && r.ParlSessID == sessionID
                                   && new[] { "0" }.Contains(r.AssitantSccBackStatus)
                                  select new
                                  {
                                      t.MemberResolutionID,
                                      r.ResolutionApproveID,
                                      html = r.AdministrativeOfcDetail,
                                      t.MemberResolutionDetail,
                                      t.MemberResolutionDate,
                                      t.MemberResolutionFIleURL,
                                      t.ParlSessID,
                                      t.AcceptStatus,
                                      t.EntryType,
                                      cnt.ConstitutentBangla,
                                      prl.ParliamentNo,
                                      prl.SessionNo,
                                      t.UserID,
                                      t.RDNo,
                                      t.AcceptanceComment,
                                      em.UserName,
                                      em.BanglaName,
                                      t.Status,

                                      r.AdministrativeOfcSignature,
                                      r.AssitantSccSignature,
                                      r.SrAssitantSccSignature,
                                      r.DeputySecSignature,
                                      r.SecSignature,
                                      r.AddSecSignature,
                                      r.SpeakerSignature,
                                  }).ToList();
                    return thList;
                }
                else if (DataMode == "Draft")
                {
                    var thList = (from t in db.MemberResolutionInfoes
                                  join em in db.EmployeeInfoes on t.UserID equals em.UserID
                                  join r in db.ResolutionApprovals on t.MemberResolutionID equals r.MemberResolutionID
                                  join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                                  join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                                  join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                          
                                  where t.AcceptStatus == "true" && map.ParliamentNo == prl.ParliamentNo.ToString() /* && r.ParlSessID == sessionID */
                                   && new[] { "31" }.Contains(r.AssitantSccApproveStatus)
                                  select new
                                  {
                                      t.MemberResolutionID,
                                      r.ResolutionApproveID,
                                      html = r.AdministrativeOfcDetail,
                                      t.MemberResolutionDetail,
                                      t.MemberResolutionDate,
                                      t.MemberResolutionFIleURL,
                                      t.ParlSessID,
                                      t.AcceptStatus,
                                      t.EntryType,
                                      cnt.ConstitutentBangla,
                                      prl.ParliamentNo,
                                      prl.SessionNo,
                                      t.UserID,
                                      t.RDNo,
                                      t.AcceptanceComment,
                                      em.UserName,
                                      em.BanglaName,
                                      t.Status,

                                      r.AdministrativeOfcSignature,
                                      r.AssitantSccSignature,
                                      r.SrAssitantSccSignature,
                                      r.DeputySecSignature,
                                      r.SecSignature,
                                      r.AddSecSignature,
                                      r.SpeakerSignature,
                                  }).ToList();
                    return thList;
                }
                else
                {
                    // Handle other cases or return null/empty list
                    return null;
                }
            }
        }
        public object GetWatingListForSrAssistantSecretary(int sessionID,string DataMode)
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                if (DataMode == "Forward")
                {
                    var thList = (from t in db.MemberResolutionInfoes
                                  join em in db.EmployeeInfoes on t.UserID equals em.UserID
                                  join r in db.ResolutionApprovals on t.MemberResolutionID equals r.MemberResolutionID
                                  join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                                  join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                                  join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                                  // join apr in db.ApprovalStatus on r.SrAssitantSccApproveStatus equals apr.Status
                                  where map.ParliamentNo == prl.ParliamentNo.ToString() && r.ParlSessID == sessionID
                                 && t.AcceptStatus == "true"
                                   && new[] { "0" }.Contains(r.SrAssitantSccApproveStatus)
                                  select new
                                  {
                                      t.MemberResolutionID,
                                      r.ResolutionApproveID,
                                      html = r.AssitantSccDetail,
                                      t.MemberResolutionDetail,
                                      t.MemberResolutionDate,
                                      t.MemberResolutionFIleURL,
                                      t.ParlSessID,
                                      t.AcceptStatus,
                                      t.EntryType,
                                      cnt.ConstitutentBangla,
                                      prl.ParliamentNo,
                                      prl.SessionNo,
                                      t.UserID,
                                      t.RDNo,
                                      t.AcceptanceComment,
                                      em.UserName,
                                      em.BanglaName,
                                      t.Status,
                                      r.AdministrativeOfcSignature,
                                      r.AssitantSccSignature,
                                      r.SrAssitantSccSignature,
                                      r.DeputySecSignature,
                                      r.SecSignature,
                                      r.AddSecSignature,
                                      r.SpeakerSignature,
                                  }).ToList();
                    return thList;
                }
                else if (DataMode == "Backward")
                {
                    var thList = (from t in db.MemberResolutionInfoes
                                  join em in db.EmployeeInfoes on t.UserID equals em.UserID
                                  join r in db.ResolutionApprovals on t.MemberResolutionID equals r.MemberResolutionID
                                  join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                                  join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                                  join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                                  // join apr in db.ApprovalStatus on r.SrAssitantSccApproveStatus equals apr.Status
                                  where map.ParliamentNo == prl.ParliamentNo.ToString() && r.ParlSessID == sessionID
                                 && t.AcceptStatus == "true"
                                   && new[] { "0" }.Contains(r.SrAssitantSccBackStatus)
                                  select new
                                  {
                                      t.MemberResolutionID,
                                      r.ResolutionApproveID,
                                      html = r.AssitantSccDetail,
                                      t.MemberResolutionDetail,
                                      t.MemberResolutionDate,
                                      t.MemberResolutionFIleURL,
                                      t.ParlSessID,
                                      t.AcceptStatus,
                                      t.EntryType,
                                      cnt.ConstitutentBangla,
                                      prl.ParliamentNo,
                                      prl.SessionNo,
                                      t.UserID,
                                      t.RDNo,
                                      t.AcceptanceComment,
                                      em.UserName,
                                      em.BanglaName,
                                      t.Status,
                                      r.AdministrativeOfcSignature,
                                      r.AssitantSccSignature,
                                      r.SrAssitantSccSignature,
                                      r.DeputySecSignature,
                                      r.SecSignature,
                                      r.AddSecSignature,
                                      r.SpeakerSignature,
                                  }).ToList();
                    return thList;
                }
                else if (DataMode == "Draft")
                {
                    var thList = (from t in db.MemberResolutionInfoes
                                  join em in db.EmployeeInfoes on t.UserID equals em.UserID
                                  join r in db.ResolutionApprovals on t.MemberResolutionID equals r.MemberResolutionID
                                  join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                                  join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                                  join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                                  // join apr in db.ApprovalStatus on r.SrAssitantSccApproveStatus equals apr.Status
                                  where map.ParliamentNo == prl.ParliamentNo.ToString() /*&& r.ParlSessID == sessionID */
                                 && t.AcceptStatus == "true"
                                   && new[] { "31" }.Contains(r.SrAssitantSccApproveStatus)
                                  select new
                                  {
                                      t.MemberResolutionID,
                                      r.ResolutionApproveID,
                                      html = r.AssitantSccDetail,
                                      t.MemberResolutionDetail,
                                      t.MemberResolutionDate,
                                      t.MemberResolutionFIleURL,
                                      t.ParlSessID,
                                      t.AcceptStatus,
                                      t.EntryType,
                                      cnt.ConstitutentBangla,
                                      prl.ParliamentNo,
                                      prl.SessionNo,
                                      t.UserID,
                                      t.RDNo,
                                      t.AcceptanceComment,
                                      em.UserName,
                                      em.BanglaName,
                                      t.Status,
                                      r.AdministrativeOfcSignature,
                                      r.AssitantSccSignature,
                                      r.SrAssitantSccSignature,
                                      r.DeputySecSignature,
                                      r.SecSignature,
                                      r.AddSecSignature,
                                      r.SpeakerSignature,
                                  }).ToList();
                    return thList;
                }
                else
                {
                    // Handle other cases or return null/empty list
                    return null;
                }

            }
        }

        public object GetWaitingListForDeputySecretary(int sessionID, string DataMode)
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                if (DataMode == "Forward")
                {
                    var thList = (from t in db.MemberResolutionInfoes
                                  join em in db.EmployeeInfoes on t.UserID equals em.UserID
                                  join r in db.ResolutionApprovals on t.MemberResolutionID equals r.MemberResolutionID
                                  join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                                  join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                                  join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                                  // join apr in db.ApprovalStatus on r.DeputySecApproveStatus equals apr.Status
                                  where map.ParliamentNo == prl.ParliamentNo.ToString()
                                  && r.ParlSessID == sessionID && t.AcceptStatus == "true"
                                     && new[] { "0" }.Contains(r.DeputySecApproveStatus)
                                  select new
                                  {
                                      t.MemberResolutionID,
                                      r.ResolutionApproveID,
                                      html = r.SrAssitantSccDetail,
                                      t.MemberResolutionDetail,
                                      t.MemberResolutionDate,
                                      t.MemberResolutionFIleURL,
                                      t.ParlSessID,
                                      t.AcceptStatus,
                                      t.EntryType,
                                      cnt.ConstitutentBangla,
                                      prl.ParliamentNo,
                                      prl.SessionNo,
                                      t.UserID,
                                      t.RDNo,
                                      t.AcceptanceComment,
                                      em.UserName,
                                      em.BanglaName,
                                      t.Status,

                                      r.AdministrativeOfcSignature,
                                      r.AssitantSccSignature,
                                      r.SrAssitantSccSignature,
                                      r.DeputySecSignature,
                                      r.SecSignature,
                                      r.AddSecSignature,
                                      r.SpeakerSignature,
                                  }).ToList();
                    return thList;
                }
                else if (DataMode == "Backward")
                {
                    var thList = (from t in db.MemberResolutionInfoes
                                  join em in db.EmployeeInfoes on t.UserID equals em.UserID
                                  join r in db.ResolutionApprovals on t.MemberResolutionID equals r.MemberResolutionID
                                  join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                                  join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                                  join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                                  // join apr in db.ApprovalStatus on r.DeputySecApproveStatus equals apr.Status
                                  where map.ParliamentNo == prl.ParliamentNo.ToString()
                                  && r.ParlSessID == sessionID && t.AcceptStatus == "true"
                                     && new[] { "0" }.Contains(r.DeputySecBackStatus)
                                  select new
                                  {
                                      t.MemberResolutionID,
                                      r.ResolutionApproveID,
                                      html = r.SrAssitantSccDetail,
                                      t.MemberResolutionDetail,
                                      t.MemberResolutionDate,
                                      t.MemberResolutionFIleURL,
                                      t.ParlSessID,
                                      t.AcceptStatus,
                                      t.EntryType,
                                      cnt.ConstitutentBangla,
                                      prl.ParliamentNo,
                                      prl.SessionNo,
                                      t.UserID,
                                      t.RDNo,
                                      t.AcceptanceComment,
                                      em.UserName,
                                      em.BanglaName,
                                      t.Status,

                                      r.AdministrativeOfcSignature,
                                      r.AssitantSccSignature,
                                      r.SrAssitantSccSignature,
                                      r.DeputySecSignature,
                                      r.SecSignature,
                                      r.AddSecSignature,
                                      r.SpeakerSignature,
                                  }).ToList();
                    return thList;
                }
                else if (DataMode == "Draft")
                {
                    var thList = (from t in db.MemberResolutionInfoes
                                  join em in db.EmployeeInfoes on t.UserID equals em.UserID
                                  join r in db.ResolutionApprovals on t.MemberResolutionID equals r.MemberResolutionID
                                  join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                                  join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                                  join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                         
                                  where map.ParliamentNo == prl.ParliamentNo.ToString()  /* && r.ParlSessID == sessionID */ && t.AcceptStatus == "true"
                                     && new[] { "31" }.Contains(r.DeputySecApproveStatus)
                                  select new
                                  {
                                      t.MemberResolutionID,
                                      r.ResolutionApproveID,
                                      html = r.SrAssitantSccDetail,
                                      t.MemberResolutionDetail,
                                      t.MemberResolutionDate,
                                      t.MemberResolutionFIleURL,
                                      t.ParlSessID,
                                      t.AcceptStatus,
                                      t.EntryType,
                                      cnt.ConstitutentBangla,
                                      prl.ParliamentNo,
                                      prl.SessionNo,
                                      t.UserID,
                                      t.RDNo,
                                      t.AcceptanceComment,
                                      em.UserName,
                                      em.BanglaName,
                                      t.Status,

                                      r.AdministrativeOfcSignature,
                                      r.AssitantSccSignature,
                                      r.SrAssitantSccSignature,
                                      r.DeputySecSignature,
                                      r.SecSignature,
                                      r.AddSecSignature,
                                      r.SpeakerSignature,
                                  }).ToList();
                    return thList;
                }
                else
                {
                    // Handle other cases or return null/empty list
                    return null;
                }
            }
        }


        public object GetWaitingListForAdditionalSecretary(int session, string DataMode)
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                if (DataMode == "Forward")
                {
                    var thList = (from x in db.ResolutionApprovals
                                  join t in db.MemberResolutionInfoes on x.MemberResolutionID equals t.MemberResolutionID
                                  join em in db.EmployeeInfoes on t.UserID equals em.UserID
                                  join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                                  join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                                  join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                                  //join apr in db.ApprovalStatus on x.AddSecApproveStatus equals apr.Status
                                  where map.ParliamentNo == prl.ParliamentNo.ToString() && x.ParlSessID == session
                                 && t.AcceptStatus == "true" && new[] { "0" }.Contains(x.AddSecApproveStatus)
                                  select new
                                  {
                                      x.ResolutionApproveID,
                                      x.MemberResolutionID,
                                      x.AdministrativeOfcApproveStatus,
                                      x.AdministrativeOfcDetail,
                                      x.AddSecApproveDate,
                                      x.DeputySecApproveDetail,

                                      x.AdministrativeOfcSignature,
                                      x.AssitantSccSignature,
                                      x.SrAssitantSccSignature,
                                      x.DeputySecSignature,
                                      x.SecSignature,
                                      x.AddSecSignature,
                                      x.SpeakerSignature,

                                      html = x.DeputySecApproveDetail,
                                      //  MemberResolutionDetail = x.DeputySecApproveDetail,

                                      t.MemberResolutionDetail,

                                      t.MemberResolutionDate,
                                      t.MemberResolutionFIleURL,
                                      t.AcceptanceComment,
                                      t.ParlSessID,
                                      prl.ParliamentNo,
                                      cnt.ConstitutentBangla,
                                      prl.SessionNo,
                                      t.UserID,
                                      t.RDNo,
                                      em.UserName,
                                      em.BanglaName,
                                      t.Status,
                                  }).ToList();
                    return thList;
                }
                else if (DataMode == "Backward")
                {
                    var thList = (from x in db.ResolutionApprovals
                                  join t in db.MemberResolutionInfoes on x.MemberResolutionID equals t.MemberResolutionID
                                  join em in db.EmployeeInfoes on t.UserID equals em.UserID
                                  join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                                  join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                                  join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                         
                                  where map.ParliamentNo == prl.ParliamentNo.ToString() && x.ParlSessID == session
                                 && t.AcceptStatus == "true" && new[] { "0" }.Contains(x.AddSecBackStatus)
                                  select new
                                  {
                                      x.ResolutionApproveID,
                                      x.MemberResolutionID,
                                      x.AdministrativeOfcApproveStatus,
                                      x.AdministrativeOfcDetail,
                                      x.AddSecApproveDate,
                                      x.DeputySecApproveDetail,

                                      x.AdministrativeOfcSignature,
                                      x.AssitantSccSignature,
                                      x.SrAssitantSccSignature,
                                      x.DeputySecSignature,
                                      x.SecSignature,
                                      x.AddSecSignature,
                                      x.SpeakerSignature,

                                      html = x.DeputySecApproveDetail,
                                      //  MemberResolutionDetail = x.DeputySecApproveDetail,

                                      t.MemberResolutionDetail,

                                      t.MemberResolutionDate,
                                      t.MemberResolutionFIleURL,
                                      t.AcceptanceComment,
                                      t.ParlSessID,
                                      prl.ParliamentNo,
                                      cnt.ConstitutentBangla,
                                      prl.SessionNo,
                                      t.UserID,
                                      t.RDNo,
                                      em.UserName,
                                      em.BanglaName,
                                      t.Status,
                                  }).ToList();
                    return thList;
                }
                else if (DataMode == "Draft")
                {
                    var thList = (from x in db.ResolutionApprovals
                                  join t in db.MemberResolutionInfoes on x.MemberResolutionID equals t.MemberResolutionID
                                  join em in db.EmployeeInfoes on t.UserID equals em.UserID
                                  join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                                  join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                                  join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                         
                                  where map.ParliamentNo == prl.ParliamentNo.ToString() /*&& x.ParlSessID == session */
                                 && t.AcceptStatus == "true" && new[] { "31" }.Contains(x.AddSecApproveStatus)
                                  select new
                                  {
                                      x.ResolutionApproveID,
                                      x.MemberResolutionID,
                                      x.AdministrativeOfcApproveStatus,
                                      x.AdministrativeOfcDetail,
                                      x.AddSecApproveDate,
                                      x.DeputySecApproveDetail,

                                      x.AdministrativeOfcSignature,
                                      x.AssitantSccSignature,
                                      x.SrAssitantSccSignature,
                                      x.DeputySecSignature,
                                      x.SecSignature,
                                      x.AddSecSignature,
                                      x.SpeakerSignature,

                                      html = x.DeputySecApproveDetail,
                                      //  MemberResolutionDetail = x.DeputySecApproveDetail,

                                      t.MemberResolutionDetail,

                                      t.MemberResolutionDate,
                                      t.MemberResolutionFIleURL,
                                      t.AcceptanceComment,
                                      t.ParlSessID,
                                      prl.ParliamentNo,
                                      cnt.ConstitutentBangla,
                                      prl.SessionNo,
                                      t.UserID,
                                      t.RDNo,
                                      em.UserName,
                                      em.BanglaName,
                                      t.Status,
                                  }).ToList();
                    return thList;
                }
                else
                {
                    // Handle other cases or return null/empty list
                    return null;
                }
            }
        }
        public object GetWaitingListForSecretary(int sessionID, string DataMode)
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                if (DataMode == "Forward")
                {
                    var thList = (from x in db.ResolutionApprovals
                                  join t in db.MemberResolutionInfoes on x.MemberResolutionID equals t.MemberResolutionID
                                  join em in db.EmployeeInfoes on t.UserID equals em.UserID
                                  join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                                  join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                                  join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                                  //  join apr in db.ApprovalStatus on x.AddSecApproveStatus equals apr.Status                           
                                  where map.ParliamentNo == prl.ParliamentNo.ToString() && x.ParlSessID == sessionID
                                    && t.AcceptStatus == "true" && new[] { "0" }.Contains(x.SecApproveStatus)
                                  select new
                                  {
                                      x.ResolutionApproveID,
                                      x.MemberResolutionID,
                                      x.SecApproveDate,
                                      x.SecApproveDetail,
                                      x.SecApproveStatus,

                                      x.AdministrativeOfcSignature,
                                      x.AssitantSccSignature,
                                      x.SrAssitantSccSignature,
                                      x.DeputySecSignature,
                                      x.SecSignature,
                                      x.AddSecSignature,
                                      x.SpeakerSignature,
                                      html = x.AddSecApproveDetail,
                                      //MemberResolutionDetail = x.AddSecApproveDetail,                          
                                      t.MemberResolutionDetail,
                                      t.MemberResolutionDate,
                                      t.MemberResolutionFIleURL,
                                      t.AcceptanceComment,
                                      t.ParlSessID,
                                      prl.ParliamentNo,
                                      cnt.ConstitutentBangla,
                                      prl.SessionNo,
                                      t.UserID,
                                      t.RDNo,
                                      em.UserName,
                                      em.BanglaName,
                                      t.Status,
                                  }).ToList();
                    return thList;
                }
                else if (DataMode == "Backward")
                {
                    var thList = (from x in db.ResolutionApprovals
                                  join t in db.MemberResolutionInfoes on x.MemberResolutionID equals t.MemberResolutionID
                                  join em in db.EmployeeInfoes on t.UserID equals em.UserID
                                  join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                                  join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                                  join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                                                        
                                  where map.ParliamentNo == prl.ParliamentNo.ToString() && x.ParlSessID == sessionID
                                    && t.AcceptStatus == "true" && new[] { "0" }.Contains(x.SecBackStatus)
                                  select new
                                  {
                                      x.ResolutionApproveID,
                                      x.MemberResolutionID,
                                      x.SecApproveDate,
                                      x.SecApproveDetail,
                                      x.SecApproveStatus,

                                      x.AdministrativeOfcSignature,
                                      x.AssitantSccSignature,
                                      x.SrAssitantSccSignature,
                                      x.DeputySecSignature,
                                      x.SecSignature,
                                      x.AddSecSignature,
                                      x.SpeakerSignature,
                                      html = x.AddSecApproveDetail,
                                      //MemberResolutionDetail = x.AddSecApproveDetail,                          
                                      t.MemberResolutionDetail,
                                      t.MemberResolutionDate,
                                      t.MemberResolutionFIleURL,
                                      t.AcceptanceComment,
                                      t.ParlSessID,
                                      prl.ParliamentNo,
                                      cnt.ConstitutentBangla,
                                      prl.SessionNo,
                                      t.UserID,
                                      t.RDNo,
                                      em.UserName,
                                      em.BanglaName,
                                      t.Status,
                                  }).ToList();
                    return thList;
                }
                else if (DataMode == "Draft")
                {
                    var thList = (from x in db.ResolutionApprovals
                                  join t in db.MemberResolutionInfoes on x.MemberResolutionID equals t.MemberResolutionID
                                  join em in db.EmployeeInfoes on t.UserID equals em.UserID
                                  join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                                  join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                                  join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                                  //  join apr in db.ApprovalStatus on x.AddSecApproveStatus equals apr.Status                           
                                  where map.ParliamentNo == prl.ParliamentNo.ToString() /*&& x.ParlSessID == sessionID */
                                    && t.AcceptStatus == "true" && new[] { "31" }.Contains(x.SecApproveStatus)
                                  select new
                                  {
                                      x.ResolutionApproveID,
                                      x.MemberResolutionID,
                                      x.SecApproveDate,
                                      x.SecApproveDetail,
                                      x.SecApproveStatus,

                                      x.AdministrativeOfcSignature,
                                      x.AssitantSccSignature,
                                      x.SrAssitantSccSignature,
                                      x.DeputySecSignature,
                                      x.SecSignature,
                                      x.AddSecSignature,
                                      x.SpeakerSignature,
                                      html = x.AddSecApproveDetail,
                                      //MemberResolutionDetail = x.AddSecApproveDetail,                          
                                      t.MemberResolutionDetail,
                                      t.MemberResolutionDate,
                                      t.MemberResolutionFIleURL,
                                      t.AcceptanceComment,
                                      t.ParlSessID,
                                      prl.ParliamentNo,
                                      cnt.ConstitutentBangla,
                                      prl.SessionNo,
                                      t.UserID,
                                      t.RDNo,
                                      em.UserName,
                                      em.BanglaName,
                                      t.Status,
                                  }).ToList();
                    return thList;
                }
                else
                {
                    // Handle other cases or return null/empty list
                    return null;
                }
            }
        }
        public object GetWaitingListForSpeaker(int sessionID)
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                
                    var thList = (from x in db.ResolutionApprovals
                                  join t in db.MemberResolutionInfoes on x.MemberResolutionID equals t.MemberResolutionID
                                  join em in db.EmployeeInfoes on t.UserID equals em.UserID
                                  join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                                  join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                                  join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                                  //  join apr in db.ApprovalStatus on x.AddSecApproveStatus equals apr.Status
                                  where map.ParliamentNo == prl.ParliamentNo.ToString() && x.ParlSessID == sessionID
                                  && t.AcceptStatus == "true" && new[] { "0" }.Contains(x.SpeakerApproveStatus)
                                  select new
                                  {
                                      x.ResolutionApproveID,
                                      x.MemberResolutionID,
                                      x.SpeakerApproveDate,
                                      x.SpeakerApproveDetail,
                                      x.SpeakerApproveStatus,

                                      x.AdministrativeOfcSignature,
                                      x.AssitantSccSignature,
                                      x.SrAssitantSccSignature,
                                      x.DeputySecSignature,
                                      x.SecSignature,
                                      x.AddSecSignature,
                                      x.SpeakerSignature,

                                      html = x.SecApproveDetail,
                                      //MemberResolutionDetail = x.SecApproveDetail,


                                      t.MemberResolutionDetail,

                                      t.MemberResolutionDate,
                                      t.MemberResolutionFIleURL,
                                      t.AcceptanceComment,
                                      t.ParlSessID,
                                      prl.ParliamentNo,
                                      cnt.ConstitutentBangla,
                                      prl.SessionNo,
                                      t.UserID,
                                      t.RDNo,
                                      em.UserName,
                                      em.BanglaName,
                                      t.Status,
                                  }).ToList();
                    return thList;
               
            }
        }
        public object GetPostedHistoryListByAdministrativeOfficer()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var thList = (from x in db.ResolutionApprovals
                              join t in db.MemberResolutionInfoes on x.MemberResolutionID equals t.MemberResolutionID
                              join em in db.EmployeeInfoes on t.UserID equals em.UserID
                              join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                              join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                              join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                              join apr in db.ApprovalStatus on x.AdministrativeOfcApproveStatus equals apr.ID.ToString()
                              where t.AcceptStatus == "true" && map.ParliamentNo == prl.ParliamentNo.ToString()
                               && (x.AdministrativeOfcApproveStatus != "0" && x.AdministrativeOfcApproveStatus != null)
                               && ((x.AssitantSccApproveStatus != "0" && x.AssitantSccApproveStatus != null) || (x.SrAssitantSccApproveStatus != "0" && x.SrAssitantSccApproveStatus != null) || (x.DeputySecApproveStatus != "0" && x.DeputySecApproveStatus != null) || (x.AddSecApproveStatus != "0" && x.AddSecApproveStatus != null ) || (x.SecApproveStatus != "0" && x.SecApproveStatus != null ) || (x.SpeakerApproveStatus != "0" && x.SpeakerApproveStatus != null))
                          
                              select new
                              {
                                  x.ResolutionApproveID,
                                  x.MemberResolutionID,
                                  AdministrativeOfcApproveStatus = apr.Comments,
                                  AprID = apr.ID,                              
                                  x.AddSecApproveDate,

                                  x.AdministrativeOfcSignature,
                                  x.AssitantSccSignature,
                                  x.SrAssitantSccSignature,
                                  x.DeputySecSignature,
                                  x.SecSignature,
                                  x.AddSecSignature,
                                  x.SpeakerSignature,

                                  html = x.AdministrativeOfcDetail,
                                  t.MemberResolutionDetail,
                                  t.MemberResolutionDate,
                                  t.MemberResolutionFIleURL,
                                  t.AcceptanceComment,
                                  t.ParlSessID,
                                  prl.ParliamentNo,
                                  cnt.ConstitutentBangla,
                                  prl.SessionNo,
                                  t.UserID,
                                  t.RDNo,
                                  em.UserName,
                                  em.BanglaName,
                                  t.Status,
                              }).ToList();
                return thList;

            }
        }
        public object GetPostedHistoryListByAssistantSecretary()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var thList = (from x in db.ResolutionApprovals
                              join t in db.MemberResolutionInfoes on x.MemberResolutionID equals t.MemberResolutionID
                              join em in db.EmployeeInfoes on t.UserID equals em.UserID
                              join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                              join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                              join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                              join apr in db.ApprovalStatus on x.AdministrativeOfcApproveStatus equals apr.ID.ToString()
                              where t.AcceptStatus == "true" && map.ParliamentNo == prl.ParliamentNo.ToString()
                               && (x.AssitantSccApproveStatus != "0" && x.AssitantSccApproveStatus != null)
                               && ((x.SrAssitantSccApproveStatus != "0" && x.SrAssitantSccApproveStatus != null) || (x.DeputySecApproveStatus != "0" && x.DeputySecApproveStatus != null) || (x.AddSecApproveStatus != "0" && x.AddSecApproveStatus != null) || (x.SecApproveStatus != "0" && x.SecApproveStatus != null) || (x.SpeakerApproveStatus != "0" && x.SpeakerApproveStatus != null))
                              select new
                              {
                                  x.ResolutionApproveID,
                                  x.MemberResolutionID,
                                  AssitantSccApproveStatus = apr.Comments,
                                  AprID = apr.ID,
                                  x.AdministrativeOfcDetail,
                                  x.AddSecApproveDate,

                                  x.AdministrativeOfcSignature,
                                  x.AssitantSccSignature,
                                  x.SrAssitantSccSignature,
                                  x.DeputySecSignature,
                                  x.SecSignature,
                                  x.AddSecSignature,
                                  x.SpeakerSignature,

                                  html = x.AssitantSccDetail,
                                  t.MemberResolutionDetail,
                                  t.MemberResolutionDate,
                                  t.MemberResolutionFIleURL,
                                  t.AcceptanceComment,
                                  t.ParlSessID,
                                  prl.ParliamentNo,
                                  cnt.ConstitutentBangla,
                                  prl.SessionNo,
                                  t.UserID,
                                  t.RDNo,
                                  em.UserName,
                                  em.BanglaName,
                                  t.Status,
                              }).ToList();
                return thList;
            }
        }
        public object GetPostedHistoryListBySrAssistantSecretary()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var thList = (from x in db.ResolutionApprovals
                              join t in db.MemberResolutionInfoes on x.MemberResolutionID equals t.MemberResolutionID
                              join em in db.EmployeeInfoes on t.UserID equals em.UserID
                              join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                              join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                              join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                              join apr in db.ApprovalStatus on x.AdministrativeOfcApproveStatus equals apr.ID.ToString()
                              where t.AcceptStatus == "true" && map.ParliamentNo == prl.ParliamentNo.ToString()
                               && x.SrAssitantSccApproveStatus != "0" && x.SrAssitantSccApproveStatus != null
                               && ((x.DeputySecApproveStatus != "0" && x.DeputySecApproveStatus != null) || (x.AddSecApproveStatus != "0" && x.AddSecApproveStatus != null) || (x.SecApproveStatus != "0" && x.SecApproveStatus != null) || (x.SpeakerApproveStatus != "0" && x.SpeakerApproveStatus != null))

                              select new
                              {
                                  x.ResolutionApproveID,
                                  x.MemberResolutionID,
                                  SrAssitantSccApproveStatus = apr.Comments,
                                  AprID = apr.ID,
                                  x.AdministrativeOfcDetail,
                                  x.AddSecApproveDate,

                                  x.AdministrativeOfcSignature,
                                  x.AssitantSccSignature,
                                  x.SrAssitantSccSignature,
                                  x.DeputySecSignature,
                                  x.SecSignature,
                                  x.AddSecSignature,
                                  x.SpeakerSignature,

                                  html = x.SrAssitantSccDetail,
                                  t.MemberResolutionDetail,
                                  t.MemberResolutionDate,
                                  t.MemberResolutionFIleURL,
                                  t.AcceptanceComment,
                                  t.ParlSessID,
                                  prl.ParliamentNo,
                                  cnt.ConstitutentBangla,
                                  prl.SessionNo,
                                  t.UserID,
                                  t.RDNo,
                                  em.UserName,
                                  em.BanglaName,
                                  t.Status,
                              }).ToList();
                return thList;
            }
        }
        public object GetPostedHistoryListByDeputySecretary()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var thList = (from x in db.ResolutionApprovals
                              join t in db.MemberResolutionInfoes on x.MemberResolutionID equals t.MemberResolutionID
                              join em in db.EmployeeInfoes on t.UserID equals em.UserID
                              join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                              join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                              join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                              join apr in db.ApprovalStatus on x.AdministrativeOfcApproveStatus equals apr.ID.ToString()
                              where t.AcceptStatus == "true" && map.ParliamentNo == prl.ParliamentNo.ToString()
                               && (x.DeputySecApproveStatus != "0" && x.DeputySecApproveStatus != null)
                               && ((x.AddSecApproveStatus != "0" && x.AddSecApproveStatus != null) || (x.SecApproveStatus != "0" && x.SecApproveStatus != null) || (x.SpeakerApproveStatus != "0" && x.SpeakerApproveStatus != null))

                              select new
                              {
                                  x.ResolutionApproveID,
                                  x.MemberResolutionID,
                                  DeputySecApproveStatus = apr.Comments,
                                  AprID = apr.ID,
                                  x.AdministrativeOfcDetail,
                                  x.AddSecApproveDate,

                                  x.AdministrativeOfcSignature,
                                  x.AssitantSccSignature,
                                  x.SrAssitantSccSignature,
                                  x.DeputySecSignature,
                                  x.SecSignature,
                                  x.AddSecSignature,
                                  x.SpeakerSignature,

                                  html = x.DeputySecApproveDetail,
                                  t.MemberResolutionDetail,
                                  t.MemberResolutionDate,
                                  t.MemberResolutionFIleURL,
                                  t.AcceptanceComment,
                                  t.ParlSessID,
                                  prl.ParliamentNo,
                                  cnt.ConstitutentBangla,
                                  prl.SessionNo,
                                  t.UserID,
                                  t.RDNo,
                                  em.UserName,
                                  em.BanglaName,
                                  t.Status,
                              }).ToList();
                return thList;
            }
        }
        public object GetPostedHistoryListByAdditionalSecretary()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var thList = (from x in db.ResolutionApprovals
                              join t in db.MemberResolutionInfoes on x.MemberResolutionID equals t.MemberResolutionID
                              join em in db.EmployeeInfoes on t.UserID equals em.UserID
                              join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                              join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                              join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                              join apr in db.ApprovalStatus on x.AdministrativeOfcApproveStatus equals apr.ID.ToString()
                              where t.AcceptStatus == "true" && map.ParliamentNo == prl.ParliamentNo.ToString()
                               && x.AddSecApproveStatus != "0" && x.AddSecApproveStatus != null
                               && ((x.SecApproveStatus != "0" && x.SecApproveStatus != null) || (x.SpeakerApproveStatus != "0" && x.SpeakerApproveStatus != null))

                              select new
                              {
                                  x.ResolutionApproveID,
                                  x.MemberResolutionID,
                                  AddSecApproveStatus = apr.Comments,
                                  AprID = apr.ID,
                                  x.AdministrativeOfcDetail,
                                  x.AddSecApproveDate,

                                  x.AdministrativeOfcSignature,
                                  x.AssitantSccSignature,
                                  x.SrAssitantSccSignature,
                                  x.DeputySecSignature,
                                  x.SecSignature,
                                  x.AddSecSignature,
                                  x.SpeakerSignature,

                                  html = x.AddSecApproveDetail,
                                  t.MemberResolutionDetail,
                                  t.MemberResolutionDate,
                                  t.MemberResolutionFIleURL,
                                  t.AcceptanceComment,
                                  t.ParlSessID,
                                  prl.ParliamentNo,
                                  cnt.ConstitutentBangla,
                                  prl.SessionNo,
                                  t.UserID,
                                  t.RDNo,
                                  em.UserName,
                                  em.BanglaName,
                                  t.Status,
                              }).ToList();
                return thList;
            }
        }
        public object GetPostedHistoryListBySecretary()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var thList = (from x in db.ResolutionApprovals
                              join t in db.MemberResolutionInfoes on x.MemberResolutionID equals t.MemberResolutionID
                              join em in db.EmployeeInfoes on t.UserID equals em.UserID
                              join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                              join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                              join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                              join apr in db.ApprovalStatus on x.AdministrativeOfcApproveStatus equals apr.ID.ToString()
                              where t.AcceptStatus == "true" && map.ParliamentNo == prl.ParliamentNo.ToString()
                               && (x.SecApproveStatus != "0" && x.SecApproveStatus != null)
                               && ((x.SpeakerApproveStatus != "0" && x.SpeakerApproveStatus != null))

                              select new
                              {
                                  x.ResolutionApproveID,
                                  x.MemberResolutionID,
                                  SecApproveStatus = apr.Comments,
                                  AprID = apr.ID,
                                  x.AdministrativeOfcDetail,
                                  x.AddSecApproveDate,

                                  x.AdministrativeOfcSignature,
                                  x.AssitantSccSignature,
                                  x.SrAssitantSccSignature,
                                  x.DeputySecSignature,
                                  x.SecSignature,
                                  x.AddSecSignature,
                                  x.SpeakerSignature,

                                  html = x.SecApproveDetail,
                                  t.MemberResolutionDetail,
                                  t.MemberResolutionDate,
                                  t.MemberResolutionFIleURL,
                                  t.AcceptanceComment,
                                  t.ParlSessID,
                                  prl.ParliamentNo,
                                  cnt.ConstitutentBangla,
                                  prl.SessionNo,
                                  t.UserID,
                                  t.RDNo,
                                  em.UserName,
                                  em.BanglaName,
                                  t.Status,
                              }).ToList();
                return thList;
            }
        }
        public object GetPostedHistoryListBySpeaker()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var thList = (from x in db.ResolutionApprovals
                              join t in db.MemberResolutionInfoes on x.MemberResolutionID equals t.MemberResolutionID
                              join em in db.EmployeeInfoes on t.UserID equals em.UserID
                              join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                              join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                              join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                              join apr in db.ApprovalStatus on x.AdministrativeOfcApproveStatus equals apr.ID.ToString()
                              where t.AcceptStatus == "true" && map.ParliamentNo == prl.ParliamentNo.ToString()
                               && x.SpeakerApproveStatus != "0" && x.SpeakerApproveStatus != null                              
                              select new
                              {
                                  x.ResolutionApproveID,
                                  x.MemberResolutionID,
                                  SpeakerApproveStatus = apr.Comments,
                                  AprID = apr.ID,
                                  x.AdministrativeOfcDetail,
                                  x.AddSecApproveDate,

                                  x.AdministrativeOfcSignature,
                                  x.AssitantSccSignature,
                                  x.SrAssitantSccSignature,
                                  x.DeputySecSignature,
                                  x.SecSignature,
                                  x.AddSecSignature,
                                  x.SpeakerSignature,

                                  html = x.SpeakerApproveDetail,
                                  t.MemberResolutionDetail,
                                  t.MemberResolutionDate,
                                  t.MemberResolutionFIleURL,
                                  t.AcceptanceComment,
                                  t.ParlSessID,
                                  prl.ParliamentNo,
                                  cnt.ConstitutentBangla,
                                  prl.SessionNo,
                                  t.UserID,
                                  t.RDNo,
                                  em.UserName,
                                  em.BanglaName,
                                  t.Status,
                              }).ToList();
                return thList;
            }
        }
        public object GetSearchForEditAdministrativeOfficer()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var thList = (from x in db.ResolutionApprovals
                              join t in db.MemberResolutionInfoes on x.MemberResolutionID equals t.MemberResolutionID
                              join em in db.EmployeeInfoes on t.UserID equals em.UserID
                              join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                              join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                              join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                              join apr in db.ApprovalStatus on x.AdministrativeOfcApproveStatus equals apr.ID.ToString()
                              where t.AcceptStatus == "true" && map.ParliamentNo == prl.ParliamentNo.ToString()
                               && (x.AdministrativeOfcApproveStatus != "0" && x.AdministrativeOfcApproveStatus != null)
                               && ( (x.AssitantSccApproveStatus == "0" || x.AssitantSccApproveStatus == null) && (x.SrAssitantSccApproveStatus == "0" || x.SrAssitantSccApproveStatus == null) && (x.DeputySecApproveStatus == "0" || x.DeputySecApproveStatus == null) && (x.AddSecApproveStatus == "0" || x.AddSecApproveStatus == null) && (x.SecApproveStatus == "0" || x.SecApproveStatus == null) && (x.SpeakerApproveStatus == "0" || x.SpeakerApproveStatus == null))

                              select new
                              {
                                  x.ResolutionApproveID,
                                  x.MemberResolutionID,
                                  AdministrativeOfcApproveStatus = apr.Comments,
                                  AprID = apr.ID,
                                  x.AdministrativeOfcDetail,                                
                                  x.AddSecApproveDate,

                                  x.AdministrativeOfcSignature,
                                  x.AssitantSccSignature,
                                  x.SrAssitantSccSignature,
                                  x.DeputySecSignature,                            
                                  x.SecSignature,
                                  x.AddSecSignature,
                                  x.SpeakerSignature,

                                  html = x.AdministrativeOfcDetail,
                                  t.MemberResolutionDetail,
                                  t.MemberResolutionDate,
                                  t.MemberResolutionFIleURL,
                                  t.AcceptanceComment,
                                  t.ParlSessID,
                                  prl.ParliamentNo,
                                  cnt.ConstitutentBangla,
                                  prl.SessionNo,
                                  t.UserID,
                                  t.RDNo,
                                  em.UserName,
                                  em.BanglaName,
                                  t.Status,

                              }).ToList();
                return thList;
            }
        }
        public object GetSearchForEditAssistantSecretary()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var thList = (from x in db.ResolutionApprovals
                              join t in db.MemberResolutionInfoes on x.MemberResolutionID equals t.MemberResolutionID
                              join em in db.EmployeeInfoes on t.UserID equals em.UserID
                              join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                              join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                              join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                              join apr in db.ApprovalStatus on x.AdministrativeOfcApproveStatus equals apr.ID.ToString()
                              where t.AcceptStatus == "true" && map.ParliamentNo == prl.ParliamentNo.ToString()
                               && (x.AssitantSccApproveStatus != "0" && x.AssitantSccApproveStatus != null)
                               && ( (x.SrAssitantSccApproveStatus == "0" || x.SrAssitantSccApproveStatus == null) && (x.DeputySecApproveStatus == "0" || x.DeputySecApproveStatus == null) && (x.AddSecApproveStatus == "0" || x.AddSecApproveStatus == null) && (x.SecApproveStatus == "0" || x.SecApproveStatus == null) && (x.SpeakerApproveStatus == "0" || x.SpeakerApproveStatus == null))

                              select new
                              {
                                  x.ResolutionApproveID,
                                  x.MemberResolutionID,
                                  AssitantSccApproveStatus = apr.Comments,
                                  AprID = apr.ID,
                                  x.AdministrativeOfcDetail,
                                  x.AddSecApproveDate,

                                  x.AdministrativeOfcSignature,
                                  x.AssitantSccSignature,
                                  x.SrAssitantSccSignature,
                                  x.DeputySecSignature,
                                  x.SecSignature,
                                  x.AddSecSignature,
                                  x.SpeakerSignature,

                                  html = x.AssitantSccDetail,
                                  t.MemberResolutionDetail,
                                  t.MemberResolutionDate,
                                  t.MemberResolutionFIleURL,
                                  t.AcceptanceComment,
                                  t.ParlSessID,
                                  prl.ParliamentNo,
                                  cnt.ConstitutentBangla,
                                  prl.SessionNo,
                                  t.UserID,
                                  t.RDNo,
                                  em.UserName,
                                  em.BanglaName,
                                  t.Status,
                              }).ToList();
                return thList;
            }
        }
        public object GetSearchForEditSrAssistantSecretary()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var thList = (from x in db.ResolutionApprovals
                              join t in db.MemberResolutionInfoes on x.MemberResolutionID equals t.MemberResolutionID
                              join em in db.EmployeeInfoes on t.UserID equals em.UserID
                              join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                              join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                              join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                              join apr in db.ApprovalStatus on x.AdministrativeOfcApproveStatus equals apr.ID.ToString()
                              where t.AcceptStatus == "true" && map.ParliamentNo == prl.ParliamentNo.ToString()
                               && (x.SrAssitantSccApproveStatus != "0" && x.SrAssitantSccApproveStatus != null)
                               && ((x.DeputySecApproveStatus == "0" || x.DeputySecApproveStatus == null) && (x.AddSecApproveStatus == "0" || x.AddSecApproveStatus == null) && (x.SecApproveStatus == "0" || x.SecApproveStatus == null) && (x.SpeakerApproveStatus == "0" || x.SpeakerApproveStatus == null))

                              select new
                              {
                                  x.ResolutionApproveID,
                                  x.MemberResolutionID,
                                  SrAssitantSccApproveStatus = apr.Comments,
                                  AprID = apr.ID,
                                  x.AdministrativeOfcDetail,
                                  x.AddSecApproveDate,

                                  x.AdministrativeOfcSignature,
                                  x.AssitantSccSignature,
                                  x.SrAssitantSccSignature,
                                  x.DeputySecSignature,
                                  x.SecSignature,
                                  x.AddSecSignature,
                                  x.SpeakerSignature,

                                  html = x.SrAssitantSccDetail,
                                  t.MemberResolutionDetail,
                                  t.MemberResolutionDate,
                                  t.MemberResolutionFIleURL,
                                  t.AcceptanceComment,
                                  t.ParlSessID,
                                  prl.ParliamentNo,
                                  cnt.ConstitutentBangla,
                                  prl.SessionNo,
                                  t.UserID,
                                  t.RDNo,
                                  em.UserName,
                                  em.BanglaName,
                                  t.Status,
                              }).ToList();
                return thList;
            }
        }
        public object GetSearchForEditDeputySecretary()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var thList = (from x in db.ResolutionApprovals
                              join t in db.MemberResolutionInfoes on x.MemberResolutionID equals t.MemberResolutionID
                              join em in db.EmployeeInfoes on t.UserID equals em.UserID
                              join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                              join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                              join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                              join apr in db.ApprovalStatus on x.AdministrativeOfcApproveStatus equals apr.ID.ToString()
                              where t.AcceptStatus == "true" && map.ParliamentNo == prl.ParliamentNo.ToString()
                               && (x.DeputySecApproveStatus != "0" && x.DeputySecApproveStatus != null)
                               && ( (x.AddSecApproveStatus == "0" || x.AddSecApproveStatus == null) && (x.SecApproveStatus == "0" || x.SecApproveStatus == null) && (x.SpeakerApproveStatus == "0" || x.SpeakerApproveStatus == null))

                              select new
                              {
                                  x.ResolutionApproveID,
                                  x.MemberResolutionID,
                                  DeputySecApproveStatus = apr.Comments,
                                  AprID = apr.ID,
                                  x.AdministrativeOfcDetail,
                                  x.AddSecApproveDate,

                                  x.AdministrativeOfcSignature,
                                  x.AssitantSccSignature,
                                  x.SrAssitantSccSignature,
                                  x.DeputySecSignature,
                                  x.SecSignature,
                                  x.AddSecSignature,
                                  x.SpeakerSignature,

                                  html = x.DeputySecApproveDetail,
                                  t.MemberResolutionDetail,
                                  t.MemberResolutionDate,
                                  t.MemberResolutionFIleURL,
                                  t.AcceptanceComment,
                                  t.ParlSessID,
                                  prl.ParliamentNo,
                                  cnt.ConstitutentBangla,
                                  prl.SessionNo,
                                  t.UserID,
                                  t.RDNo,
                                  em.UserName,
                                  em.BanglaName,
                                  t.Status,
                              }).ToList();
                return thList;
            }
        }
        public object GetSearchForEditAdditionalSecretary()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var thList = (from x in db.ResolutionApprovals
                              join t in db.MemberResolutionInfoes on x.MemberResolutionID equals t.MemberResolutionID
                              join em in db.EmployeeInfoes on t.UserID equals em.UserID
                              join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                              join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                              join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                              join apr in db.ApprovalStatus on x.AdministrativeOfcApproveStatus equals apr.ID.ToString()
                              where t.AcceptStatus == "true" && map.ParliamentNo == prl.ParliamentNo.ToString()
                               && (x.AddSecApproveStatus != "0" && x.AddSecApproveStatus != null)
                               && ((x.SecApproveStatus == "0" || x.SecApproveStatus == null) && (x.SpeakerApproveStatus == "0" || x.SpeakerApproveStatus == null))

                              select new
                              {
                                  x.ResolutionApproveID,
                                  x.MemberResolutionID,
                                  AddSecApproveStatus = apr.Comments,
                                  AprID = apr.ID,
                                  x.AdministrativeOfcDetail,
                                  x.AddSecApproveDate,

                                  x.AdministrativeOfcSignature,
                                  x.AssitantSccSignature,
                                  x.SrAssitantSccSignature,
                                  x.DeputySecSignature,
                                  x.SecSignature,
                                  x.AddSecSignature,
                                  x.SpeakerSignature,

                                  html = x.AddSecApproveDetail,
                                  t.MemberResolutionDetail,
                                  t.MemberResolutionDate,
                                  t.MemberResolutionFIleURL,
                                  t.AcceptanceComment,
                                  t.ParlSessID,
                                  prl.ParliamentNo,
                                  cnt.ConstitutentBangla,
                                  prl.SessionNo,
                                  t.UserID,
                                  t.RDNo,
                                  em.UserName,
                                  em.BanglaName,
                                  t.Status,
                              }).ToList();
                return thList;
            }
        }
        public object GetSearchForEditSecretary()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var thList = (from x in db.ResolutionApprovals
                              join t in db.MemberResolutionInfoes on x.MemberResolutionID equals t.MemberResolutionID
                              join em in db.EmployeeInfoes on t.UserID equals em.UserID
                              join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                              join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                              join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                              join apr in db.ApprovalStatus on x.AdministrativeOfcApproveStatus equals apr.ID.ToString()
                              where t.AcceptStatus == "true" && map.ParliamentNo == prl.ParliamentNo.ToString()
                               && (x.SecApproveStatus != "0" && x.SecApproveStatus != null)
                               && ( (x.SpeakerApproveStatus == "0" || x.SpeakerApproveStatus == null))

                              select new
                              {
                                  x.ResolutionApproveID,
                                  x.MemberResolutionID,
                                  SecApproveStatus = apr.Comments,
                                  AprID = apr.ID,
                                  x.AdministrativeOfcDetail,
                                  x.AddSecApproveDate,

                                  x.AdministrativeOfcSignature,
                                  x.AssitantSccSignature,
                                  x.SrAssitantSccSignature,
                                  x.DeputySecSignature,
                                  x.SecSignature,
                                  x.AddSecSignature,
                                  x.SpeakerSignature,

                                  html = x.SecApproveDetail,
                                  t.MemberResolutionDetail,
                                  t.MemberResolutionDate,
                                  t.MemberResolutionFIleURL,
                                  t.AcceptanceComment,
                                  t.ParlSessID,
                                  prl.ParliamentNo,
                                  cnt.ConstitutentBangla,
                                  prl.SessionNo,
                                  t.UserID,
                                  t.RDNo,
                                  em.UserName,
                                  em.BanglaName,
                                  t.Status,
                              }).ToList();
                return thList;
            }
        }
        public object GetSearchForEditSpeaker()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var thList = (from x in db.ResolutionApprovals
                              join t in db.MemberResolutionInfoes on x.MemberResolutionID equals t.MemberResolutionID
                              join em in db.EmployeeInfoes on t.UserID equals em.UserID
                              join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                              join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                              join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                              join apr in db.ApprovalStatus on x.AdministrativeOfcApproveStatus equals apr.ID.ToString()
                              where t.AcceptStatus == "true" && map.ParliamentNo == prl.ParliamentNo.ToString()
                               && (x.SpeakerApproveStatus != "0" && x.SpeakerApproveStatus != null)
                              

                              select new
                              {
                                  x.ResolutionApproveID,
                                  x.MemberResolutionID,
                                  SpeakerApproveStatus = apr.Comments,
                                  AprID = apr.ID,
                                  x.AdministrativeOfcDetail,
                                  x.AddSecApproveDate,

                                  x.AdministrativeOfcSignature,
                                  x.AssitantSccSignature,
                                  x.SrAssitantSccSignature,
                                  x.DeputySecSignature,
                                  x.SecSignature,
                                  x.AddSecSignature,
                                  x.SpeakerSignature,

                                  html = x.SpeakerApproveDetail,
                                  t.MemberResolutionDetail,
                                  t.MemberResolutionDate,
                                  t.MemberResolutionFIleURL,
                                  t.AcceptanceComment,
                                  t.ParlSessID,
                                  prl.ParliamentNo,
                                  cnt.ConstitutentBangla,
                                  prl.SessionNo,
                                  t.UserID,
                                  t.RDNo,
                                  em.UserName,
                                  em.BanglaName,
                                  t.Status,
                              }).ToList();
                return thList;
            }
        }

        public object GetResolutionLog(Nullable<int> ResolutionID)
        {
            //using (PMRS_BcEntities db = new PMRS_BcEntities())
            //{
            //    var result = db.ResolutionUpdateLog(ResolutionID).ToList();
            //    return result;
            //}
            return "";
        }

        public List<ApprovalStatu> GetApprovalStatus()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var thList = db.ApprovalStatus.Select(x => x).ToList();
                return thList;
            }
        }

       

       

        public bool SaveAdministrativeApproval(ResolutionApproval master)
        {
            bool isTrue = false;
            IUMode = "I";

            if (master != null)
            {
                using (PMRS_BcEntities obj = new PMRS_BcEntities())
                {
                    try
                    {
                        master.AdministrativeOfcEmpID = Convert.ToInt32(userID);
                        master.AdministrativeOfcApproveDate = DateTime.Now;
                        obj.ResolutionApprovals.Add(master);
                        obj.SaveChanges();

                        isTrue = true;
                        MaxCode = master.ResolutionApproveID.ToString();
                        MaxID = master.ResolutionApproveID.ToString();

                        _adt.InsertAudit("frmAdministrativeApproval", "AdministrativeApproval", IUMode, "", master.ResolutionApproveID);
                        return isTrue;
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException.InnerException.Message.Contains("UNIQUE"))
                        {
                            IUMode = "Unique";
                            return true;
                        }
                    }
                }
            }

            return false;
        }
     

        public bool SaveAssistantApproval(ResolutionApproval master)
        {
            bool isTrue = false;
            IUMode = "I";

            if (master != null)
            {
                using (PMRS_BcEntities obj = new PMRS_BcEntities())
                {
                    try
                    {
                        master.AssitantSccEmpID = Convert.ToInt32(userID);
                        master.AssitantSccApproveDate = DateTime.Now;
                        obj.ResolutionApprovals.Add(master);
                        obj.SaveChanges();

                        isTrue = true;
                        MaxCode = master.ResolutionApproveID.ToString();
                        MaxID = master.ResolutionApproveID.ToString();

                        _adt.InsertAudit("frmAdministrativeApproval", "AdministrativeApproval", IUMode, "", master.ResolutionApproveID);
                        return isTrue;
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException.InnerException.Message.Contains("UNIQUE"))
                        {
                            IUMode = "Unique";
                            return true;
                        }
                    }
                }
            }

            return false;
        }
     

     
        public bool SaveSrAssistantSecApproval(ResolutionApproval master)
        {
            bool isTrue = false;
            IUMode = "I";

            if (master != null)
            {
                using (PMRS_BcEntities obj = new PMRS_BcEntities())
                {
                    try
                    {
                        master.SrAssitantSccEmpID = Convert.ToInt32(userID);
                        master.SrAssitantSccApproveDate = DateTime.Now;
                        obj.ResolutionApprovals.Add(master);
                        obj.SaveChanges();

                        isTrue = true;
                        MaxCode = master.ResolutionApproveID.ToString();
                        MaxID = master.ResolutionApproveID.ToString();

                        _adt.InsertAudit("frmAdministrativeApproval", "AdministrativeApproval", IUMode, "", master.ResolutionApproveID);
                        return isTrue;
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException.InnerException.Message.Contains("UNIQUE"))
                        {
                            IUMode = "Unique";
                            return true;
                        }
                    }
                }
            }

            return false;
        }


        public bool SaveDeputySecretaryApproval(ResolutionApproval master)
        {
            bool isTrue = false;
            IUMode = "I";

            if (master != null)
            {
                using (PMRS_BcEntities obj = new PMRS_BcEntities())
                {
                    try
                    {
                        master.DeputySecEmpID = Convert.ToInt32(userID);
                        master.DeputySecApproveDate = DateTime.Now;
                        obj.ResolutionApprovals.Add(master);
                        obj.SaveChanges();

                        isTrue = true;
                        MaxCode = master.ResolutionApproveID.ToString();
                        MaxID = master.ResolutionApproveID.ToString();

                        _adt.InsertAudit("frmResolutionApproval", "ResolutionApproval", IUMode, "", master.ResolutionApproveID);
                        return isTrue;
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException.InnerException.Message.Contains("UNIQUE"))
                        {
                            IUMode = "Unique";
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        public bool SaveAdditionalSecApproval(ResolutionApproval master)
        {
            bool isTrue = false;
            IUMode = "I";

            if (master != null)
            {
                using (PMRS_BcEntities obj = new PMRS_BcEntities())
                {
                    try
                    {
                        master.AddSecEmpID = Convert.ToInt32(userID);
                        master.AddSecApproveDate = DateTime.Now;
                        obj.ResolutionApprovals.Add(master);
                        obj.SaveChanges();

                        isTrue = true;
                        MaxCode = master.ResolutionApproveID.ToString();
                        MaxID = master.ResolutionApproveID.ToString();

                        _adt.InsertAudit("frmResolutionApproval", "ResolutionApproval", IUMode, "", master.ResolutionApproveID);
                        return isTrue;
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException.InnerException.Message.Contains("UNIQUE"))
                        {
                            IUMode = "Unique";
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        public bool SaveSecApproval(ResolutionApproval master)
        {
            bool isTrue = false;
            IUMode = "I";

            if (master != null)
            {
                using (PMRS_BcEntities obj = new PMRS_BcEntities())
                {
                    try
                    {
                        master.SecEmpID = Convert.ToInt32(userID);
                        master.SecApproveDate = DateTime.Now;
                        obj.ResolutionApprovals.Add(master);
                        obj.SaveChanges();

                        isTrue = true;
                        MaxCode = master.ResolutionApproveID.ToString();
                        MaxID = master.ResolutionApproveID.ToString();

                        _adt.InsertAudit("frmResolutionApproval", "ResolutionApproval", IUMode, "", master.ResolutionApproveID);
                        return isTrue;
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException.InnerException.Message.Contains("UNIQUE"))
                        {
                            IUMode = "Unique";
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        public bool SaveSpeakerApproval(ResolutionApproval master)
        {
            bool isTrue = false;
            IUMode = "I";

            if (master != null)
            {
                using (PMRS_BcEntities obj = new PMRS_BcEntities())
                {
                    try
                    {
                        master.SpeakerEmpID = Convert.ToInt32(userID);
                        master.SpeakerApproveDate = DateTime.Now;
                        obj.ResolutionApprovals.Add(master);
                        obj.SaveChanges();

                        isTrue = true;
                        MaxCode = master.ResolutionApproveID.ToString();
                        MaxID = master.ResolutionApproveID.ToString();

                        _adt.InsertAudit("frmResolutionApproval", "ResolutionApproval", IUMode, "", master.ResolutionApproveID);
                        return isTrue;
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException.InnerException.Message.Contains("UNIQUE"))
                        {
                            IUMode = "Unique";
                            return true;
                        }
                    }
                }
            }

            return false;
        }









        public bool UpdateAdministrativeApproval(ResolutionApproval master)
        {
            bool isTrue = false;
            IUMode = "U";

            if (master != null)
            {
                try
                {
                    using (PMRS_BcEntities db = new PMRS_BcEntities())
                    {
                        var updt = db.ResolutionApprovals.Where(x => x.ResolutionApproveID == master.ResolutionApproveID).FirstOrDefault();

                        if (updt != null)
                        {
                            updt.AdministrativeOfcSignature = master.AdministrativeOfcSignature;
                            updt.AdministrativeOfcApproveStatus = master.AdministrativeOfcApproveStatus;
                            updt.AdministrativeOfcBackStatus = master.AdministrativeOfcBackStatus;
                            updt.AdministrativeOfcDetail = master.AdministrativeOfcDetail;
                            updt.AdministrativeOfcApproveDate = DateTime.Now;
                            updt.AdministrativeOfcEmpID = userID;
                            db.SaveChanges();

                            isTrue = true;
                            MaxCode = updt.ResolutionApproveID.ToString();
                            MaxID = updt.ResolutionApproveID.ToString();

                            _adt.InsertAudit("frmAdministrativeApproval", "AdministrativeApproval", IUMode, "", master.ResolutionApproveID);
                            return isTrue;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.InnerException.Message.Contains("UNIQUE"))
                    {
                        IUMode = "Unique";
                        return true;
                    }
                }
            }
            return false;
        }

        //public bool UpdateAssitantSecApproval(ResolutionApproval master)
        //{
        //    bool isTrue = false;
        //    IUMode = "U";

        //    if (master != null)
        //    {
        //        try
        //        {
        //            using (PMRS_BcEntities db = new PMRS_BcEntities())
        //            {
        //                var updt = db.ResolutionApprovals.Where(x => x.ResolutionApproveID == master.ResolutionApproveID).FirstOrDefault();

        //                if (updt != null)
        //                {
        //                    updt.AssitantSccSignature = master.AssitantSccSignature;
        //                    updt.AssitantSccApproveStatus = master.AssitantSccApproveStatus;
        //                    updt.AssitantSccDetail = master.AssitantSccDetail;
        //                    updt.AssitantSccApproveDate = DateTime.Now;
        //                    updt.AssitantSccEmpID = userID;
        //                    db.SaveChanges();

        //                    isTrue = true;
        //                    MaxCode = updt.ResolutionApproveID.ToString();
        //                    MaxID = updt.ResolutionApproveID.ToString();

        //                    _adt.InsertAudit("frmAdministrativeApproval", "AdministrativeApproval", IUMode, "", master.ResolutionApproveID);
        //                    return isTrue;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            if (ex.InnerException.InnerException.Message.Contains("UNIQUE"))
        //            {
        //                IUMode = "Unique";
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}


        public bool UpdateAssistantSecApproval(ResolutionApproval master)
        {
            bool isTrue = false;
            IUMode = "U";

            if (master != null)
            {
                try
                {
                    using (PMRS_BcEntities db = new PMRS_BcEntities())
                    {
                        var updt = db.ResolutionApprovals.Where(x => x.ResolutionApproveID == master.ResolutionApproveID).FirstOrDefault();

                        if (updt != null)
                        {
                            updt.AssitantSccApproveStatus = master.AssitantSccApproveStatus;
                            updt.AssitantSccBackStatus = master.AssitantSccBackStatus;
                            updt.AssitantSccDetail = master.AssitantSccDetail;
                            updt.AssitantSccApproveDate = DateTime.Now;
                            updt.AssitantSccEmpID = userID;
                            db.SaveChanges();

                            isTrue = true;
                            MaxCode = updt.ResolutionApproveID.ToString();
                            MaxID = updt.ResolutionApproveID.ToString();

                            _adt.InsertAudit("frmAssistantSecApproval", "AssistantSecApproval", IUMode, "", master.ResolutionApproveID);
                            return isTrue;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.InnerException.Message.Contains("UNIQUE"))
                    {
                        IUMode = "Unique";
                        return true;
                    }
                }
            }
            return false;
        }

        public bool UpdateSrAssistantSecApproval(ResolutionApproval master)
        {
            bool isTrue = false;
            IUMode = "U";

            if (master != null)
            {
                try
                {
                    using (PMRS_BcEntities db = new PMRS_BcEntities())
                    {
                        var updt = db.ResolutionApprovals.Where(x => x.ResolutionApproveID == master.ResolutionApproveID).FirstOrDefault();

                        if (updt != null)
                        {
                            updt.SrAssitantSccSignature = master.SrAssitantSccSignature;
                            updt.SrAssitantSccApproveStatus = master.SrAssitantSccApproveStatus;
                            updt.SrAssitantSccBackStatus = master.SrAssitantSccBackStatus;
                            updt.SrAssitantSccDetail = master.SrAssitantSccDetail;
                            updt.SrAssitantSccApproveDate = DateTime.Now;
                            updt.SrAssitantSccEmpID = userID;
                            db.SaveChanges();

                            isTrue = true;
                            MaxCode = updt.ResolutionApproveID.ToString();
                            MaxID = updt.ResolutionApproveID.ToString();

                            _adt.InsertAudit("frmAdministrativeApproval", "AdministrativeApproval", IUMode, "", master.ResolutionApproveID);
                            return isTrue;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.InnerException.Message.Contains("UNIQUE"))
                    {
                        IUMode = "Unique";
                        return true;
                    }
                }
            }
            return false;
        }

        public bool UpdateDeputySecretaryApproval(ResolutionApproval master)
        {
            bool isTrue = false;
            IUMode = "U";

            if (master != null)
            {
                try
                {
                    using (PMRS_BcEntities db = new PMRS_BcEntities())
                    {
                        var updt = db.ResolutionApprovals.Where(x => x.ResolutionApproveID == master.ResolutionApproveID).FirstOrDefault();

                        if (updt != null)
                        {
                            updt.DeputySecSignature = master.DeputySecSignature;
                            updt.DeputySecApproveStatus = master.DeputySecApproveStatus;
                            updt.DeputySecBackStatus = master.DeputySecBackStatus;
                            updt.DeputySecApproveDetail = master.DeputySecApproveDetail;
                            updt.DeputySecApproveDate = DateTime.Now;
                            updt.DeputySecEmpID = userID;
                            db.SaveChanges();

                            isTrue = true;
                            MaxCode = updt.ResolutionApproveID.ToString();
                            MaxID = updt.ResolutionApproveID.ToString();

                            _adt.InsertAudit("frmResolutionApproval", "ResolutionApproval", IUMode, "", master.ResolutionApproveID);
                            return isTrue;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.InnerException.Message.Contains("UNIQUE"))
                    {
                        IUMode = "Unique";
                        return true;
                    }
                }
            }
            return false;
        }
        public bool UpdateAdditionalSecretaryApproval(ResolutionApproval model)
        {
            bool isTrue = false;
            IUMode = "U";
            try
            {
                using (PMRS_BcEntities db = new PMRS_BcEntities())
                {
                    var updt = db.ResolutionApprovals.Where(x => x.ResolutionApproveID == model.ResolutionApproveID).FirstOrDefault();

                    updt.AddSecApproveStatus = model.AddSecApproveStatus;
                    updt.AddSecBackStatus = model.AddSecBackStatus;
                    updt.AddSecApproveDetail = model.AddSecApproveDetail;
                    updt.AddSecApproveDate = DateTime.Now;
                    updt.AddSecEmpID = userID;
                    updt.AddSecSignature = model.AddSecSignature;


                    db.SaveChanges();

                    isTrue = true;
                    MaxCode = updt.ResolutionApproveID.ToString();
                    MaxID = updt.ResolutionApproveID.ToString();

                    _adt.InsertAudit("frmResolutionASApproval", "ResolutionApproval", IUMode, "", updt.ResolutionApproveID);
                    return isTrue;
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("UNIQUE"))
                {
                    IUMode = "Unique";
                    return true;
                }
            }

            return false;
        }

     
     

       



        public bool UpdateSecretaryApproval(ResolutionApproval model)
        {
            bool isTrue = false;
            IUMode = "U";
            try
            {
                using (PMRS_BcEntities db = new PMRS_BcEntities())
                {
                    var updt = db.ResolutionApprovals.Where(x => x.ResolutionApproveID == model.ResolutionApproveID).FirstOrDefault();

                    updt.SecSignature = model.SecSignature;
                    updt.SecApproveStatus = model.SecApproveStatus;
                    updt.SecBackStatus = model.SecBackStatus;
                    updt.SecApproveDetail = model.SecApproveDetail;
                    updt.SecApproveDate = DateTime.Now;
                    updt.SecEmpID = userID;
                    updt.SecSignature = model.SecSignature;
                    db.SaveChanges();

                    isTrue = true;
                    MaxCode = updt.ResolutionApproveID.ToString();
                    MaxID = updt.ResolutionApproveID.ToString();

                    _adt.InsertAudit("frmResolutionSecApproval", "ResolutionApproval", IUMode, "", updt.ResolutionApproveID);
                    return isTrue;
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("UNIQUE"))
                {
                    IUMode = "Unique";
                    return true;
                }
            }

            return false;
        }

        public bool UpdateSpeakerApproval(ResolutionApproval model)
        {
            bool isTrue = false;
            IUMode = "U";
            try
            {
                using (PMRS_BcEntities db = new PMRS_BcEntities())
                {
                    var updt = db.ResolutionApprovals.Where(x => x.ResolutionApproveID == model.ResolutionApproveID).FirstOrDefault();
                    updt.SpeakerApproveStatus = model.SpeakerApproveStatus;
                    updt.SpeakerBackStatus = model.SpeakerBackStatus;
                    updt.SpeakerApproveDetail = model.SpeakerApproveDetail;
                    updt.SpeakerApproveDate = DateTime.Now;
                    updt.SpeakerEmpID = userID;
                    updt.SpeakerSignature = model.SpeakerSignature;
                    db.SaveChanges();

                    isTrue = true;
                    MaxCode = updt.ResolutionApproveID.ToString();
                    MaxID = updt.ResolutionApproveID.ToString();

                    _adt.InsertAudit("frmResolutionSpeakerApproval", "ResolutionApproval", IUMode, "", updt.ResolutionApproveID);
                    return isTrue;
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("UNIQUE"))
                {
                    IUMode = "Unique";
                    return true;
                }
            }

            return false;
        }

        public bool UpdateNofificationStatus(ResolutionApproval model)
        {
            bool isTrue = false;
            IUMode = "U";
            try
            {
                using (PMRS_BcEntities db = new PMRS_BcEntities())
                {
                    var updt = db.ResolutionApprovals.Where(x => x.ResolutionApproveID == model.ResolutionApproveID).FirstOrDefault();
                    updt.NoticeBackStatus = model.NoticeBackStatus;               
                    db.SaveChanges();

                    isTrue = true;
                    MaxCode = updt.ResolutionApproveID.ToString();
                    MaxID = updt.ResolutionApproveID.ToString();

                    _adt.InsertAudit("frmResolutionSpeakerApproval", "ResolutionApproval", IUMode, "", updt.ResolutionApproveID);
                    return isTrue;
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("UNIQUE"))
                {
                    IUMode = "Unique";
                    return true;
                }
            }

            return false;
        }
        

    }
}