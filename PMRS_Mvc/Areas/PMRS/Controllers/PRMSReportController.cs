
using PMRS_Mvc.Areas.PMRS.DAO;
using PMRS_Mvc.Common;
using System.Data;
using PMRS_Mvc.Models;
using System;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


namespace PMRS_Mvc.Areas.PMRS.Controllers
{
    [LoginChecker]
    public class PRMSReportController : Controller
    {
        private PRMSReportDAO primaryDAO = new PRMSReportDAO();

        // GET: PMRS/PRMSReport
        public ActionResult frmPRMSRpt()
        {
          
            ViewBag.Msg = "";
            return View();
        }


        [HttpPost]
        public ActionResult frmPRMSRpt(string ReportName, string FromDate, string ToDate, string ParliamentSessionID, string ReportFormat)
        {
            ReportDocument rd = new ReportDocument();
            string reportPath = Server.MapPath("~/Areas/PMRS/Reports");
            DataTable dt = null;
            switch (ReportName)
            {
                case "Summary":
                    dt = primaryDAO.GetPrimarySummary(ParliamentSessionID, FromDate, ToDate);
                    reportPath = reportPath + "/rptPrimarySummary.rpt";
                   // reportPath = reportPath + "/rptTest.rpt";

                    rd.Load(reportPath);
                    rd.SetDataSource(dt);
                    rd.SetDataSource(dt);
                    rd.Refresh();
                    break;

                case "Ballot Info":
                    dt = primaryDAO.GetBallotReport(FromDate, ToDate);
                    reportPath = reportPath + "/rptBallotInfo.rpt";
                    rd.Load(reportPath);
                    rd.SetDataSource(dt);
                    rd.Refresh();
                    break;

                case "Circular Report":
                    dt = primaryDAO.GetBallotReport(FromDate, ToDate);
                    reportPath = reportPath + "/rptCircular.rpt";
                    rd.Load(reportPath);
                    rd.SetDataSource(dt);
                    rd.Refresh();
                    break;

                case "Speaker Approved":
                    dt = primaryDAO.GetApprovedBySpeaker(ParliamentSessionID, FromDate, ToDate);
                    reportPath = reportPath + "/rptSpeakerAccepted.rpt";
                    rd.Load(reportPath);
                    rd.SetDataSource(dt);
                    rd.Refresh();
                    break;

                case "MP Total Summary":
                    dt = primaryDAO.GetMPSummaryTabular(ParliamentSessionID, FromDate, ToDate);
                    reportPath = reportPath + "/rptTabularSummary.rpt";
                    rd.Load(reportPath);
                    rd.SetDataSource(dt);
                    rd.Refresh();
                    break;


            }


            if (dt != null && dt.Rows.Count > 0)
            {
                string rptName = ReportName;

                if (ReportFormat == "PDF")
                {
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, rptName);
                    //rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, rptName);
                }
                else if (ReportFormat == "Word")
                {
                    rd.ExportToHttpResponse(ExportFormatType.WordForWindows, System.Web.HttpContext.Current.Response, true, rptName);
                }

                rd.Close();
                rd.Dispose();
                return View();
            }
            rd.Close();
            rd.Dispose();
            ViewBag.Msg = "No Data Found";
            return View();
        }



    }
}