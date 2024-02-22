using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PMRS_Mvc.Areas.Transaction.DAO
{
    public class ParliamentSessionInfoDAO : ReturnData
    {
        private readonly AuditTrailLogger _adt = new AuditTrailLogger();

        public List<ParliamentSessionInfo> GetSession()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var thList = db.ParliamentSessionInfoes.Select(x => x).OrderBy(x => x.ParliamentNo).ToList();
                return thList;
            }
        }

        public bool InsertSession(ParliamentSessionInfo master)
        {
            bool isTrue = false;
            IUMode = "I";

            if (master != null)
            {
                using (PMRS_BcEntities obj = new PMRS_BcEntities())
                {
                    try
                    {
                        //---------- Deactivating previous Data ------------
                        var u = obj.ParliamentSessionInfoes.Where(x => x.Status == 1).ToList();
                        u.ForEach(a => { a.Status = 0; } );
                        obj.SaveChanges();
                        //---------- ---------------------- -----------------

                        obj.ParliamentSessionInfoes.Add(master);
                        obj.SaveChanges();

                        isTrue = true;
                        MaxCode = master.ParliamentSessionID.ToString();
                        MaxID = master.ParliamentSessionID.ToString();

                        _adt.InsertAudit("frmParliamentSessionInfo", "ParliamentSessionInfo", IUMode, "", master.ParliamentSessionID);
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

        public bool UpdateSession(ParliamentSessionInfo master)
        {
            bool isTrue = false;
            IUMode = "U";

            if (master != null)
            {
                try
                {
                    using (PMRS_BcEntities db = new PMRS_BcEntities())
                    {
                        //---------- Deactivating previous Data ------------
                        var u = db.ParliamentSessionInfoes.Where(x => x.Status == 1 && x.ParliamentSessionID != master.ParliamentSessionID).ToList();
                        u.ForEach(a => { a.Status = 0; });
                        db.SaveChanges();
                        //---------- ---------------------- -----------------

                        db.ParliamentSessionInfoes.Attach(master);

                        var entry = db.Entry(master);
                        entry.State = EntityState.Modified;

                        entry.Property(e => e.ParliamentNo).IsModified = true;
                        entry.Property(e => e.SessionNo).IsModified = true;
                        entry.Property(e => e.FromDate).IsModified = true;
                        entry.Property(e => e.ToDate).IsModified = true;
                        entry.Property(e => e.Status).IsModified = true;

                        db.SaveChanges();

                        isTrue = true;
                        MaxCode = master.ParliamentSessionID.ToString();
                        MaxID = master.ParliamentSessionID.ToString();

                        _adt.InsertAudit("frmParliamentSessionInfo", "ParliamentSessionInfo", IUMode, "", master.ParliamentSessionID);
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

        public object GetActiveSession()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var dptList = db.ParliamentSessionInfoes.Where(f => f.Status == 1).Select(x => x).OrderBy(x => x.ParliamentNo).ToList();
                return dptList;
            }
        }
    }
}