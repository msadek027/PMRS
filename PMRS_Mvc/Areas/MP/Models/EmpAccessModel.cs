using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMRS_Mvc.Areas.MP.Models
{
    public class EmpAccessModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class DesignationInfo
        {
            public int officeId { get; set; }
            public string officeNameEn { get; set; }
            public string officeNameBn { get; set; }
            public int designationId { get; set; }
            public string designationNameEn { get; set; }
            public string designationNameBn { get; set; }
            public string inchargeLabel { get; set; }
        }

        public class AccessPayload
        {
            public string username { get; set; }
            public string nameEng { get; set; }
            public string nameBng { get; set; }
            public string fatherNameEng { get; set; }
            public string fatherNameBng { get; set; }
            public string motherNameEng { get; set; }
            public string motherNameBng { get; set; }
            public string dateOfBirth { get; set; }
            public string gender { get; set; }
            public string personalEmail { get; set; }
            public string officialEmail { get; set; }
            public string mobileNumber { get; set; }
            public string presentAddressEng { get; set; }
            public string presentAddressBng { get; set; }
            public string permanentAddressEng { get; set; }
            public string permanentAddressBng { get; set; }
            public string nid { get; set; }
            public string passport { get; set; }
            public string passportIssueDate { get; set; }
            public string passportExpireDate { get; set; }
            public string religion { get; set; }
            public string bloodGroup { get; set; }
            public string identificationMark { get; set; }
            public sbyte[] photo { get; set; }
            public List<int> sign { get; set; }
            public double height { get; set; }
            public List<DesignationInfo> designationInfos { get; set; }
            public int empId { get; set; }
        }

        public class AccessRoot
        {
            public int responseCode { get; set; }
            public AccessPayload payload { get; set; }
            public string msg { get; set; }
        }


    }
}