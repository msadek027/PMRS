using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PMRS_Mvc.Areas.Transaction.DAO
{
    public class DesignationInfoDAO : ReturnData
    {
        private readonly AuditTrailLogger _adt = new AuditTrailLogger();

        public List<DesignationInfo> GetDesignationInfoList()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var thList = db.DesignationInfoes.Select(x => x).OrderBy(x => x.DesignationName).ToList();
                return thList;
            }
        }
        public DesignationInfo GetDesignationInfoById(int degId)
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var thList = db.DesignationInfoes.Where(x => x.DesignationID == degId).Select(x => x).OrderBy(x => x.DesignationName).ToList().FirstOrDefault();
                return thList;
            }
        }
        public DesignationInfo GetDesignationInfoByName(string degName)
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var thList = db.DesignationInfoes.Where(x => x.DesignationName == degName).Select(x => x).OrderBy(x => x.DesignationName).ToList().FirstOrDefault();
                return thList;
            }
        }
        public int InsertDesignation(DesignationInfo master)
        {
            if(master.DesignationName.Length > 50)
            {
                master.DesignationName = master.DesignationName.Split('(')[0];
            }
            bool isTrue = false;
            IUMode = "I";

            if (master != null)
            {
                using (PMRS_BcEntities obj = new PMRS_BcEntities())
                {
                    var mxCode = obj.DesignationInfoes.Max(s => s.DesignationCode);
                    int mxId = Convert.ToInt32(mxCode ?? "0");
                    master.DesignationCode = (mxId + 1).ToString("0000");

                    try
                    {
                        obj.DesignationInfoes.Add(master);
                        obj.SaveChanges();

                        isTrue = true;
                        MaxCode = master.DesignationCode;
                        MaxID = master.DesignationID.ToString();

                        _adt.InsertAudit("frmDesignationInfo", "DesignationInfo", IUMode, "", master.DesignationID);
                        return master.DesignationID;
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException.InnerException.Message.Contains("UNIQUE"))
                        {
                            IUMode = "Unique";
                            return 0;
                        }
                    }
                }
            }

            return 0;
        }

        public bool UpdateDesignation(DesignationInfo master)
        {
            bool isTrue = false;
            IUMode = "U";

            if (master != null)
            {
                try
                {
                    using (PMRS_BcEntities db = new PMRS_BcEntities())
                    {
                        try
                        {
                            db.DesignationInfoes.Attach(master);

                            var entry = db.Entry(master);
                            entry.State = EntityState.Modified;

                            entry.Property(e => e.DesignationName).IsModified = true;
                            entry.Property(e => e.Status).IsModified = true;

                            db.SaveChanges();

                            isTrue = true;
                            MaxCode = master.DesignationCode;
                            MaxID = master.DesignationID.ToString();

                            _adt.InsertAudit("frmDesignationInfo", "DesignationInfo", IUMode, "", master.DesignationID);
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
                catch (Exception)
                {
                    return isTrue;
                }
            }
            return false;
        }

        public object GetActiveDesignationInfoList()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                try
                {
                    var dsList = db.DesignationInfoes.Where(f => f.Status == 1).Select(x => x).OrderBy(x => x.DesignationName).ToList();
                    return dsList;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}