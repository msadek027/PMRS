
using System;
using System.Data;
using System.Globalization;
using PMRS_Mvc.Common;
using PMRS_Mvc.Models;
using System.Linq;
using System.Data.SqlClient;

namespace PMRS_Mvc.Areas.PMRS.DAO
{
    public class PRMSReportDAO
    {
        private readonly DBHelper _dh = new DBHelper();
        public DataTable GetPrimarySummary(string ParliamentSessionID, string fromDt, string toDt)
        {

            DateTime fdt = DateTime.ParseExact(fromDt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime tdt = DateTime.ParseExact(toDt, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string qry = "select * from [VW_Summary] Where 1=1 AND ParlSessID = "+ ParliamentSessionID + " AND MemberResolutionDate between '" + fdt + "' AND '" + tdt + "' Order by RDNo ";

          // string qry = "select * from VW_Summary";
          
            DataTable SummaryList = _dh.GetDataTable(qry);
            return SummaryList;
        }

        public DataTable GetBallotReport(string fromDt, string toDt)
        {
            DateTime fdt = DateTime.ParseExact(fromDt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime tdt = DateTime.ParseExact(toDt, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string qry = "select * from [VW_Summary] Where 1=1" +
                "AND BallotEnglish Between '" + fdt + "' AND '" + tdt + "' ";
            //string qry = "Select * from VW_RequisitionDetails AS VR INNER JOIN VW_ProductInfo AS VP on VR.ProjectNo = VP.ProjectNo WHERE ProjectNo = '" + reportData + "' AND ItemTypeCode = " + intItemTypeCode;
            DataTable ballotList = _dh.GetDataTable(qry);
            return ballotList;
        }

        public DataTable GetApprovedBySpeaker(string ParliamentSessionID, string fromDt, string toDt)
        {
            DateTime fdt = DateTime.ParseExact(fromDt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime tdt = DateTime.ParseExact(toDt, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string qry = "select a.* from [VW_Summary] a join [dbo].[ResolutionApproval] b on a.MemberResolutionID = b.MemberResolutionID Where 1=1 AND b.SpeakerApproveStatus = '1' AND a.ParlSessID = " + ParliamentSessionID + " AND b.SpeakerApproveDate between '"+fdt+"' AND '"+tdt+"' Order by RDNo ";
            DataTable SummaryList = _dh.GetDataTable(qry);
            return SummaryList;
        }


        public DataTable GetMPSummaryTabular(string ParliamentSessionID, string fromDt, string toDt)
        {
            DateTime fdt = DateTime.ParseExact(fromDt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime tdt = DateTime.ParseExact(toDt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DataTable dt = new DataTable();

            PMRS_BcEntities db = new PMRS_BcEntities();

            SqlConnection cnct = new SqlConnection(db.Database.Connection.ConnectionString);
            using (SqlCommand command = new SqlCommand("SP_TabularSummaryMP", cnct) { CommandType = CommandType.StoredProcedure })
            {

                command.Parameters.Add("@ParlSessID", SqlDbType.Int).Value = Convert.ToInt32(ParliamentSessionID);
                //command.Parameters.Add("@Type", SqlDbType.Int).Value = update.Type;

                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    da.Fill(dt);
                }

            }

            return dt;
        }

    }
}