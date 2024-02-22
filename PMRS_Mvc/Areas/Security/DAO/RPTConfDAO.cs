using PMRS_Mvc.Models;
using System;
using System.Linq;
using System.Web;

namespace PMRS_Mvc.Areas.Security.DAO
{
    public class RPTConfDAO
    {
        public object GetReportFormList()
        {
            using (SCEntities db = new SCEntities())
            {
                var list = (from t in db.RPTMngs
                            select new
                            {
                                t.FormName
                            }).Distinct().ToList();

                return list;
            }
        }

        public object GetReportsByForm(string frmName)
        {
            using (SCEntities db = new SCEntities())
            {
                var list = (from t in db.RPTMngs
                            where t.FormName == frmName
                            select new
                            {
                                t.ID,
                                t.ReportName
                            }).Distinct().ToList();

                return list;
            }
        }

        public object GetRoleWiseReports(string frmName, int rlId)
        {
            using (SCEntities db = new SCEntities())
            {
                var list = (from t in db.RPTConfs
                            from u in db.RPTMngs
                            from x in db.RLs
                            where t.FormName == frmName && t.RL_ID == rlId
                            && t.Rpt_ID == u.ID && t.RL_ID == x.ID
                            select new
                            {
                                t.ID,
                                t.FormName,
                                t.RL_ID,
                                t.Rpt_ID,
                                u.ReportName,
                                x.Nm
                            }).ToList();

                return list;
            }
        }

        public object GetReportByFormRole(string frmName)
        {
            using (SCEntities db = new SCEntities())
            {
                int empId = Convert.ToInt32(HttpContext.Current.Session["empID"].ToString());

                var list = (from t in db.RPTConfs
                            from u in db.RPTMngs
                            from e in db.RLConfs
                            from x in db.RLs
                            where t.FormName == frmName && e.Emp_ID == empId
                                  && t.Rpt_ID == u.ID && t.RL_ID == x.ID
                                  && t.RL_ID == e.RL_ID
                            select new
                            {
                                t.ID,
                                t.FormName,
                                t.RL_ID,
                                t.Rpt_ID,
                                u.ReportName,
                                u.RptCode,
                                x.Nm
                            }).ToList();

                return list;
            }
        }

        public string InsertRptConf(RPTConf master)
        {
            if (master != null)
            {
                using (SCEntities obj = new SCEntities())
                {
                    var dbchk = obj.RPTConfs.FirstOrDefault(x => x.RL_ID == master.RL_ID && x.FormName == master.FormName && x.Rpt_ID == master.Rpt_ID);

                    if (dbchk != null) return "Data Exists";

                    int mxId = obj.RPTConfs.DefaultIfEmpty().Max(x => x.ID);
                    master.ID = mxId + 1;

                    try
                    {
                        obj.RPTConfs.Add(master);
                        obj.SaveChanges();
                        return "Access Permission Added Successfully";
                    }
                    catch
                    {
                        return "Not Saved! Try Again";
                    }
                }
            }
            return "Not Saved! Try Again";
        }

        public string ReportDel(int id)
        {
            using (SCEntities db = new SCEntities())
            {
                var list = (from t in db.RPTConfs
                            where t.ID == id
                            select t).FirstOrDefault();

                if (list != null)
                {
                    db.RPTConfs.Remove(list);
                    db.SaveChanges();

                    return "Access Permission Updated Successfully";
                }
            }
            return "Error Occured! Try Again";
        }
    }
}