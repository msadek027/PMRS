using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PMRS_Mvc.Areas.PMRS.DAO
{
    public class BallotInfoDAO : ReturnData
    {
        private readonly AuditTrailLogger _adt = new AuditTrailLogger();
        int userID = Convert.ToInt32(HttpContext.Current.Session["empID"]);

        public object GetResolutionForBalloting(int sessionID)
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var thList = (from t in db.MemberResolutionInfoes
                              join apr in db.ResolutionApprovals on t.MemberResolutionID equals apr.MemberResolutionID
                              join em in db.EmployeeInfoes on t.UserID equals em.UserID
                              join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                              join map in db.ConstitutentUserMappingInfoes on em.UserID equals map.UserID
                              join cnt in db.ConstitutentInfoes on map.ConstitutentID equals cnt.ConstitutentID
                              where prl.ParliamentSessionID == sessionID && apr.SpeakerApproveStatus == "1" && apr.MemberResPriority == 1 && map.ParliamentNo == prl.ParliamentNo.ToString()
                              && !(from xxx in db.BallotInfoes select (xxx.ResolutionApproveID)).Contains(apr.ResolutionApproveID)
                              select new
                              {
                                  apr.ResolutionApproveID,
                                  t.MemberResolutionID,
                                  apr.SpeakerApproveDetail,
                                  html = apr.SpeakerApproveDetail,
                                  t.MemberResolutionDate,
                                  t.MemberResolutionFIleURL,
                                  t.ParlSessID,
                                  prl.ParliamentNo,
                                  cnt.ConstitutentBangla,
                                  prl.SessionNo,
                                  t.UserID,
                                  t.RDNo,
                                  em.UserName,
                                  em.BanglaName,
                                  t.Status,
                                  apr.MemberResPriority,
                              }).ToList();
                return thList;
            }
        }

        public bool InsertBallot(BallotInfo master)
        {
            bool isTrue = false;
            IUMode = "I";

            if (master != null)
            {
                using (PMRS_BcEntities obj = new PMRS_BcEntities())
                {
                    try
                    {
                        master.BallotEmpID = Convert.ToInt32(userID);
                        master.BallotDate = DateTime.Now;
                        obj.BallotInfoes.Add(master);
                        obj.SaveChanges();

                        isTrue = true;
                        MaxCode = master.BallotID.ToString();
                        MaxID = master.BallotID.ToString();

                        _adt.InsertAudit("frmBallotInfo", "BallotInfo", IUMode, "", master.BallotID);
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
    }
}