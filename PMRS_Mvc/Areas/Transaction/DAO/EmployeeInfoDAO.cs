using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
//using System.String;

namespace PMRS_Mvc.Areas.Transaction.DAO
{
    public class EmployeeInfoDAO : ReturnData
    {
        private readonly AuditTrailLogger _adt = new AuditTrailLogger();

        public object GetEmployeeInfoList()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var emList = (from em in db.EmployeeInfoes
                              join ds in db.DesignationInfoes on em.DesignationID equals ds.DesignationID into empDesigGroup
                              from rs in empDesigGroup.DefaultIfEmpty()
                              join dpt in db.DepartmentInfoes on em.DepartmentID equals dpt.DepartmentID into empGroup
                              from rt in empGroup.DefaultIfEmpty()
                              orderby em.UserName
                              select new
                              {
                                  UserID = (int?)em.UserID,
                                  em.EmployeeCode,
                                  em.UserName,
                                  em.BanglaName,
                                  em.Address,
                                  em.FatherName,
                                  em.NationalID,
                                  em.UserType,
                                  em.PhotoURL,
                                  em.DesignationID,
                                  rs.DesignationName,
                                  em.DepartmentID,
                                  rt.DepartmentName,
                                  em.PhoneNumber,
                                  em.Email,
                                  em.Status
                              }).ToList();

