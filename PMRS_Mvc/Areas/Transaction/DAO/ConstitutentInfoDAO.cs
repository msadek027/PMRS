using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace PMRS_Mvc.Areas.Transaction.DAO
{
    public class ConstitutentInfoDAO : ReturnData
    {
        private readonly AuditTrailLogger _adt = new AuditTrailLogger();
        public object GetConstitutentInfoList()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var thList = db.ConstitutentInfoes.Select(x => x).OrderBy(x => x.ConstitutentArea).ToList();
                return thList;
            }
        }

        public bool InsertConstitutent(ConstitutentInfo master)
        {
            bool isTrue = false;
            IUMode = "I";

            if (master != null)
            {
                using (PMRS_BcEntities obj = new PMRS_BcEntities())
                {
                    try
                    {
                        obj.ConstitutentInfoes.Add(master);
                        obj.SaveChanges();

                        isTrue = true;
                        MaxCode = master.ConstitutentID.ToString();
                        MaxID = master.ConstitutentID.ToString();

                        _adt.InsertAudit("frmConstitutentInfo", "ConstitutentInfo", IUMode, "", master.ConstitutentID);
                        return isTrue;
                    }
                    catch (Exception ex)
                    {
                        // the exception alone won't tell you why it failed...
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

        public bool UpdateConstitutent(ConstitutentInfo master)
        {
            bool isTrue = false;
            IUMode = "U";

            if (master != null)
            {
                try
                {
                    using (PMRS_BcEntities db = new PMRS_BcEntities())
                    {
                        db.ConstitutentInfoes.Attach(master);

                        var entry = db.Entry(master);
                        entry.State = EntityState.Modified;

                        entry.Property(e => e.ConstitutentArea).IsModified = true;
                        entry.Property(e => e.ConstitutentBangla).IsModified = true;
                        entry.Property(e => e.ConstitutentNumber).IsModified = true;
                        entry.Property(e => e.Status).IsModified = true;

                        db.SaveChanges();

                        isTrue = true;
                        MaxCode = master.ConstitutentID.ToString();
                        MaxID = master.ConstitutentID.ToString();

                        _adt.InsertAudit("frmConstitutentInfo", "ConstitutentInfo", IUMode, "", master.ConstitutentID);
                        return isTrue;
                    }
                }
                catch (Exception ex)
                {
                    // the exception alone won't tell you why it failed...
                    if (ex.InnerException.InnerException.Message.Contains("UNIQUE"))
                    {
                        IUMode = "Unique";
                        return true;
                    }
                }
            }
            return false;
        }

        public object GetActiveConstitutentInfoList()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var dptList = db.ConstitutentInfoes.Where(f => f.Status == 1).Select(x => x).OrderBy(x => x.ConstitutentArea).ToList();
                return dptList;
            }
        }
        public ConstitutentInfo GetActiveConstitutentInfoByName(string name)
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var dptList = db.ConstitutentInfoes.Where(f => f.ConstitutentArea == name).Select(x => x).ToList().FirstOrDefault();
                return dptList;
            }
        }
    }
}