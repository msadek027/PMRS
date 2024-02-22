using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PMRS_Mvc.Areas.Security.DAO
{
    public class UserLoginDAO : ReturnData
    {
        private readonly AuditTrailLogger _adt = new AuditTrailLogger();
        private readonly CommonMailer _mailer = new CommonMailer();

        private static readonly string PasswordHash = "P@A5w0rD";
        private static readonly string SaltKey = "S@L7&K3y";
        private static readonly string VIKey = "@1B2c3D4e5F6g7H8";

        public string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }

        public string Decrypt(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }

        public object GetUserLoginList()
        {
            using (SCEntities db = new SCEntities())
            {
                var loginList = (from t in db.UserLogins
                                 join a in db.VW_Login on t.UserID equals a.UserID
                                 select new
                                 {
                                     t.UserLoginID,
                                     t.UserID,
                                     a.UserName,
                                     a.UserLoginName,
                                     t.Password,
                                     a.EmployeeCode,
                                     t.Status
                                 }).AsEnumerable().Select(n => new
                                 {
                                     n.UserLoginID,
                                     n.UserID,
                                     n.UserName,
                                     n.UserLoginName,
                                     n.EmployeeCode,
                                     n.Status,
                                     Password = Decrypt(n.Password),
                                 }).ToList();

                return loginList;
            }
        }

        public object GetRemainingEmployee(string empType)
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var emp = db.EmployeeInfoes.SqlQuery("SELECT * FROM EmployeeInfo AS ei WHERE ei.[Status] = 1 AND UserType = '" + empType + "' AND ei.UserID NOT IN(SELECT ul.UserID FROM SC.UserLogin AS ul)");

                var emList = (from em in emp
                              join ds in db.DesignationInfoes on em.DesignationID equals ds.DesignationID
                              join dpt in db.DepartmentInfoes on em.DepartmentID equals dpt.DepartmentID into empGroup
                              where em.Status == 1
                              from rt in empGroup.DefaultIfEmpty()
                              orderby em.UserName
                              select new
                              {
                                  UserID = (int?)em.UserID,
                                  em.EmployeeCode,
                                  em.UserName,
                                  em.DepartmentID,
                                  ds.DesignationName,
                                  em.DesignationID,
                                  rt.DepartmentName,
                                  em.Status,
                              }).ToList();
                return emList;
            }
        }

        public bool InsertUser(UserLogin uLogin)
        {
            IUMode = "I";

            if (uLogin != null)
            {
                using (SCEntities obj = new SCEntities())
                {
                    try
                    {
                        uLogin.Password = Encrypt(uLogin.Password);

                        obj.UserLogins.Add(uLogin);
                        obj.SaveChanges();
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

        public bool UpdateUser(UserLogin uLogin)
        {
            IUMode = "U";

            if (uLogin != null)
            {
                try
                {
                    using (SCEntities db = new SCEntities())
                    {
                        uLogin.Password = Encrypt(uLogin.Password);

                        db.UserLogins.Attach(uLogin);

                        var entry = db.Entry(uLogin);
                        entry.State = EntityState.Modified;

                        entry.Property(e => e.Password).IsModified = true;
                        entry.Property(e => e.Status).IsModified = true;

                        db.SaveChanges();

                        MaxCode = uLogin.UserLoginID.ToString();
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public bool CheckCurrentPassword(string currentPassword)
        {
            SCEntities db = new SCEntities();
            UserLoginDAO lgndao = new UserLoginDAO();
            bool status = false;
            string userName = HttpContext.Current.Session["Username"].ToString();

            if ((!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(currentPassword)))
            {
                string pass = lgndao.Encrypt(currentPassword);

                var lgn = (from t in db.VW_Login
                           where t.UserLoginName == userName && t.Password == pass && t.Status == 1
                           select t).FirstOrDefault();

                if (lgn != null)
                {
                    status = true;
                }
            }
            return status;
        }

        public bool UpdatePassword(string password)
        {
            IUMode = "U";
            if (!string.IsNullOrEmpty(password))
            {
                try
                {
                    UserLogin uLogin = new UserLogin
                    {
                        UserID = Convert.ToInt32(HttpContext.Current.Session["empID"].ToString()),
                        Password = Encrypt(password)
                    };

                    using (SCEntities db = new SCEntities())
                    {
                        var ul = (from u in db.UserLogins
                                  where u.UserID == uLogin.UserID
                                  select new
                                  {
                                      u.UserLoginID
                                  }).FirstOrDefault();

                        uLogin.UserLoginID = ul.UserLoginID;
                        db.UserLogins.Attach(uLogin);

                        var entry = db.Entry(uLogin);
                        entry.State = EntityState.Modified;

                        entry.Property(e => e.Password).IsModified = true;
                        entry.Property(e => e.Status).IsModified = false;
                        entry.Property(e => e.UserID).IsModified = false;
                        entry.Property(e => e.UserLoginName).IsModified = false;
                        db.SaveChanges();

                        //NotifyMail();

                        _adt.InsertAudit("frmChangePass", "UserLogin", "ChangePass", "", Convert.ToInt32(HttpContext.Current.Session["empID"].ToString()));

                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }


        //private void NotifyMail()
        //{
        //    string subject = "Password Update Notification";
        //    string body = "Dear Sir," +
        //                  Environment.NewLine +
        //                  Environment.NewLine +
        //                  "This is to inform you that, your password has been updated recently. If it is not you, please contact to system administrator immediately." +
        //                  Environment.NewLine +
        //                  Environment.NewLine +
        //                  "Regards," +
        //                  Environment.NewLine +
        //                  Environment.NewLine +
        //                  "PDMS Team" +
        //                  Environment.NewLine +
        //                  Environment.NewLine +
        //                  "Note: This is an automated e-mail. Do not reply.";

        //    using (PDMSEntities db = new PDMSEntities())
        //    {
        //        int empId = Convert.ToInt32(HttpContext.Current.Session["empID"].ToString());

        //        var emp = (from t in db.EmployeeInfoes
        //                   where t.EmployeeID == empId
        //                   select t).FirstOrDefault();

        //        if (emp != null)
        //        {
        //            string email = emp.Email;
        //            _mailer.SendMail(email, subject, body);
        //        }
        //    }
        //}
    }
}