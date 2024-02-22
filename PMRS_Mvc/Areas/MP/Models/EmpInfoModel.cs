using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMRS_Mvc.Areas.MP.Models
{
    public class EmpInfoModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class EmpPayload
        {
            public int id { get; set; }
            public string designationEng { get; set; }
            public string designationBng { get; set; }
            public int departmentId { get; set; }
            public int grade { get; set; }
            public string gradeEn { get; set; }
            public string gradeBn { get; set; }
            public string userId { get; set; }
            public string nameEng { get; set; }
            public string nameBng { get; set; }
            public string fatherNameEng { get; set; }
            public string fatherNameBng { get; set; }
            public string motherNameEng { get; set; }
            public string motherNameBng { get; set; }
            public string dateOfBirth { get; set; }
            public bool isFreedomFighter { get; set; }
            public string mobile { get; set; }
            public string email { get; set; }
            public sbyte[] photo { get; set; }
            public string additionalMobile { get; set; }
            public string additionalEmail { get; set; }
            public string presentAddressEng { get; set; }
            public string presentAddressBng { get; set; }
            public string permanentAddressEng { get; set; }
            public string permanentAddressBng { get; set; }
            public int empId { get; set; }
        }

        public class EmpRoot
        {
            public int responseCode { get; set; }
            public List<EmpPayload> payload { get; set; }
            public string msg { get; set; }
        }


    }
}