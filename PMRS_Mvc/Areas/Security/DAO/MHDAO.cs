using PMRS_Mvc.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PMRS_Mvc.Areas.Security.DAO
{
    public class MHDAO
    {
        public List<SecMH> GetHeadMenuList()
        {
            using (SCEntities db = new SCEntities())
            {
                var mhList = db.SecMHs.Select(x => x).ToList();
                return mhList;
            }
        }

        public string InsertMh(SecMH secMh)
        {
            if (secMh != null)
            {
                using (SCEntities obj = new SCEntities())
                {
                    int mxId = obj.SecMHs.Max(x => x.ID);
                    secMh.ID = mxId + 1;

                    try
                    {
                        obj.SecMHs.Add(secMh);
                        obj.SaveChanges();
                        return "Menu Head added Successfully";
                    }
                    catch
                    {
                        return "Not Saved! Try Again";
                    }
                }
            }
            return "Not Saved! Try Again";
        }

        public string UpdateMh(SecMH secMh)
        {
            if (secMh != null)
            {
                try
                {
                    //var updt = _db.SecMHs.FirstOrDefault(x => x.ID == secMh.ID);

                    //updt.ID = secMh.ID;
                    //updt.CssClass = secMh.CssClass;
                    //updt.Nm = secMh.Nm;
                    //updt.Seq = secMh.Seq;

                    //_db.SaveChanges();

                    //_db.SecMHs.Attach(secMh);
                    //_db.Entry(secMh).State = EntityState.Modified;
                    //_db.SaveChanges();

                    using (SCEntities db = new SCEntities())
                    {
                        db.SecMHs.Attach(secMh);

                        var entry = db.Entry(secMh);
                        entry.State = EntityState.Modified;

                        entry.Property(e => e.CssClass).IsModified = true;
                        entry.Property(e => e.Nm).IsModified = true;
                        entry.Property(e => e.Seq).IsModified = true;

                        db.SaveChanges();

                        return "Menu Head Updated Successfully";
                    }
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