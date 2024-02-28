using PMRS_Mvc.Models;
using System.Data;
using System.Data.SqlClient;

namespace PMRS_Mvc.Common
{
    public class DBHelper
    {
        public DataTable GetDataTable(string qry)
        {
            using (PMRS_BcEntities db = new PMRS_BcEntities())
            {
                string cnct = db.Database.Connection.ConnectionString;

                SqlConnection conn = new SqlConnection(cnct);
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from [VW_Summary] Where 1=1 AND ParlSessID = 1005 AND MemberResolutionDate between '2024-01-01' AND '2024-03-29' Order by RDNo ";// qry;
                da.SelectCommand = cmd;
                DataTable ds = new DataTable();

                conn.Open();
                da.Fill(ds);
                conn.Close();

                return ds;
            }
        }
    }
}