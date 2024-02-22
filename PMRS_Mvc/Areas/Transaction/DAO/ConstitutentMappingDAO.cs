using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PMRS_Mvc.Areas.Transaction.DAO
{
    public class ConstitutentMappingDAO :ReturnData
    {
        private readonly AuditTrailLogger _adt = new AuditTrailLogger();
        public object GetConstitutentMappingList()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var thList = (from cn in db.ConstitutentUserMappingInfoes
                              join em in db.EmployeeInfoes on cn.UserID equals em.UserID
                              join xcn in db.ConstitutentInfoes on cn.ConstitutentID equals xcn.ConstitutentID into empGroup
                              from rt in empGroup.DefaultIfEmpty()
                              orderby cn.ParliamentNo
                              select new
                              {
                                  cn.UserMappingID,
                                  cn.ConstitutentID,
                                  cn.ParliamentNo,
                                  cn.UserID,
                                  em.UserName,
                                  rt.ConstitutentArea,
                                  rt.ConstitutentNumber
                              }).ToList();

                return thList;
            }
        }
        public ConstitutentUserMappingInfo GetConstitutentMappingByConstituentId(int ConstitutentID)
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var dptList = db.ConstitutentUserMappingInfoes.Where(f => f.ConstitutentID == ConstitutentID).Select(x => x).ToList().FirstOrDefault();
                return dptList;
            }
        }
        public ConstitutentUserMappingInfo GetConstitutentMappingByUserId(int userId)
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var dptList = db.ConstitutentUserMappingInfoes.Where(f => f.UserID == userId).Select(x => x).ToList().FirstOrDefault();
                return dptList;
            }
        }
        public bool InsertConstitutentMapping(ConstitutentUserMappingInfo master)
        {
            bool isTrue = false;
            IUMode = "I";

            if (master != null)
            {
                using (PMRS_BcEntities obj = new PMRS_BcEntities())
                {
                    try
                    {
                        obj.ConstitutentUserMappingInfoes.Add(master);
                        obj.SaveChanges();

                        isTrue = true;
                        MaxCode = master.UserMappingID.ToString();
                        MaxID = master.UserMappingID.ToString();

                        _adt.InsertAudit("frmConstitutentMapping", "ConstitutentUserMappingInfo", IUMode, "", master.UserMappingID);
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

        public bool UpdateConstitutentMapping(ConstitutentUserMappingInfo master)
        {
            bool isTrue = false;
            IUMode = "U";

            if (master != null)
            {
                try
                {
                    using (PMRS_BcEntities db = new PMRS_BcEntities())
                    {
                        db.ConstitutentUserMappingInfoes.Attach(master);

                        var entry = db.Entry(master);
                        entry.State = EntityState.Modified;

                        entry.Property(e => e.ConstitutentID).IsModified = true;
                        entry.Property(e => e.ParliamentNo).IsModified = true;
                        entry.Property(e => e.UserID).IsModified = true;

                        db.SaveChanges();

                        isTrue = true;
                        MaxCode = master.UserMappingID.ToString();
                        MaxID = master.UserMappingID.ToString();

                        _adt.InsertAudit("frmConstitutentMapping", "ConstitutentUserMappingInfo", IUMode, "", master.UserMappingID);
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

    }
}