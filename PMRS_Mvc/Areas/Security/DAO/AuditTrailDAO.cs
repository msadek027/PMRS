using PMRS_Mvc.Models;
using System;
using System.Linq;

namespace PMRS_Mvc.Areas.Security.DAO
{
    public class AuditTrailDAO
    {
        public object GetAuditTrail(string fromDate, string toDate, string actionBy, string actionType)
        {
            using (SCEntities db = new SCEntities())
            {
                DateTime frmDt = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);

                DateTime toDt = DateTime.ParseExact(toDate, "dd/MM/yyyy", null).AddHours(23).AddMinutes(59);

                string qry = "Select * from VW_RPT_AuditTrail WHERE Action_Date >= '" + frmDt + "' AND Action_Date <= '" + toDt + "'";

                if (!string.IsNullOrEmpty(actionBy))
                {
                    qry = qry + " AND EmployeeID = " + actionBy + " ";
                }

                if (!string.IsNullOrEmpty(actionType))
                {
                    qry = qry + " AND Activity_Type = '" + actionType + "' ";
                }

                //var list = db.VW_RPT_AuditTrail.SqlQuery(qry).ToList();
                //return list;

                return 0;
            }
        }
    }
}