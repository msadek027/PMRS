using Newtonsoft.Json;
using PMRS_Mvc.Areas.Security.DAO;
using PMRS_Mvc.Areas.Transaction.DAO;
using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace PMRS_Mvc.Areas.Transaction.Controllers
{
    [LoginChecker]
    public class EmployeeInfoController : Controller
    {
        private EmployeeInfoDAO primaryDAO = new EmployeeInfoDAO();
        private ConstitutentInfoDAO constitutentDAO = new ConstitutentInfoDAO();
        private ConstitutentMappingDAO constitutentMapping = new ConstitutentMappingDAO();
        private ROLConfDAO rlConfDAO = new ROLConfDAO();
        private DesignationInfoDAO degDAO = new DesignationInfoDAO();
        public ActionResult frmEmployeeInfo()
        {
            return View();
        }

        public async Task<List<MP.Models.MPInfoModel.Payload>> GetAllMpInfo()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://prp.parliament.gov.bd/api/secure/external?action=all_mp_infos");
            request.Headers.Add("Authorization", Session["authToken"].ToString());
            var response = await client.SendAsync(request);
            string responseContent = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            PMRS_Mvc.Areas.MP.Models.MPInfoModel.MpRoot loginmodel = JsonConvert.DeserializeObject<PMRS_Mvc.Areas.MP.Models.MPInfoModel.MpRoot>(responseContent);
            List<MP.Models.MPInfoModel.Payload> missingList = new List<MP.Models.MPInfoModel.Payload>();
            List<PMRS_Mvc.Areas.MP.Models.MPInfoModel.ElectionDetailModel> missingCi = new List<PMRS_Mvc.Areas.MP.Models.MPInfoModel.ElectionDetailModel>();
            ConstitutentInfo ci = new ConstitutentInfo();

            try
            {
                DeleteImages("E:/Office Projects/PRMS/PMRS_Mvc/Images/Mp");
         
                foreach (var emp in loginmodel.payload)
                {
                  
                    #region Images

                    byte[] byteArray = new byte[] { /* your byte values here */ };
                    //byteArray = IntListToByteArray(accessModel.payload.photo);
                    // Convert byte array to image
                    string url = string.Format("E:/Office Projects/PRMS/PMRS_Mvc/Images/Mp/{0}.jpeg", emp.nameEng);
                    string dbUrl = string.Format("/Images/Mp/{0}.jpeg", emp.nameEng);
                    Image image = ByteArrayToImage((byte[])(Array)(emp.photo), url);
                    #endregion
                    #region Insert ConstitutentInfo
                    ci = constitutentDAO.GetActiveConstitutentInfoByName(emp.electionDetailModels[0].constituencyNameEn);
                    if (ci == null)
                    {
                        ConstitutentInfo newCi = new ConstitutentInfo();
                        newCi.ConstitutentID = emp.electionDetailModels[0].constituencyId;
                        newCi.ConstitutentArea = emp.electionDetailModels[0].constituencyNameEn;
                        newCi.ConstitutentBangla = emp.electionDetailModels[0].constituencyNameBn;
                        newCi.ConstitutentNumber = emp.electionDetailModels[0].constituencyId.ToString();
                        if (emp.electionDetailModels[0].status == "Active")
                        {
                            newCi.Status = 1;
                        }
                        else
                        {
                            newCi.Status = 0;
                        }

                        constitutentDAO.InsertConstitutent(newCi);
                        ci = newCi;
                    }
                    else
                    {

                        ci.ConstitutentArea = emp.electionDetailModels[0].constituencyNameEn;
                        ci.ConstitutentBangla = emp.electionDetailModels[0].constituencyNameBn;
                        ci.ConstitutentNumber = emp.electionDetailModels[0].constituencyId.ToString();
                        if (emp.electionDetailModels[0].status == "Active")
                        {
                            ci.Status = 1;
                        }
                        else
                        {
                            ci.Status = 0;
                        }
                        constitutentDAO.UpdateConstitutent(ci);
                    }
                    #endregion
                    #region Update EmployeeInfo
                    EmployeeInfo em = primaryDAO.GetIndividualMPInfoByEmployeeCode(emp.username);

                    if (em == null)
                    {
                        EmployeeInfo missingMP = new EmployeeInfo();
                        missingMP.EmployeeCode = emp.username;
                        missingMP.UserName = emp.nameEng;
                        missingMP.BanglaName = emp.nameBng;
                        missingMP.FatherName = emp.fatherNameEng;
                        missingMP.Address = emp.presentAddressEng;
                        missingMP.PhoneNumber = emp.mobileNumber;
                        missingMP.UserType = "MP";
                        missingMP.DesignationID = 1;
                        missingMP.DepartmentID = 1;
                        missingMP.Email = emp.officialEmail;
                        missingMP.NationalID = emp.nid;
                        missingMP.PhotoURL = dbUrl;
                        if (emp.electionDetailModels[0].status == "Active")
                        {
                            missingMP.Status = 1;
                        }
                        else
                        {
                            missingMP.Status = 0;
                        }
                        primaryDAO.InsertEmployeeInfo(missingMP);
                        em = missingMP;
                    }
                    else
                    {

                        em.EmployeeCode = emp.username;
                        em.UserName = emp.nameEng;
                        em.BanglaName = emp.nameBng;
                        em.FatherName = emp.fatherNameEng;
                        em.Address = emp.presentAddressEng;
                        em.PhoneNumber = emp.mobileNumber;
                        em.UserType = "MP";
                        em.DesignationID = 1;
                        em.DepartmentID = 1;
                        em.Email = emp.officialEmail;
                        em.NationalID = emp.nid;
                        em.PhotoURL = dbUrl;
                        if (emp.electionDetailModels[0].status == "Active")
                        {
                            em.Status = 1;
                        }
                        else
                        {
                            em.Status = 0;
                        }
                        primaryDAO.UpdateEmployeeInfo(em);
                    }
                    #endregion

                    #region Insert Constitutent and User Mapping

                    ConstitutentUserMappingInfo consMap = constitutentMapping.GetConstitutentMappingByUserId(em.UserID);
                    if (consMap == null)
                    {
                        ConstitutentUserMappingInfo newConsMap = new ConstitutentUserMappingInfo();
                        newConsMap.ConstitutentID = ci.ConstitutentID;
                        newConsMap.UserID = em.UserID;
                        newConsMap.ParliamentNo = emp.electionDetailModels[0].parliamentNo.ToString();
                        constitutentMapping.InsertConstitutentMapping(newConsMap);
                    }
                    else
                    {

                        consMap.ConstitutentID = ci.ConstitutentID;
                        consMap.UserID = em.UserID;
                        consMap.ParliamentNo = emp.electionDetailModels[0].parliamentNo.ToString();
                        constitutentMapping.UpdateConstitutentMapping(consMap);
                    }

                    #endregion

                    #region Insert Role Configure
                    RLConf rc = rlConfDAO.GetRoleConfByEmployeeId(em.UserID);
                    if (rc == null)
                    {
                        rc = new RLConf();
                        rc.RL_ID = 5;
                        rc.Emp_ID = em.UserID;
                        rlConfDAO.SaveRLConf(rc);
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {

            }
           

            return missingList;
        }
        public async Task<bool> GetAllSignatures()
        {
            var client = new HttpClient();
            try
            {
                DeleteImages("E:/Office Projects/PRMS/PMRS_Mvc/Images/Signature");

                #region Images
        
                    List<EmployeeInfo> employeeInfos =  primaryDAO.GetActiveEmployeeList();
                    foreach(var item in employeeInfos)
                    {
                        string endPoint = string.Format("https://prp.parliament.gov.bd/api/secure/external?action=image&username={0}&type=2", item.EmployeeCode);
                        var request = new HttpRequestMessage(HttpMethod.Get, endPoint);
                        request.Headers.Add("Authorization", Session["authToken"].ToString());
                        var response = await client.SendAsync(request);
                        string responseContent = await response.Content.ReadAsStringAsync();
                        response.EnsureSuccessStatusCode();
                        PMRS_Mvc.Areas.MP.Models.SignatureModel.SignatureRoot signatureModel = JsonConvert.DeserializeObject<PMRS_Mvc.Areas.MP.Models.SignatureModel.SignatureRoot>(responseContent);
                       if(signatureModel != null && signatureModel.msg == "Success")
                        {
                            byte[] byteArray = new byte[] { /* your byte values here */ };
                            //byteArray = IntListToByteArray(accessModel.payload.photo);
                            // Convert byte array to image
                            string url = string.Format("E:/Office Projects/PRMS/PMRS_Mvc/Images/Signature/{0}.jpeg", item.UserName);
                            string dbUrl = string.Format("/Images/Signature/{0}.jpeg", item.UserName);
                            Image image = ByteArrayToImage((byte[])(Array)(signatureModel.payload.sign), url);
                            item.Signature = dbUrl;
                            primaryDAO.UpdateEmployeeInfo(item);
                        }
                        
                    }
                    #endregion
                
            }
            catch (Exception ex)
            {

            }
            return true;
        }
        static void DeleteImages(string folderPath)
        {
            try
            {
                // Check if the folder exists
                if (Directory.Exists(folderPath))
                {
                    // Get all files in the folder
                    string[] files = Directory.GetFiles(folderPath);

                    // Delete each file
                    foreach (string filePath in files)
                    {
                        System.IO.File.Delete(filePath);
                       // Console.WriteLine($"Deleted: {filePath}");
                    }

               
                }
                else
                {
                 
                }
            }
            catch (Exception ex)
            {
            
            }

        
        }
        static Image ByteArrayToImage(byte[] byteArray,string url)
        {
            try
            {
                if (byteArray == null || byteArray.Length == 0)
                    return null;

                using (MemoryStream stream = new MemoryStream(byteArray))
                {
                    // You may need to replace Image.FromStream with Image.FromStreamIgnorePixelFormat
                    // if you encounter issues with pixel format.
                    // Image.FromStreamIgnorePixelFormat is available in .NET Core and later versions.
                    // If you are using .NET Framework, you may not need to worry about this.
                    stream.Position = 0;
                    var formate = ImageFormat.Png;
                    var img = Image.FromStream(stream);
                    using (Image image = Image.FromStream(stream))
                    {
                        image.Save(url, ImageFormat.Jpeg);
                    }

                    return img;
                }
            }
            catch (Exception ex)
            {

            }
            return null;

        }
        public async Task<List<PMRS_Mvc.Areas.MP.Models.EmpInfoModel.EmpPayload>> GetAllEmpInfo()
        {
            var client = new HttpClient();
            DeleteImages("E:/Office Projects/PRMS/PMRS_Mvc/Images/Emp");
            try
            {
                for (int i = 1; i <= 15; i++)
                {
                    string url = string.Format("https://prp.parliament.gov.bd/api/secure/external?action=employee_details&departmentId={0}&classes=1,2", i);
                    var request = new HttpRequestMessage(HttpMethod.Get, url);
                    request.Headers.Add("Authorization", Session["authToken"].ToString());
                    var response = await client.SendAsync(request);
                    string responseContent = await response.Content.ReadAsStringAsync();
                    response.EnsureSuccessStatusCode();
                    PMRS_Mvc.Areas.MP.Models.EmpInfoModel.EmpRoot empModel = JsonConvert.DeserializeObject<PMRS_Mvc.Areas.MP.Models.EmpInfoModel.EmpRoot>(responseContent);
                    DesignationInfo degInfo = new DesignationInfo();
                    foreach (var emp in empModel.payload)
                    {
                       
                        #region Designation
                        int degId = 0;
                        degInfo = degDAO.GetDesignationInfoByName(emp.designationEng);
                        if (degInfo == null)
                        {
                            degInfo = new DesignationInfo();
                            degInfo.DesignationName = emp.designationEng;
                            degInfo.DesignationNameBN = emp.designationBng;
                            degInfo.Grade = emp.grade.ToString();
                            degId = degDAO.InsertDesignation(degInfo);

                        }
                        else
                        {
                            degId = degInfo.DesignationID;
                        }
                        #endregion


                        #region Images

                        byte[] byteArray = new byte[] { /* your byte values here */ };
                        //byteArray = IntListToByteArray(accessModel.payload.photo);
                        // Convert byte array to image
                        string imgurl = string.Format("E:/Office Projects/PRMS/PMRS_Mvc/Images/Emp/{0}.jpeg", emp.nameEng);
                        string dbUrl = string.Format("/Images/Emp/{0}.jpeg", emp.nameEng);
                        Image image = ByteArrayToImage((byte[])(Array)(emp.photo), imgurl);
                        #endregion
                        #region Update EmployeeInfo
                        EmployeeInfo em = primaryDAO.GetIndividualMPInfoByEmployeeCode(emp.userId);

                        if (em == null)
                        {
                            EmployeeInfo missingEmp = new EmployeeInfo();
                            missingEmp.EmployeeCode = emp.userId;
                            missingEmp.UserName = emp.nameEng;
                            missingEmp.BanglaName = emp.nameBng;
                            missingEmp.FatherName = emp.fatherNameEng;
                            missingEmp.Address = emp.presentAddressEng;
                            missingEmp.PhoneNumber = emp.mobile;
                            missingEmp.Address = emp.presentAddressEng;
                            missingEmp.DesignationID = degId;
                            missingEmp.DepartmentID = 2;
                            missingEmp.Email = emp.email;
                            missingEmp.Status = 1;
                            missingEmp.UserType = "EMP";
                            missingEmp.NationalID = "N/A";
                            missingEmp.PhotoURL = dbUrl;
                            primaryDAO.InsertEmployeeInfo(missingEmp);

                        }
                        else
                        {

                            em.EmployeeCode = emp.userId;
                            em.UserName = emp.nameEng;
                            em.BanglaName = emp.nameBng;
                            em.FatherName = emp.fatherNameEng;
                            em.Address = emp.presentAddressEng;
                            em.PhoneNumber = emp.mobile;
                            em.UserType = "EMP";
                            em.DesignationID = degId;
                            em.DepartmentID = 2;
                            em.Email = emp.email;
                            em.Status = 1;
                            em.PhotoURL = dbUrl;
                            em.NationalID = "N/A";
                            primaryDAO.UpdateEmployeeInfo(em);
                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {

            }
           
            return null;
        }

        [HttpGet]
        public ActionResult GetEmployeeInfoList()
        {
            var data = primaryDAO.GetEmployeeInfoList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetActiveMPListByParliament(string parliamentNo)
        {
            var data = primaryDAO.GetActiveMPListByParliament(Convert.ToInt32(parliamentNo));
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetIndividualMPInfo()
        {
            var data = primaryDAO.GetIndividualMPInfo();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetActiveMPList(string parliamentNo)
        {
            var data = primaryDAO.GetActiveMPList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetActiveEmployeeInfoList()
        {
            var data = primaryDAO.GetActiveEmployeeInfoList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetEmployeeByRoleList(string roleId)
        {
            var data = primaryDAO.GetEmployeeByRoleList(Convert.ToInt32(roleId));
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetEmployeeByDept(string dptCode)
        {
            var data = primaryDAO.GetEmployeeByDept(dptCode);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult IsLogInInfoExist(string employeeId)
        {
            var data = primaryDAO.IsLogInInfoExist(employeeId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertEmployeeInfo(EmployeeInfo master)
        {
            try
            {
                if (primaryDAO.InsertEmployeeInfo(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmEmployeeInfo");
            }
            catch (Exception)
            {
                return View("frmEmployeeInfo");
            }
        }

        [HttpPost]
        public ActionResult UpdateEmployeeInfo(EmployeeInfo master)
        {
            try
            {
                if (primaryDAO.UpdateEmployeeInfo(master))
                {
                    return Json(new { Code = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes", ID = primaryDAO.MaxID });
                }
                return View("frmEmployeeInfo");
            }
            catch (Exception)
            {
                return View("frmEmployeeInfo");
            }
        }
    }
}