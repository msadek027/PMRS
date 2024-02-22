using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Linq;

namespace PMRS_Mvc.Areas.PMRS.DAO
{
    public class PrioritySetDAO : ReturnData
    {
        private readonly AuditTrailLogger _adt = new AuditTrailLogger();

        public object GetResolutionBySession(int sessionID)
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var thList = (from t in db.MemberResolutionInfoes
                              join apr in db.ResolutionApprovals on t.MemberResolutionID equals apr.MemberResolutionID
                              join em in db.EmployeeInfoes on t.UserID equals em.UserID
                              join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                              join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                              join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                              where prl.ParliamentSessionID == sessionID && apr.AddSecApproveStatus == "1" && map.ParliamentNo == prl.ParliamentNo.ToString()
                              &&  apr.SpeakerApproveStatus == "1"
                              && apr.MemberResPriority == null
                              select new
                              {
                                  apr.ResolutionApproveID,
                                  apr.DeputySecSignature,                               
                                  apr.SecSignature,
                                  apr.AddSecSignature,
                                  apr.SpeakerSignature,
                                  t.MemberResolutionID,
                                  apr.SpeakerApproveDetail,
                                  html = apr.SpeakerApproveDetail,
                                  t.MemberResolutionDate,
                                  t.MemberResolutionFIleURL,
                                  t.ParlSessID,
                                  cnt.ConstitutentBangla,
                                  prl.ParliamentNo,
                                  prl.SessionNo,
                                  t.UserID,
                                  t.RDNo,
                                  em.BanglaName,
                                  em.UserName,
                                  t.Status,
                                  apr.MemberResPriority,
                              }).ToList();
                return thList;
            }
        }
        public object GetSentResolutionBySession(int sessionID)
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var thList = (from t in db.MemberResolutionInfoes
                              join apr in db.ResolutionApprovals on t.MemberResolutionID equals apr.MemberResolutionID
                              join em in db.EmployeeInfoes on t.UserID equals em.UserID
                              join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                              join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                              join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                              where prl.ParliamentSessionID == sessionID && apr.AddSecApproveStatus == "1" && map.ParliamentNo == prl.ParliamentNo.ToString()
                              && apr.SpeakerApproveStatus == "1"
                              && apr.MemberResPriority != null
                              select new
                              {
                                  t.MemberResolutionID,
                                  t.MemberResolutionDetail,
                                  t.MemberResolutionDate,
                                  html = t.MemberResolutionDetail,
                                  t.MemberResolutionFIleURL,
                                  t.ParlSessID,
                                  apr.DeputySecSignature,
                                
                                  apr.SecSignature,
                                  apr.AddSecSignature,
                                  apr.SpeakerSignature,
                                  prl.ParliamentNo,
                                  prl.SessionNo,
                                  t.UserID,
                                  t.RDNo,
                                  t.AcceptStatus,
                                  t.EntryType,
                                  t.AcceptanceComment,
                                  em.UserName,
                                  em.BanglaName,
                                  t.Status
                              }).ToList();
                return thList;
            }
        }
        public bool UpdatePriority(int resID, int priority)
        {
            bool isTrue = false;
            IUMode = "U";
            try
            {
                using (PMRS_BcEntities db = new PMRS_BcEntities())
                {
                    var updt = db.ResolutionApprovals.Where(x => x.ResolutionApproveID == resID).FirstOrDefault();
                    updt.MemberResPriority = priority;
                    db.SaveChanges();

                    isTrue = true;
                    MaxCode = resID.ToString();
                    MaxID = resID.ToString();

                    _adt.InsertAudit("frmPrioritySet", "ResolutionApproval", IUMode, "", resID);
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