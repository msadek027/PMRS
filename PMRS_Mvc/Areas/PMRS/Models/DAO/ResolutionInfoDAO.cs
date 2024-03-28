using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;

namespace PMRS_Mvc.Areas.PMRS.DAO
{
    public class ResolutionInfoDAO : ReturnData
    {
        private readonly AuditTrailLogger _adt = new AuditTrailLogger();

        int userID = Convert.ToInt32(HttpContext.Current.Session["empID"]);
        int Grade = Convert.ToInt32(HttpContext.Current.Session["Grade"]);

        public object GetResolutionList()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {             

                var thList = (from t in db.MemberResolutionInfoes
                              join em in db.EmployeeInfoes on t.UserID equals em.UserID
                              join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                              where t.Status == 1 && (t.IsDraft == false || t.IsDraft==null)  && !(from xxx in db.ResolutionApprovals select (xxx.MemberResolutionID)).Contains(t.MemberResolutionID)
                              select new
                              {
                                  t.MemberResolutionID,
                                  t.MemberResolutionDetail,
                                  t.MemberResolutionDate,
                                  html = t.MemberResolutionDetail,
                                  t.MemberResolutionFIleURL,
                                  t.ParlSessID,
                                  prl.ParliamentNo,
                                  prl.SessionNo,
                                  t.UserID,
                                  t.RDNo,
                                  t.AcceptStatus,
                                  t.EntryType,
                                  AcceptanceComment = t.AcceptanceComment ?? "গ্রহনযোগ্য", 
                                  em.UserName,
                                  em.BanglaName,
                                  t.Status,
                              }).OrderByDescending(x => x.MemberResolutionID).ToList();
                return thList;
            }
        }
        public object GetDraftResolutionList()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {

