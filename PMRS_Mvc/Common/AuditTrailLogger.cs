using PMRS_Mvc.Models;
using System;
using System.Linq;
using System.Web;

namespace PMRS_Mvc.Common
{
    public class AuditTrailLogger
    {
        private readonly CommonMailer _cmn = new CommonMailer();

        public void InsertAudit(string formName, string tableName, string actType, string prjNm, int trsId)
        {
            //bool status = false;

            //using (SCEntities db = new SCEntities())
            //{
            //    try
            //    {
            //        if (actType == "I")
            //        {
            //            var mailer = (from t in db.MLConfs
            //                          join u in db.VW_Login on t.Emp_ID equals u.EmployeeID
            //                          join mal in db.MLMngs on t.ML_ID equals mal.ID
            //                          join sm in db.SecSMs on t.SM_ID equals sm.ID
            //                          where t.SM_Url == formName && t.Act_Type == actType
            //                          select new
            //                          {
            //                              u.Email,
            //                              mal.Mail_Sub,
            //                              mal.Mail_Body,
            //                              t.Notify_For,
            //                              mal.ID,
            //                              sm.Nm,
            //                              t.Emp_ID
            //                          }).ToList();

            //            if (mailer.Count > 0)
            //            {
            //                foreach (var m in mailer)
            //                {
            //                    if (m.Notify_For == "Common")
            //                    {
            //                        using (PMRS_BcEntities obj = new PMRS_BcEntities())
            //                        {
            //                            var chk = (from t in obj.VW_RPT_NewProductProposal
            //                                       where t.ProposalNo == prjNm
            //                                       select t).FirstOrDefault();
            //                            //var proposal = from t in 

            //                            status = _cmn.SendMail(m.Email, m.Mail_Sub,
            //                                   m.Mail_Body + Environment.NewLine
            //                                   + Environment.NewLine + Environment.NewLine + "Proposal No: " + prjNm
            //                                   + Environment.NewLine + Environment.NewLine + "Generic & Strength: " + chk.GenericStrength
            //                                   + Environment.NewLine + Environment.NewLine + "Dosage Form: " + chk.DosageName
            //                                   + Environment.NewLine + Environment.NewLine + "Product Nature: " + chk.ProductNatureName
            //                                   + Environment.NewLine + Environment.NewLine + "Concern PMD Personnel: " + chk.EmployeeName);

            //                            _cmn.MailLogger(prjNm, "Proposal", m.Email, formName, status.ToString(), "", "", mailer.FirstOrDefault().ID);
            //                        }
            //                    }

            //                    if (m.Notify_For == "Proposal")
            //                    {
            //                        using (PMRS_BcEntities obj = new PMRS_BcEntities())
            //                        {
            //                            var prml = (from t in obj.EmpProposalMappings
            //                                        where t.ProposalNo == prjNm && t.EmployeeID == m.Emp_ID
            //                                        select t).FirstOrDefault();

            //                            var chk = (from t in obj.VW_RPT_NewProductProposal
            //                                       where t.ProposalNo == prjNm
            //                                       select t).FirstOrDefault();

            //                            if (prml != null)
            //                            {

            //                                status = _cmn.SendMail(m.Email, m.Mail_Sub,
            //                                    m.Mail_Body + Environment.NewLine
            //                                                + Environment.NewLine + Environment.NewLine +
            //                                                "Proposal No: " + prjNm
            //                                                + Environment.NewLine + Environment.NewLine +
            //                                                "Generic & Strength: " + chk.GenericStrength
            //                                                + Environment.NewLine + Environment.NewLine +
            //                                                "Dosage Form: " + chk.DosageName
            //                                                + Environment.NewLine + Environment.NewLine +
            //                                                "Product Nature: " + chk.ProductNatureName
            //                                                + Environment.NewLine + Environment.NewLine +
            //                                                "Concern PMD Personnel: " + chk.EmployeeName);
            //                                _cmn.MailLogger(prjNm, "Proposal", m.Email, formName, status.ToString(), "", "", mailer.FirstOrDefault().ID);
            //                            }
            //                        }
            //                    }

            //                    if (m.Notify_For == "Project")
            //                    {
            //                        using (PMRS_BcEntities obj = new PMRS_BcEntities())
            //                        {
            //                            var chk = (from t in obj.EmpProjectMappings
            //                                       where t.ProjectNo == prjNm && t.EmployeeID == m.Emp_ID
            //                                       select t).FirstOrDefault();

            //                            var prjdtl = (from t in obj.VW_ProductInfo
            //                                          where t.ProjectNo == prjNm
            //                                          select t).FirstOrDefault();

            //                            if (chk != null)
            //                            {
            //                                status = _cmn.SendMail(m.Email, m.Mail_Sub,
            //                                    m.Mail_Body + Environment.NewLine
            //                                    + Environment.NewLine + Environment.NewLine + "Project No: " + prjNm
            //                                    + Environment.NewLine + Environment.NewLine + "Generic & Strength: " + prjdtl.GenericStrength
            //                                    + Environment.NewLine + Environment.NewLine + "Dosage From: " + prjdtl.DosageName
            //                                    + Environment.NewLine + Environment.NewLine + "Concern PMD Personnel: " + prjdtl.EmployeeName);

            //                                _cmn.MailLogger(prjNm, "Project", m.Email, formName, status.ToString(), "", "", mailer.FirstOrDefault().ID);
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }

            //        string empId = HttpContext.Current.Session["empID"].ToString();

            //        Audit_Trail audt = new Audit_Trail
            //        {
            //            Action_By = Convert.ToInt32(empId),
            //            Action_Date = DateTime.Now,
            //            Action_Form = formName,
            //            Action_Table = tableName,
            //            Activity_Type = actType,
            //            Project_Name = prjNm,
            //            Terminal = GetLanIPAddress(),
            //            Transaction_ID = trsId
            //        };

            //        db.Audit_Trail.Add(audt);
            //        db.SaveChanges();
            //    }
            //    catch (Exception e)
            //    {
            //        string s = e.Message.ToString();
            //    }
            //}
        }

        public String GetLanIPAddress()
        {
            String ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (ip == "::1")
            {
                ip = "127.0.0.1";
            }
            return ip;
        }
    }
}
