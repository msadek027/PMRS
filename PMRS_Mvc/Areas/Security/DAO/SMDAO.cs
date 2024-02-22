using PMRS_Mvc.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace PMRS_Mvc.Areas.Security.DAO
{
    public class SMDAO
    {
        private readonly SCEntities _db = new SCEntities();
        public IQueryable GetSubMenuList()
        {
            var mhList = from t in _db.SecMHs
                         join u in _db.SecSMs on t.ID equals u.MH_ID
                         orderby t.Seq, u.Seq
                         select new
                         {
                             u.ID,
                             u.MH_ID,
                             t.Nm,
                             Subname = u.Nm,
                             u.Url,
                             u.Seq,
                             u.CssClass
                         };

            return mhList;
        }

        public IQueryable GetSubMenuListNyMHID(int MH_ID)
        {
            var mhList = _db.SecSMs.Where(x => x.MH_ID == MH_ID).Select(x => x);
            return mhList;
        }

        public string InsertSm(SecSM secSm)
        {
            if (secSm != null)
            {
                int mxId = _db.SecSMs.Max(x => x.ID);

                secSm.ID = mxId + 1;

                using (SCEntities obj = new SCEntities())
                {
                    try
                    {
                        obj.SecSMs.Add(secSm);
                        obj.SaveChanges();
                        return "Sub Menu added Successfully";
                    }
                    catch (Exception)
                    {
                        return "Not Saved! Try Again";
                    }
                }
            }
            return "Not Saved! Try Again";
        }

        public string UpdateSm(SecSM secSm)
        {
            if (secSm != null)
            {
                try
                {
                    _db.SecSMs.Attach(secSm);

                    var entry = _db.Entry(secSm);
                    entry.State = EntityState.Modified;

                    entry.Property(e => e.CssClass).IsModified = true;
                    entry.Property(e => e.Nm).IsModified = true;
                    entry.Property(e => e.Seq).IsModified = true;
                    entry.Property(e => e.Url).IsModified = true;
                    entry.Property(e => e.MH_ID).IsModified = true;

                    _db.SaveChanges();

                    return "Sub Menu Updated Successfully";
                }
                catch
                {
                    return "Not Saved! Try Again";
                }
            }
            return "Not Saved! Try Again";
        }
    }
}