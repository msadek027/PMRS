using System;
using PMRS_Mvc.Models;
using System.Data.Entity;
using System.Linq;
using PMRS_Mvc.Common;

namespace PMRS_Mvc.Areas.Security.DAO
{
    public class MNConfDAO : ReturnData
    {
        public IQueryable GetMNConfListByMHRL(int rlID, int mhID)
        {
            SCEntities db = new SCEntities();

            var mnConfList = from t in db.MNConfs
                             join u in db.SecSMs on t.SM_ID equals u.ID
                             join v in db.SecMHs on t.MH_ID equals v.ID
                             join w in db.RLs on t.RL_ID equals w.ID
                             where t.RL_ID == rlID && t.MH_ID == mhID
                             orderby v.Seq
                             select new
                             {
                                 t.ID,
                                 RL_ID = w.ID,
                                 RL_NM = w.Nm,
                                 SM_ID = u.ID,
                                 SM_NM = u.Nm,
                                 MH_ID = v.ID,
                                 MH_NM = v.Nm,
                                 t.Sv,
                                 t.Dl,
                                 t.Vw,
                             };

            return mnConfList;
        }

        public bool InsertMNConf(MNConf mnConf)
        {
            if (mnConf != null)
            {
                using (SCEntities obj = new SCEntities())
                {
                    var dbchk = obj.MNConfs.FirstOrDefault(x => x.RL_ID == mnConf.RL_ID && x.SM_ID == mnConf.SM_ID && x.MH_ID == mnConf.MH_ID);

                    if (dbchk != null)
                    {
                        IUMode = "Unique";
                        return true;
                    }

                    IUMode = "I";
                    int mxId = obj.MNConfs.DefaultIfEmpty().Max(x => x.ID);
                    mnConf.ID = mxId + 1;

                    try
                    {
                        obj.MNConfs.Add(mnConf);
                        obj.SaveChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public bool UpdateMNConf(MNConf mnConf)
        {
            if (mnConf != null)
            {
                try
                {
                    using (SCEntities db = new SCEntities())
                    {
                        db.MNConfs.Attach(mnConf);

                        var entry = db.Entry(mnConf);
                        entry.State = EntityState.Modified;

                        entry.Property(e => e.MH_ID).IsModified = true;
                        entry.Property(e => e.RL_ID).IsModified = true;
                        entry.Property(e => e.SM_ID).IsModified = true;

                        entry.Property(e => e.Sv).IsModified = true;
                        entry.Property(e => e.Dl).IsModified = true;

                        db.SaveChanges();

                        IUMode = "U";

                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public bool DeleteMNConf(int ID)
        {
            try
            {
                using (SCEntities db = new SCEntities())
                {
                    IUMode = "D";

                    var delMnConf = (from t in db.MNConfs
                                     where t.ID == ID
                                     select t).FirstOrDefault();
                    if (delMnConf != null)
                    {
                        db.MNConfs.Remove(delMnConf);
                        db.SaveChanges();

                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }

            return false;
        }
    }
}