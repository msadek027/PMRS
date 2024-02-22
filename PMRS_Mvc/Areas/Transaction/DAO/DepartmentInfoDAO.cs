using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PMRS_Mvc.Areas.Transaction.DAO
{
    public class DepartmentInfoDAO : ReturnData
    {
        private readonly AuditTrailLogger _adt = new AuditTrailLogger();

        public List<DepartmentInfo> GetDepartmentInfoList()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var thList = db.DepartmentInfoes.Select(x => x).OrderBy(x => x.DepartmentName).ToList();
                return thList;
            }
        }
        public DepartmentInfo GetDepartmentInfoById(int id)
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var thList = db.DepartmentInfoes.Where(x => x.DepartmentID == id).Select(x => x).OrderBy(x => x.DepartmentName).ToList().FirstOrDefault();
                return thList;
            }
        }
        public DepartmentInfo GetDepartmentInfoByName(string name)
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var thList = db.DepartmentInfoes.Where(x => x.DepartmentName == name).Select(x => x).OrderBy(x => x.DepartmentName).ToList().FirstOrDefault();
                return thList;
            }
        }
        public bool InsertDepartment(DepartmentInfo master)
        {
            bool isTrue = false;
            IUMode = "I";

            if (master != null)
            {
                using (PMRS_BcEntities obj = new PMRS_BcEntities())
                {
                    //int mxId = obj.DepartmentInfoes.Select(x => x.DepartmentID).Count();
                    var mxCode = obj.DepartmentInfoes.Max(s => s.DepartmentCode);
                    int mxId = Convert.ToInt32(mxCode ?? "0");
                    master.DepartmentCode = (mxId + 1).ToString("0000");

                    try
                    {
                        obj.DepartmentInfoes.Add(master);
                        obj.SaveChanges();

                        isTrue = true;
                        MaxCode = master.DepartmentCode;
                        MaxID = master.DepartmentID.ToString();

                        _adt.InsertAudit("frmDepartmentInfo", "DepartmentInfo", IUMode, "", master.DepartmentID);
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

        public bool UpdateDepartment(DepartmentInfo master)
        {
            bool isTrue = false;
            IUMode = "U";

            if (master != null)
            {
                try
                {
                    using (PMRS_BcEntities db = new PMRS_BcEntities())
                    {
                        db.DepartmentInfoes.Attach(master);

                        var entry = db.Entry(master);
                        entry.State = EntityState.Modified;

                        entry.Property(e => e.DepartmentName).IsModified = true;
                        entry.Property(e => e.Status).IsModified = true;

                        db.SaveChanges();

                        isTrue = true;
                        MaxCode = master.DepartmentCode;
                        MaxID = master.DepartmentID.ToString();

                        _adt.InsertAudit("frmDepartmentInfo", "DepartmentInfo", IUMode, "", master.DepartmentID);
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

        public object GetActiveDepartmentInfoList()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var dptList = db.DepartmentInfoes.Where(f => f.Status == 1).Select(x => x).OrderBy(x => x.DepartmentName).ToList();
                return dptList;
            }
        }
    }
}