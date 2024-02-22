using PMRS_Mvc.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMRS_Mvc.Common
{
    public class FileChecker
    {
        [HttpPost]
        public bool UploadManager(HttpPostedFileBase document, string filePath, string fileName)
        {
            try
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                filePath = filePath + fileName;
                //filePath = filePath + Path.GetFileName(document.FileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                document.SaveAs(filePath);
     
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteDoc(string filePath, string PrNo, string ID)
        {
            bool isTrue = false;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                isTrue = true;
            }

            if (ID != null)
            {
                using (PMRS_BcEntities obj = new PMRS_BcEntities())
                {
                    try
                    {
                        int intId = Convert.ToInt32(ID);
                        var x = (from y in obj.DocUpInfoes
                                 where y.PrNo == PrNo && y.ID == intId
                                 select y).FirstOrDefault();

                        if (x != null)
                        {
                            //_adt.InsertAudit("frmNewProductProposal", "ProposalMst", IUMode, x.ProposalNo,x.ProposalMarBdDtlID);
                            obj.DocUpInfoes.Remove(x);
                            obj.SaveChanges();
                            isTrue = true;
                        }
                    }
                    catch (Exception)
                    {
                        isTrue = false;
                    }
                }
            }

            return isTrue;
        }

        public bool InsertOrUpdateDoc(List<DocUpInfo> docUpInfo, string proposalNo, string formName, string trackNo, string randomTrackNo)
        {
            try
            {
                using (PMRS_BcEntities obj = new PMRS_BcEntities())
                {
                    var x = (from y in obj.DocUpInfoes
                             where y.PrNo == proposalNo && y.FormName == formName && y.TrackNo == randomTrackNo
                             select y).ToList();
                    if (x.Count > 0)
                    {
                        obj.DocUpInfoes.RemoveRange(x);
                        obj.SaveChanges();
                    }
                    if (docUpInfo != null)
                    {
                        foreach (var documents in docUpInfo)
                        {
                            documents.PrNo = proposalNo;
                            documents.FormName = formName;
                            documents.TrackNo = trackNo;
                            obj.DocUpInfoes.Add(documents);
                            obj.SaveChanges();
                        }
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public object GetDocInfo(string prNo, string formName, string trackNo)
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var x = (from y in db.DocUpInfoes
                         where y.PrNo == prNo && y.FormName == formName && y.TrackNo == trackNo
                         select y).ToList();
                return x;
            }
        }

        public object GetDocInfo(string prNo, string formNamePrv, string formName, string trackNo)
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                var x = (from y in db.DocUpInfoes
                         where y.PrNo == prNo && (y.FormName == formNamePrv || y.FormName == formName) && y.TrackNo == trackNo
                         select y).ToList();
                return x;
            }
        }
    }
}