                return emList;
            }
        }
        public object GetActiveEmployeeInfoList()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var emList = (from em in db.EmployeeInfoes
                              join ds in db.DesignationInfoes on em.DesignationID equals ds.DesignationID into empDesigGroup
                              from rs in empDesigGroup.DefaultIfEmpty()
                              join dpt in db.DepartmentInfoes on em.DepartmentID equals dpt.DepartmentID into empGroup
                              from rt in empGroup.DefaultIfEmpty()
                              where em.Status == 1 && em.UserType == "EMP"
                              orderby em.UserName
                              select new
                              {
                                  UserID = (int?)em.UserID,
                                  em.EmployeeCode,
                                  em.UserName,
                                  em.BanglaName,
                                  em.Address,
                                  em.FatherName,
                                  em.NationalID,
                                  em.UserType,
                                  em.PhotoURL,
                                  em.DesignationID,
                                  rs.DesignationName,
                                  em.DepartmentID,
                                  rt.DepartmentName,
                                  em.PhoneNumber,
                                  em.Email,
                                  em.Status
                              }).ToList();

                return emList;
            }
        }

        public List<EmployeeInfo> GetActiveEmployeeList()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var emList = (from em in db.EmployeeInfoes
                              where em.Status == 1
                              orderby em.UserName select em).ToList();

                return emList;
            }
        }
        public object GetActiveMPListByParliament(int parliamentSessionID)
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var plno = (from xx in db.ParliamentSessionInfoes where xx.ParliamentSessionID == parliamentSessionID select xx.ParliamentNo).FirstOrDefault();

                string parlno = plno.ToString();

                var emList = (from t in db.VW_MPInfo
                             where t.Status == 1 && t.UserType == "MP"
                             && t.ParliamentNo == parlno
                              orderby t.UserName
                             select new
                             {
                                 UserID = (int?)t.UserID,
                                 t.EmployeeCode,
                                 t.UserName,
                                 t.BanglaName,
                                 t.Address,
                                 t.FatherName,
                                 t.NationalID,
                                 t.UserType,
                                 t.PhotoURL,
                                 t.DesignationID,
                                 t.DesignationName,
                                 t.DepartmentID,
                                 t.DepartmentName,
                                 t.PhoneNumber,
                                 t.Email,
                                 t.Status,
                                 t.ConstitutentBangla,
                                 t.ConstitutentNumber,
                             }).OrderBy(x=>x.EmployeeCode).ToList();

                return emList;
            }
        }


        public object GetIndividualMPInfo()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                int UserID = Convert.ToInt32(HttpContext.Current.Session["empID"]);

                var emList = (from t in db.VW_MPInfo
                              where t.Status == 1 && t.UserType == "MP"
                              && t.UserID == UserID
                              orderby t.UserName
                              select new
                              {
                                  UserID = (int?)t.UserID,
                                  t.EmployeeCode,
                                  t.UserName,
                                  t.BanglaName,
                                  t.Address,
                                  t.FatherName,
                                  t.NationalID,
                                  t.UserType,
                                  t.PhotoURL,
                                  t.DesignationID,
                                  t.DesignationName,
                                  t.DepartmentID,
                                  t.DepartmentName,
                                  t.PhoneNumber,
                                  t.Email,
                                  t.Status,
                                  t.ConstitutentBangla,
                                  t.ConstitutentNumber,
                              }).ToList();

                return emList;
            }
        }

        public EmployeeInfo GetIndividualMPInfoByEmployeeCode(string employeeCode)
        {
            try
            {
                using (PMRS_BcEntities db = new PMRS_BcEntities())
                {
                    var emp = db.EmployeeInfoes.Where(f => f.EmployeeCode == employeeCode).Select(x => x).ToList().FirstOrDefault();

                    return emp;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }
        public object GetActiveMPList()
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {

                var emList = (from t in db.EmployeeInfoes
                              where t.Status == 1 && t.UserType == "MP"
                              orderby t.UserName
                              select new
                              {
                                  UserID = (int?)t.UserID,
                                  t.EmployeeCode,
                                  t.UserName,
                                  t.BanglaName,
                                  t.Address,
                                  t.FatherName,
                                  t.NationalID,
                                  t.UserType,
                                  t.PhotoURL,                       
                                  t.PhoneNumber,
                                  t.Email,
                                  t.Status,
                              }).ToList();

                return emList;
            }
        }

        public object GetEmployeeByRoleList(int RoleID)
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var emList = (from em in db.EmployeeInfoes
                              join ds in db.DesignationInfoes on em.DesignationID equals ds.DesignationID
                              join dpt in db.DepartmentInfoes on em.DepartmentID equals dpt.DepartmentID into empGroup
                              from rl in db.RLConfs
                              where em.Status == 1 && rl.Emp_ID == em.UserID && rl.RL_ID == RoleID
                              from rt in empGroup.DefaultIfEmpty()
                              orderby em.UserName
                              select new
                              {
                                  EmployeeID = (int?)em.UserID,
                                  em.EmployeeCode,
                                  em.BanglaName,
                                  em.UserName,
                                  em.DesignationID,
                                  ds.DesignationName,
                                  em.DepartmentID,
                                  rt.DepartmentName,
                                  em.Status,
                              }).ToList();
                return emList;
            }
        }


        public object GetEmployeeByDept(string dptCode)
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                //var emList = db.VW_EmpInfo.Where(x => x.DepartmentCode == dptCode).GroupBy(x => x.EmployeeCode).Select(x => x.FirstOrDefault()).ToList();

                //return emList;
                return 0;
            }
        }

        public bool InsertEmployeeInfo(EmployeeInfo master)
        {
            IUMode = "I";

            if (master != null)
            {
                using (PMRS_BcEntities obj = new PMRS_BcEntities())
                {
                    try
                    {
                        obj.EmployeeInfoes.Add(master);
                        obj.SaveChanges();

                        MaxCode = master.EmployeeCode;
                        MaxID = master.UserID.ToString();

                        _adt.InsertAudit("frmEmployeeInfo", "EmployeeInfo", IUMode, "", master.UserID);
                        return true;
                    }
                    catch (Exception ex)
                    {
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

        public bool UpdateEmployeeInfo(EmployeeInfo master)
        {
            IUMode = "U";

            if (master != null)
            {
                try
                {
                    using (PMRS_BcEntities db = new PMRS_BcEntities())
                    {
                        db.EmployeeInfoes.Attach(master);

                        var entry = db.Entry(master);
                        entry.State = EntityState.Modified;

                        entry.Property(e => e.UserName).IsModified = true;
                        entry.Property(e => e.BanglaName).IsModified = true;
                        entry.Property(e => e.Status).IsModified = true;
                        entry.Property(e => e.DepartmentID).IsModified = true;
                        entry.Property(e => e.DesignationID).IsModified = true;
                        entry.Property(e => e.Email).IsModified = true;
                        entry.Property(e => e.PhoneNumber).IsModified = true;

                        db.SaveChanges();

                        MaxCode = master.EmployeeCode;
                        MaxID = master.UserID.ToString();

                        _adt.InsertAudit("frmEmployeeInfo", "EmployeeInfo", IUMode, "", master.UserID);
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.InnerException.Message.Contains("UNIQUE"))
                    {
                        IUMode = "Unique";
                        return true;
                    }
                }
            }
            return false;
        }

        public object IsLogInInfoExist(string employeeId)
        {
            using (SCEntities db = new SCEntities())
            {
                int intEmployeeId = Convert.ToInt32(employeeId);
                var emList = (from u in db.UserLogins
                              where u.UserID == intEmployeeId
                              select new
                              {
                                  u.UserLoginID
                              }).ToList();

                return emList;
            }
        }
    }
}