                var thList = (from t in db.MemberResolutionInfoes
                              join em in db.EmployeeInfoes on t.UserID equals em.UserID
                              join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                              where t.Status == 1 && t.IsDraft== true && !(from xxx in db.ResolutionApprovals select (xxx.MemberResolutionID)).Contains(t.MemberResolutionID)
                              select new
                              {
                                  t.MemberResolutionID,
                                  t.MemberResolutionDetail,
                                  t.MemberResolutionDate,
                                  html = t.MemberResolutionDetail,
                                  t.MemberResolutionFIleURL,
                                  t.ParlSessID,
                                  prl.ParliamentNo,
                                  prl.SessionNo,
                                  t.UserID,
                                  t.RDNo,
                                  t.AcceptStatus,
                                  t.EntryType,
                                  t.AcceptanceComment,
                                  em.UserName,
                                  em.BanglaName,
                                  t.Status,
                              }).OrderByDescending(x => x.MemberResolutionID).ToList();
                return thList;
            }
        }

        internal object GetWorkflow()
        {
            
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                Grade = Grade == 0 ? 13 : Grade;
                 var thList = (from a in db.EmployeeInfoes
                              join b in db.RLConfs on a.UserID equals b.Emp_ID
                              join c in db.DesignationInfoes on a.DesignationID equals c.DesignationID
                              where /*b.RL_ID != 1 && */ new[] {1, 6, 7, 8, 9, 10, 11, 12 }.Contains(b.RL_ID)
                              select new
                              {
                                  Grade = c.Grade,
                                  UserName = a.BanglaName,
                                  DesignationNameBN = c.DesignationNameBN
                              })
                 .AsEnumerable() // Bring the data into memory
                 //.Where(x => Convert.ToInt16(x.Grade) < Grade) // Perform the conversion in memory
                 .OrderByDescending(x => int.Parse(x.Grade)) // Perform the ordering in memory
                 .ToList(); // Materialize the query




 

                //var thList = (from a in db.EmployeeInfoes
                //              join b in db.RLConfs on a.UserID equals b.Emp_ID
                //              join c in db.DesignationInfoes on a.DesignationID equals c.DesignationID
                //              where b.RL_ID != 1 && int.Parse(c.Grade) < Grade && new[] { 6, 7, 8, 9, 10, 11, 12 }.Contains(b.RL_ID)

                //              select new
                //              {

                //                  Grade = c.Grade,
                //                  UserName = a.UserName,
                //                  DesignationNameBN = c.DesignationNameBN
                //              }).ToList() // Fetch the data first
                //   .OrderByDescending(x => int.Parse(x.Grade)) // Then perform the conversion
                //   .ToList(); // Materialize the query


                return thList;
            }
        }

        public object GetSentResolutionList()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                
                var thList = (from t in db.MemberResolutionInfoes
                              join apr in db.ResolutionApprovals on t.MemberResolutionID equals apr.MemberResolutionID
                              join em in db.EmployeeInfoes on t.UserID equals em.UserID
                              join prl in db.ParliamentSessionInfoes on t.ParlSessID equals prl.ParliamentSessionID
                              where t.AcceptStatus=="true"
                              select new
                              {
                                  t.MemberResolutionID,
                                  t.MemberResolutionDetail,
                                  t.MemberResolutionDate,
                                  html = t.MemberResolutionDetail,
                                  t.MemberResolutionFIleURL,
                                  t.ParlSessID,

                                  apr.AdministrativeOfcSignature,
                                  apr.AssitantSccSignature,
                                  apr.SrAssitantSccSignature,                             
                                  apr.DeputySecSignature,
                                  apr.AddSecSignature,
                                  apr.SecSignature,                            
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
                                  t.Status,
                              }).OrderByDescending(x => x.MemberResolutionID).ToList();
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
                        var chkQry = "Select count(*) SLNO from MemberResolutionInfo where ParlSessID = " + master.ParlSessID + " AND RDNo=N'"+ master.RDNo +"' ";
                        var rdChk = obj.Database.SqlQuery<int>(chkQry).First();

                        if (Convert.ToInt32(rdChk) > 0)
                        {
                            IUMode = "Unique";
                            return true;
                        }
                        else
                        {

                            string[] dtFormat = { "dd-MMM-yyyy hh:mm:ss tt", "dd/MM/yyyy hh:mm", "dd-MMM-yy h:mm:ss tt", "dd/MM/yyyy H:m", "dd/MM/yyyy H:mm" };

                            var sql = "Select count(*) + 1 as SLNO from MemberResolutionInfo where ParlSessID = " + master.ParlSessID + "";
                            var total = obj.Database.SqlQuery<int>(sql).First();
                       

                           // DateTime mstDt = DateTime.ParseExact(master.MemberResolutionDateStr, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                            DateTime mstDt = DateTime.ParseExact(master.MemberResolutionDate.ToString(), dtFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);

                            master.MemberResolutionDate = mstDt;
                            master.ResolutionSLNo = Convert.ToInt32(total);
                            master.AcceptStatus = "True";
                            master.EntryType = "System";
                            master.EnteredBy = userID;

                            obj.MemberResolutionInfoes.Add(master);
                            obj.SaveChanges();

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

                        else
                        {
                            Console.WriteLine(ex.Message.ToString());
                            return false;
                        }
                    }
                }
            }

            return false;
        }
        public bool DraftResolution(MemberResolutionInfo master)
        {
            bool isTrue = false;
            IUMode = "U";

            if (master != null)
            {
                try
                {
                    if (master.AcceptanceComment == "সুস্পষ্ট প্রস্তাব নয় বিধায় বাতিলযোগ্য ১৩৩(১) বিধি")
                    {
                        master.AcceptStatus = "false";
                    }
                    else
                    {
                        master.AcceptStatus = "True";
                    }




                    using (PMRS_BcEntities db = new PMRS_BcEntities())
                    {
                        var chkQry = "Select count(*) SLNO from MemberResolutionInfo where ParlSessID = " + master.ParlSessID + " AND RDNo=N'" + master.RDNo + "' AND MemberResolutionID != " + master.MemberResolutionID + " ";
                        var rdChk = db.Database.SqlQuery<int>(chkQry).First();

                        if (Convert.ToInt32(rdChk) > 0)
                        {
                            IUMode = "Unique";
                            return true;
                        }
                        else
                        {
                            db.MemberResolutionInfoes.Attach(master);

                            var entry = db.Entry(master);
                            entry.State = EntityState.Modified;

                            entry.Property(e => e.MemberResolutionDetail).IsModified = true;
                            entry.Property(e => e.MemberResolutionFIleURL).IsModified = true;
                            entry.Property(e => e.ParlSessID).IsModified = true;
                            entry.Property(e => e.UserID).IsModified = true;
                            entry.Property(e => e.AcceptanceComment).IsModified = true;
                            entry.Property(e => e.Status).IsModified = true;
                            entry.Property(e => e.AcceptStatus).IsModified = true;
                            entry.Property(e => e.RDNo).IsModified = true;

                            entry.Property(e => e.ResolutionSLNo).IsModified = false;
                            entry.Property(e => e.EntryType).IsModified = false;
                            entry.Property(e => e.MemberResolutionDate).IsModified = false;

                            entry.Property(e => e.IsDraft).IsModified = true;

                            db.SaveChanges();

                            isTrue = true;
                            MaxCode = master.MemberResolutionID.ToString();
                            MaxID = master.MemberResolutionID.ToString();

                            _adt.InsertAudit("frmResolutionInfo", "MemberResolutionInfo", IUMode, "", master.MemberResolutionID);
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
        public bool UpdateResolution(MemberResolutionInfo master)
        {
            bool isTrue = false;
            IUMode = "U";

            if (master != null)
            {
                try
                {
                    if(master.AcceptanceComment=="সুস্পষ্ট প্রস্তাব নয় বিধায় বাতিলযোগ্য ১৩৩(১) বিধি")
                    {
                        master.AcceptStatus = "false";
                    }
                    else
                    {
                        master.AcceptStatus = "True";
                    }
                        
                        
                      

                    using (PMRS_BcEntities db = new PMRS_BcEntities())
                    {

                        var QryGenRdNo = "SELECT RIGHT('0000' + CAST(ISNULL(MAX(RDNo), 0) + 1 AS VARCHAR), 4) AS NewID FROM MemberResolutionInfo ";
                        var rdGenRdNo = db.Database.SqlQuery<string>(QryGenRdNo).First();

                        master.RDNo = rdGenRdNo.ToString();
                 
                        
                        var chkQry = "Select count(*) SLNO from MemberResolutionInfo where ParlSessID = " + master.ParlSessID + " AND RDNo=N'" + master.RDNo + "' AND MemberResolutionID != "+ master.MemberResolutionID +" ";
                        var rdChk = db.Database.SqlQuery<int>(chkQry).First();

                        if (Convert.ToInt32(rdChk) > 0)
                        {
                            IUMode = "Unique";
                            return true;
                        }
                        else
                        {
                            db.MemberResolutionInfoes.Attach(master);

                            var entry = db.Entry(master);
                            entry.State = EntityState.Modified;

                            entry.Property(e => e.MemberResolutionDetail).IsModified = true;                   
                            entry.Property(e => e.MemberResolutionFIleURL).IsModified = true;
                            entry.Property(e => e.ParlSessID).IsModified = true;
                            entry.Property(e => e.UserID).IsModified = true;
                            entry.Property(e => e.AcceptanceComment).IsModified = true;
                            entry.Property(e => e.Status).IsModified = true;
                            entry.Property(e => e.AcceptStatus).IsModified = true;
                            entry.Property(e => e.RDNo).IsModified = true;

                            entry.Property(e => e.ResolutionSLNo).IsModified = false;
                            entry.Property(e => e.EntryType).IsModified = false;
                            entry.Property(e => e.MemberResolutionDate).IsModified = false;


                            db.SaveChanges();

                            isTrue = true;
                            MaxCode = master.MemberResolutionID.ToString();
                            MaxID = master.MemberResolutionID.ToString();

                            _adt.InsertAudit("frmResolutionInfo", "MemberResolutionInfo", IUMode, "", master.MemberResolutionID);
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

        public bool UpdateUpload(int ID, string filepath)
        {
            bool isTrue = false;
            IUMode = "U";
            try
            {
                using (PMRS_BcEntities db = new PMRS_BcEntities())
                {
                    var updt = db.MemberResolutionInfoes.Where(x => x.MemberResolutionID == ID).FirstOrDefault();
                    updt.MemberResolutionFIleURL = filepath;
                    db.SaveChanges();

                    isTrue = true;
                    MaxCode = ID.ToString();
                    MaxID = ID.ToString();

                    //_adt.InsertAudit("frmPrioritySet", "ResolutionApproval", IUMode, "", ID);
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