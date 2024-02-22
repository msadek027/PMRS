using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PMRS_Mvc.Areas.Security.DAO
{
    public class RLDAO : ReturnData
    {
        public List<RL> GetRoleList()
        {
            using (SCEntities db = new SCEntities())
            {
                var rlList = db.RLs.Select(x => x).OrderBy(y => y.Nm).ToList();
                return rlList;
            }
        }

        public bool InsertRL(RL master)
        {
            bool isTrue = false;
            IUMode = "I";

            if (master != null)
            {
                using (SCEntities obj = new SCEntities())
                {
                    int mxId = obj.RLs.Select(x => x.ID).DefaultIfEmpty(0).Max();
                    master.ID = mxId + 1;

                    try
                    {
                        obj.RLs.Add(master);
                        obj.SaveChanges();

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

            return false;
        }

        public bool UpdateRL(RL master)
        {
            bool isTrue = false;
            IUMode = "U";

            if (master != null)
            {
                try
                {
                    using (SCEntities db = new SCEntities())
                    {
                        db.RLs.Attach(master);

                        var entry = db.Entry(master);
                        entry.State = EntityState.Modified;

                        entry.Property(e => e.Nm).IsModified = true;
                        entry.Property(e => e.Priority).IsModified = true;

                        db.SaveChanges();

                        isTrue = true;
                        MaxCode = master.ID.ToString();
                        return isTrue;
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