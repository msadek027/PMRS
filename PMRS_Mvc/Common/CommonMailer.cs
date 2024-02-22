using PMRS_Mvc.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace PMRS_Mvc.Common
{
    public class CommonMailer
    {
        //[Obsolete("Do not use this in Production code!!!", true)]
        static void Disable_CertificateValidation()
        {
            // Disabling certificate validation can expose you to a man-in-the-middle attack
            // which may allow your encrypted message to be read by an attacker
            // https://stackoverflow.com/a/14907718/740639
            ServicePointManager.ServerCertificateValidationCallback =
                delegate (
                    object s,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors
                ) {
                    return true;
                };
        }

        public bool SendMail(string mailTo, string subject, string body)
        {
            bool sts = false;

            //SmtpClient smtpServer = new SmtpClient("172.16.128.39")
            SmtpClient smtpServer = new SmtpClient("172.16.128.39")
            {
                Port = 25,
                //Port = 587,
                Credentials = new NetworkCredential("notify.pdms@squaregroup.com", "SquarePharma121"),
                EnableSsl = true
            };

            SmtpClient smtpServer2 = new SmtpClient("172.16.128.40")
            {
                Port = 25,
                //Port = 587,
                Credentials = new NetworkCredential("notify.pdms@squaregroup.com", "SquarePharma121"),
                EnableSsl = true
            };

            SmtpClient smtpServer3 = new SmtpClient("172.16.128.41")
            {
                Port = 25,
                //Port = 587,
                Credentials = new NetworkCredential("notify.pdms@squaregroup.com", "SquarePharma121"),
                EnableSsl = true
            };


            char[] splitter = { ';' };
            var mailList = mailTo.Split(splitter);

            for (int i = 0; i <= mailList.Length - 1; i++)
            {
                Disable_CertificateValidation();
                try
                {
                    smtpServer.Send("notify.pdms@squaregroup.com", mailList[i], subject, body);
                    sts = true;
                }
                catch (SmtpException se)
                {
                    smtpServer2.Send("notify.pdms@squaregroup.com", mailList[i], subject, body);
                    sts = true;
                }
                catch
                {
                    smtpServer3.Send("notify.pdms@squaregroup.com", mailList[i], subject, body);
                    sts = true;
                }
            }

            return sts;
        }

        public void MailLogger(string proNo, string trigType, string address, string formName, string sts, string smtp, string error, int mlID)
        {
            using (SCEntities db = new SCEntities())
            {

                MailTrack track = new MailTrack
                {
                    ProNo = proNo,
                    TriggerType = trigType,
                    MailTo = address,
                    FormName = formName,
                    MailTime = DateTime.Now,
                    MailStatus = sts,
                    MailTypeID = mlID,
                    SMTPServer = smtp,
                    SpecialMsg = error
                };
                db.MailTracks.Add(track);
                db.SaveChanges();
            }
        }
    }
}