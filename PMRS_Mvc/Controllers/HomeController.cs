
using PMRS_Mvc.Areas.MP.Models;
using PMRS_Mvc.Areas.Security.DAO;
using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Text;

using PMRS_Mvc.Areas.Transaction.DAO;
using System.Collections.Generic;

using System.Drawing;
using System.Drawing.Imaging;
using System.Net;

namespace PMRS_Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly PMRS_BcEntities _mDB = new PMRS_BcEntities();
        private readonly SCEntities _db = new SCEntities();
        private readonly UserLoginDAO _lgndao = new UserLoginDAO();
        private readonly AuditTrailLogger _adt = new AuditTrailLogger();
        private readonly FileChecker _fileChecker = new FileChecker();
        private static readonly HttpClient client = new HttpClient();
        private EmployeeInfoDAO primaryDAO = new EmployeeInfoDAO();
        private DepartmentInfoDAO deptInfoDAO = new DepartmentInfoDAO();
        private DesignationInfoDAO degInfoDAO = new DesignationInfoDAO();

        [LoginChecker]
        public ActionResult Index()
        {
            return View();
        }

        [LoginChecker]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Login()
        {
            _db.Database.CreateIfNotExists();
            return View();
        }


        public async Task<string> SingleSignOn(UserLogin model)
        {
            try
            {
                //bool result = false;
                var requestData = new
                {
                    username = "squareit",   // Use the provided username parameter
                    password = "T}Qyl&ml"    // Use the provided password parameter
                };
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                var content = new StringContent(
                    JsonConvert.SerializeObject(requestData),
                    Encoding.UTF8,
                    "application/json");
                string apiUrl = "https://prp.parliament.gov.bd/api/authentication/external?action=token";
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    LoginResponseModel loginmodel = JsonConvert.DeserializeObject<LoginResponseModel>(responseContent);
                    Session["authToken"] = loginmodel.payload;
                    var lgn = new EmployeeInfo();
                    try
                    {
                         lgn = primaryDAO.GetIndividualMPInfoByEmployeeCode(model.UserLoginName);
                    }
                    catch(Exception ex)
                    {
                        return "Lgn nai";
                    }
                    
                    //var client = new HttpClient();
                    if (lgn != null)
                    {
                        if (lgn.UserType == "EMP")
                        {
                            apiUrl = "https://prp.parliament.gov.bd/api/secure/external?action=emp_verify";
                        }
                        else
                        {
                            apiUrl = "https://prp.parliament.gov.bd/api/secure/external?action=mp_verify";
                        }
                    }
                    else
                    {
                        return "lng null";
                    }
                    var requestAccessData = new
                    {
                        username = model.UserLoginName,   // Use the provided username parameter
                        password = model.Password    // Use the provided password parameter
                    };

                    var accessContent = new StringContent(
                        JsonConvert.SerializeObject(requestAccessData),
                        Encoding.UTF8,
                        "application/json");

                    var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
                    request.Headers.Add("Authorization", Session["authToken"].ToString());
                    request.Content = accessContent;
                    var accessResponse = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent2 = await accessResponse.Content.ReadAsStringAsync();
                        response.EnsureSuccessStatusCode();
                        PMRS_Mvc.Areas.MP.Models.EmpAccessModel.AccessRoot accessModel = JsonConvert.DeserializeObject<PMRS_Mvc.Areas.MP.Models.EmpAccessModel.AccessRoot>(responseContent2);


                        //if (accessModel.msg != "Success")///--------
                        //{
                            var dept = deptInfoDAO.GetDepartmentInfoById(lgn.DepartmentID);
                            var deg = degInfoDAO.GetDesignationInfoById(lgn.DesignationID.Value);
                            int ID = Convert.ToInt32(lgn.UserID);

                            Session["empID"] = lgn.UserID;
                            Session["empName"] = lgn.UserName;
                            Session["EmployeeCode"] = lgn.EmployeeCode;
                            Session["Username"] = lgn.UserName;
                            Session["Photo"] = lgn.PhotoURL;
                            Session["Signature"] = lgn.Signature;
                            Session["UserId"] = lgn.UserID;
                            Session["Grade"] = deg.Grade;
                        if (deg != null)
                            {
                                Session["Designation"] = deg.DesignationName;
                            }
                            else
                            {
                                Session["Designation"] = "";
                            }

                            if (dept != null)
                            {
                                Session["Department"] = dept.DepartmentName;
                            }
                            else
                            {
                                Session["Department"] = "";
                            }

                            Session["UserMenu"] = "";
                            _adt.InsertAudit("Login", "UserLogin", "Login", "", ID);

                            return "true";
                    //}

                    //else
                    //{
                    //    return response.IsSuccessStatusCode + ";" + requestAccessData.username + " ;" + requestAccessData.password + ";" + apiUrl + ";" + responseContent2; 
                    //}
                }

                return "false";
                }
                else
                {
                    return "false 1";
                }
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message + "  sdfsdfsdf    " + ex.Message;
            }
        }



        //public async Task<string> SingleSignOn(UserLogin model)
        //{
        //    string apiUrl = "";
        //    try
        //    {
        //        var lgn = primaryDAO.GetIndividualMPInfoByEmployeeCode(model.UserLoginName);
        //        if(lgn != null)
        //        {
        //            if (CheckSingleLogin(model).Result == true)
        //            {
        //                int ID = Convert.ToInt32(lgn.UserID);

        //                Session["empID"] = lgn.UserID;
        //                Session["empName"] = lgn.UserName;
        //                Session["EmployeeCode"] = lgn.EmployeeCode;
        //                Session["Username"] = lgn.UserName;

        //                Session["Designation"] = lgn.DesignationID;
        //                Session["Department"] = lgn.DepartmentID;



        //                Session["UserMenu"] = "";
        //                _adt.InsertAudit("Login", "UserLogin", "Login", "", ID);

        //                return "true";
        //            }
        //        }

        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        Console.WriteLine("HTTP request exception: " + ex.Message);
        //    }

        //    return "false";
        //}

        public string TryLogin(UserLogin model)
        {
            if ((model.UserLoginName.Length > 0 && model.Password.Length > 0))
            {
                string pass = _lgndao.Encrypt(model.Password);

                var lgn = (from t in _db.VW_Login
                           where t.UserLoginName == model.UserLoginName && t.Password == pass && t.Status == 1
                           select t).FirstOrDefault();
                // bool result = SingleSignOn("squareit", "T}Qyl&ml").Result;

                if (lgn != null)
                {
                    int ID = Convert.ToInt32(lgn.UserID);

                    Session["empID"] = lgn.UserID;
                    Session["empName"] = lgn.UserName;
                    Session["EmployeeCode"] = lgn.EmployeeCode;
                    Session["Username"] = lgn.UserLoginName;
                    Session["Designation"] = lgn.DesignationName;
                    Session["Department"] = lgn.DepartmentName;
                    //Session["DepartmentCode"] = lgn.DepartmentCode;
                    Session["UserMenu"] = "";
                    //string tst = Regex.Unescape(Session["CompanyCode"].ToString());

                    //tst = Regex.Unescape(tst);
                    //tst = tst.Replace("\"", " ");

                    _adt.InsertAudit("Login", "UserLogin", "Login", "", ID);

                    return "true";
                }
            }
            return "false";
        }

        [HttpGet]
        public string CreateMenu()
        {
            if (Session["UserMenu"].ToString().Length == 0)
            {
                string htmlMenu = "";

                int empId = Convert.ToInt32(Session["empID"].ToString());
                //int empID = 5;

                var mhList = (from a in _db.RLConfs
                              join b in _db.RLs on a.RL_ID equals b.ID
                              join c in _db.MNConfs on a.RL_ID equals c.RL_ID
                              join e in _db.SecMHs on c.MH_ID equals e.ID
                              where a.Emp_ID == empId
                              orderby e.Seq
                              select new
                              {
                                  e.Nm,
                                  e.ID,
                                  e.Seq,
                                  e.CssClass
                              }).Distinct().OrderBy(s => s.Seq);

                htmlMenu = htmlMenu + "<ul class='nav nav-sidebar'>";
                htmlMenu = htmlMenu +
                //"<li class='nav-parent'><a href='/Home/Index'><i class='icon-star' style='color: #66d659;'></i><span>Dashboard</span></a></li>";
                "<li><a href='/Home/Index'><i class='icon-star' style='color: #66d659;'></i><span>Dashboard</span></a></li>";

                foreach (var u in mhList)
                {
                    htmlMenu = htmlMenu + "<li class='nav-parent  " + u.Nm + "'>";
                    //htmlMenu = htmlMenu + "<a href='#'><i class='" + u.CssClass + "'></i><span>" + u.Nm + "</span></a>";
                    htmlMenu = htmlMenu + "<a href='#'><i class='fa fa-th' style='color: #66d659;'></i><span>" + u.Nm + "</span></a>";
                    //htmlMenu = htmlMenu + "<a href='#'> <img src='/assets/images/square_only_logo.png' height='20px' ; /><span>" + u.Nm + "</span></a>";

                    var u1 = u;
                    var sublist = from a in _db.RLConfs
                                  join b in _db.RLs on a.RL_ID equals b.ID
                                  join c in _db.MNConfs on a.RL_ID equals c.RL_ID
                                  join e in _db.SecSMs on c.SM_ID equals e.ID
                                  where a.Emp_ID == empId && c.MH_ID == u1.ID && e.ID == c.SM_ID
                                  orderby e.Seq
                                  select new
                                  {
                                      e.Nm,
                                      e.ID,
                                      e.Seq,
                                      e.Url,
                                      e.CssClass
                                  };

                    if (sublist.Any())
                    {
                        htmlMenu = htmlMenu + "<ul class= 'children collapse'>";

                        foreach (var v in sublist)
                        {
                            //htmlMenu = htmlMenu + " <li><a href='" + v.Url + "' data-toggle='tooltip' title='"+ v.Nm + "' data-translate='buttons'><i class='" + v.CssClass + "'></i> " + v.Nm + "</a></li>";
                            htmlMenu = htmlMenu + " <li><a href='" + v.Url + "' data-toggle='tooltip' title='" + v.Nm +
                                       //  "' data-translate='buttons'><i class='" + v.CssClass + "'></i> <span>" + v.Nm +
                                       "' data-translate='buttons'><img src='/assets/images/sqr_pharama_logo.png' height='20px' ; /></i> <span>" + v.Nm +
                                       "</span ></a></li>";
                        }

                        htmlMenu = htmlMenu + "</ul>";
                    }

                    htmlMenu = htmlMenu + "</li>  </li>";
                }

                htmlMenu = htmlMenu + "</ul>";
                //*****************************nav bar hover*******************************
                htmlMenu = htmlMenu + "<script>$('.nav-sidebar > li').hover(function() {";
                htmlMenu = htmlMenu + "clearTimeout(hoverTimeout);";
                htmlMenu = htmlMenu + "     $(this).siblings().removeClass('nav-hover');";
                htmlMenu = htmlMenu + "     $(this).addClass('nav-hover');";
                htmlMenu = htmlMenu + " }, function() {";
                htmlMenu = htmlMenu + "    var $self = $(this);";
                htmlMenu = htmlMenu + "    hoverTimeout = setTimeout(function() {";
                htmlMenu = htmlMenu + "        $self.removeClass('nav-hover');";
                htmlMenu = htmlMenu + "    }, 200);";
                htmlMenu = htmlMenu + " });";
                htmlMenu = htmlMenu + "  $('.nav-sidebar > li .children').hover(function() {";
                htmlMenu = htmlMenu + "     clearTimeout(hoverTimeout);";
                htmlMenu = htmlMenu + "        $(this).closest('.nav-parent').siblings().removeClass('nav-hover');";
                htmlMenu = htmlMenu + "       $(this).closest('.nav-parent').addClass('nav-hover');";
                htmlMenu = htmlMenu + "}, function() {";
                htmlMenu = htmlMenu + "   var $self = $(this);";
                htmlMenu = htmlMenu + "  hoverTimeout = setTimeout(function() {";
                htmlMenu = htmlMenu + "      $(this).closest('.nav-parent').removeClass('nav-hover');";
                htmlMenu = htmlMenu + "   }, 200);";
                htmlMenu = htmlMenu + " });";

                //htmlMenu = htmlMenu + "var url = window.location.href;";
                //*****************************active when pageload*******************************
                htmlMenu = htmlMenu + "var url = window.location.pathname;";
                //htmlMenu = htmlMenu + " var res = url.split('/');";
                htmlMenu = htmlMenu + " var status = ' ';";
                //htmlMenu = htmlMenu + " var newURL = window.location.protocol + '_ _' + window.location.host + '_ _' + window.location.pathname;";

                //htmlMenu = htmlMenu + "varhref=res[1]);";
                //htmlMenu = htmlMenu + "alert(url);";
                htmlMenu = htmlMenu + "  $('li.nav-parent').each(function(){";
                htmlMenu = htmlMenu + "    var parent_span_data = $(this).find('a span').html();";

                htmlMenu = htmlMenu + "     $(this).find('ul.children li').each(function(){";
                htmlMenu = htmlMenu + "         var a_href = $(this).find(' a').attr('href');";
                htmlMenu = htmlMenu + "         var child_span_data = $(this).find('a span').html();";
                //htmlMenu = htmlMenu + "      alert(parent_span_data);";
                //htmlMenu = htmlMenu + "      alert(child_span_data);";
                htmlMenu = htmlMenu + "         if (a_href == url)";
                htmlMenu = htmlMenu + "          {";
                // htmlMenu = htmlMenu + "          alert('yes');";
                htmlMenu = htmlMenu + "          $('.'+parent_span_data).addClass('nav-active active');";
                htmlMenu = htmlMenu + "          $(this).addClass('active');";
                htmlMenu = htmlMenu + "          $(this).attr('id','active');";
                htmlMenu = htmlMenu + "          $('.mCSB_container').removeClass('mCS_no_scrollbar');";
                htmlMenu = htmlMenu + "         $('.mCSB_scrollTools').show();";
                htmlMenu = htmlMenu + "          $(this).parent().css('display','block');";
                //htmlMenu = htmlMenu + "          $('.sidebar-inner').mCustomScrollbar('scrollTo', $('.sidebar-inner').find('.mCSB_container').find('.active'));";
                htmlMenu = htmlMenu + "          $('.sidebar-inner').mCustomScrollbar('scrollTo', '#active');";

                htmlMenu = htmlMenu + "          status='f';";
                //htmlMenu = htmlMenu + "alert(status);";
                htmlMenu = htmlMenu + "         return false;";
                htmlMenu = htmlMenu + "        }";
                htmlMenu = htmlMenu + "     });";
                htmlMenu = htmlMenu + "     if(status=='f'){return false;}";
                htmlMenu = htmlMenu + "});";
                //htmlMenu = htmlMenu + "$('.sidebar').mCustomScrollbar('scrollTo', $('.active').position().top);";
                // htmlMenu = htmlMenu + "$('.sidebar-inner').mCustomScrollbar({ theme: 'minimal' });";

                //htmlMenu = htmlMenu + "$('.mCSB_container').find('.active').position().top;";
                htmlMenu = htmlMenu + " </script>";

                Session["UserMenu"] = htmlMenu;
            }

            return Session["UserMenu"].ToString();
        }

        [HttpPost]
        public JsonResult EventPermission(string smId)
        {
            int empId = Convert.ToInt32(Session["empID"].ToString());
            //int empID = 5;
            int smD = Convert.ToInt32(smId);

            var data = from t in _db.RLConfs
                       from u in _db.MNConfs
                       from v in _db.SecSMs
                       from m in _db.SecMHs
                       where t.RL_ID == u.RL_ID
                             && u.SM_ID == v.ID
                             && v.MH_ID == m.ID
                             && t.Emp_ID == empId && u.SM_ID == smD
                       select new
                       {
                           u.Sv,
                           u.Dl,
                           v.Nm,
                           v.MH_ID,
                           MenuName = m.Nm
                       };

            return Json(data);
        }

        public ActionResult LogOut()
        {
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Login", "Home", "");
        }

        [HttpPost]
        public JsonResult UploadManager(string FolderName, string PrNo, string DocName, string TrackNo)
        {
            string message = " ";
            try
            {
                // string filePath = string.Empty;
                HttpPostedFileBase document = Request.Files[0];
                string path = Server.MapPath("~/UploadDocs/" + FolderName + "/");
                string filename = DocName + "_" + PrNo + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Path.GetFileName(document.FileName);
                string filePath = "/UploadDocs/" + FolderName + "/" + filename;
                string formName = "frm" + FolderName;
                if (TrackNo == "undefined")
                {
                    message = "notDone";
                    return Json(new { Message = message, TrackNo = TrackNo });
                }
                if (!_fileChecker.UploadManager(document, path, filename))
                {
                    message = "notDone";
                    return Json(new { Message = message, TrackNo = TrackNo });
                    //return Json(new {Message = Message, FilePath = FilePath, Filename = filename});
                }

                DocUpInfo docUpInfo = new DocUpInfo
                {
                    DocName = DocName,
                    FileName = filename,
                    FilePath = filePath,
                    FormName = formName,
                    PrNo = PrNo,
                    TrackNo = TrackNo,
                    UploadedBy = Convert.ToInt32(System.Web.HttpContext.Current.Session["empID"]),
                    UploadedDate = DateTime.Now
                };
                using (PMRS_BcEntities obj = new PMRS_BcEntities())
                {
                    obj.DocUpInfoes.Add(docUpInfo);
                    obj.SaveChanges();
                }
                message = "done";
            }
            catch (Exception ex)
            {
                string s = ex.ToString();
                message = "notDone";
            }

            return Json(new { Message = message, TrackNo = TrackNo });
        }

        [HttpPost]
        public JsonResult DeleteDoc(string FolderName, string FileName, string PrNo, string ID)
        {
            string message = " ";
            //string filePath = string.Empty;
            // HttpPostedFileBase document = Request.Files[0];
            string filePath = Server.MapPath("~/UploadDocs/" + FolderName + "/" + FileName);
            //string filename = PropsalNo+"_"+Path.GetFileName(document.FileName);
            if (_fileChecker.DeleteDoc(filePath, PrNo, ID))
            {
                message = "Delete";
                //string FilePath ="/UploadDocs/ "+FolderName+"/"+filename;
                return Json(new { Message = message });
            }

            //return  Json { new { Message = Message,FilePath= path+ filename } };
            return Json(new { Message = message });
        }

        //[HttpPost]
        //public JsonResult GetDocInfo(string PrNo, string FormName, string TrackNo)
        //{
        //    var data = _fileChecker.GetDocInfo(PrNo, FormName, TrackNo);
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        public JsonResult GetDocInfo(string PrNo, string FormNamePrv, string FormName, string TrackNo)
        {
            if (FormNamePrv != null)
            {
                var docData = _fileChecker.GetDocInfo(PrNo, FormNamePrv, FormName, TrackNo);
                return Json(docData, JsonRequestBehavior.AllowGet);
            }

            var data = _fileChecker.GetDocInfo(PrNo, FormName, TrackNo);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}