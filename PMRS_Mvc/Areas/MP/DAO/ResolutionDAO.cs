using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PMRS_Mvc.Areas.MP.DAO
{
    public class ResolutionDAO : ReturnData
    {
        private readonly AuditTrailLogger _adt = new AuditTrailLogger();
        private readonly int UserID = Convert.ToInt32(HttpContext.Current.Session["empID"]);

        public object GetResolutionListForIndividualMP()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var thList = (from t in db.MemberResolutionInfoes
                              join em in db.EmployeeInfoes on t.UserID equals em.UserID
                              join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                              where t.AcceptStatus == "False" && t.EntryType == "MP" && t.UserID == UserID
                              select new
                              {
                                  t.MemberResolutionID,
                                  t.MemberResolutionDetail,
                                  t.MemberResolutionDate,
                                  t.MemberResolutionFIleURL,
                                  t.ParlSessID,
                                  prl.ParliamentNo,
                                  prl.SessionNo,
                                  t.UserID,
                                  t.RDNo,
                                  t.AcceptanceComment,
                                  em.UserName,
                                  em.BanglaName,
                                  t.Status,
                              }).ToList();
                return thList;
            }
        }

        public object GetSentResolutionListForIndividualMP()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var thList = (from t in db.MemberResolutionInfoes
                              join em in db.EmployeeInfoes on t.UserID equals em.UserID
                              join apr in db.ResolutionApprovals on t.MemberResolutionID equals apr.MemberResolutionID
                              join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                              where t.AcceptStatus == "True" && t.EntryType == "MP" && t.UserID == UserID
                              select new
                              {
                                  t.MemberResolutionID,
                                  t.MemberResolutionDetail,
                                  t.MemberResolutionDate,
                                  t.MemberResolutionFIleURL,
                                  apr.DeputySecSignature,
                                
                                  apr.SecSignature,
                                  apr.AddSecSignature,
                                  apr.SpeakerSignature,
                                  t.ParlSessID,
                                  prl.ParliamentNo,
                                  prl.SessionNo,
                                  t.UserID,
                                  t.RDNo,
                                  t.AcceptanceComment,
                                  em.UserName,
                                  em.BanglaName,
                                  t.Status,
                              }).ToList();
                return thList;
            }
        }
        public bool InsertResolution(MemberResolutionInfo master)
        {
            bool isTrue = false;
            IUMode = "I";

            if (master != null)
            {
                using (PMRS_BcEntities obj = new PMRS_BcEntities())
                {
                    try
                    {
                        var sql = "Select count(*) + 1 as SLNO from MemberResolutionInfo where ParlSessID = " + master.ParlSessID + "";
                        var total = obj.Database.SqlQuery<int>(sql).First();

                        master.UserID = UserID;
                        master.MemberResolutionDate = DateTime.Now;
                        master.ResolutionSLNo = Convert.ToInt32(total);
                        master.AcceptStatus = "False";
                        master.EntryType = "MP";

                        obj.MemberResolutionInfoes.Add(master);
                        obj.SaveChanges();

                        isTrue = true;
                        MaxCode = master.MemberResolutionID.ToString();
                        MaxID = master.MemberResolutionID.ToString();

                        _adt.InsertAudit("frmResolutionInfo", "MemberResolutionInfo", IUMode, "", master.MemberResolutionID);
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

        public bool UpdateResolution(MemberResolutionInfo master)
        {
            bool isTrue = false;
            IUMode = "U";

            if (master != null)
            {
                try
                {
                    using (PMRS_BcEntities db = new PMRS_BcEntities())
                    {
                        db.MemberResolutionInfoes.Attach(master);

                        var entry = db.Entry(master);
                        entry.State = EntityState.Modified;

                        entry.Property(e => e.MemberResolutionDetail).IsModified = true;
                        entry.Property(e => e.AcceptanceComment).IsModified = true;
                        entry.Property(e => e.Status).IsModified = true;

                        entry.Property(e => e.MemberResolutionFIleURL).IsModified = false;
                        entry.Property(e => e.ParlSessID).IsModified = false;
                        entry.Property(e => e.MemberResolutionDate).IsModified = false;
                        entry.Property(e => e.ResolutionSLNo).IsModified = false;
                        entry.Property(e => e.EntryType).IsModified = false;
                        entry.Property(e => e.AcceptStatus).IsModified = false;
                        entry.Property(e => e.UserID).IsModified = false;

                        db.SaveChanges();

                        isTrue = true;
                        MaxCode = master.MemberResolutionID.ToString();
                        MaxID = master.MemberResolutionID.ToString();

                        _adt.InsertAudit("frmResolutionInfo", "MemberResolutionInfo", IUMode, "", master.MemberResolutionID);
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
            }
            return false;
        }

    }
}