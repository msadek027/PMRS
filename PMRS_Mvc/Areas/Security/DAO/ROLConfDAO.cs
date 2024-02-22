using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Linq;

namespace PMRS_Mvc.Areas.Security.DAO
{
    public class ROLConfDAO : ReturnData
    {

        public RLConf GetRoleConfByEmployeeId(int empId)
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var dptList = db.RLConfs.Where(f => f.Emp_ID == empId).Select(x => x).ToList().FirstOrDefault();
                return dptList;
            }
        }
        public bool SaveRLConf(RLConf master)
        {
            bool isTrue = false;

            if (master != null)
            {
                try
                {
                    using (SCEntities db = new SCEntities())
                    {
                        var rlUpdt = (from t in db.RLConfs
                                      where t.Emp_ID == master.Emp_ID
                                      select t).FirstOrDefault();

                        if (rlUpdt != null)
                        {
                            IUMode = "U";

                            rlUpdt.RL_ID = master.RL_ID;
                            db.SaveChanges();

                            isTrue = true;
                            return isTrue;
                        }

                        int mxId = db.RLConfs.Select(x => x.ID).DefaultIfEmpty(0).Max();
                        master.ID = mxId + 1;

                        try
                        {
                            db.RLConfs.Add(master);
                            db.SaveChanges();

                            IUMode = "I";

                            isTrue = true;
                            MaxCode = master.ID.ToString();
                            return isTrue;
                        }
                        catch (Exception)
                        {
                            return isTrue;
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
    }
}