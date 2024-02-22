using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMRS_Mvc.Areas.MP.Models
{
    public class MPInfoModel
    {
        public class ElectionDetailModel
        {
            public int id { get; set; }
            public int electionConstituencyNumber { get; set; }
            public string startDate { get; set; }
            public string endDate { get; set; }
            public int constituencyId { get; set; }
            public string constituencyNameEn { get; set; }
            public string constituencyNameBn { get; set; }
            public int politicalPartyId { get; set; }
            public string politicalPartyNameEn { get; set; }
            public string politicalPartyNameBn { get; set; }
            public string status { get; set; }
            public string division { get; set; }
            public string district { get; set; }
            public string thana { get; set; }
            public string boundaryDetails { get; set; }
            public string electionDate { get; set; }
            public int parliamentNo { get; set; }
        }

        public class Payload
        {
            public List<ElectionDetailModel> electionDetailModels { get; set; }
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
            public sbyte[] photo { get; set; }
            public string nid { get; set; }
            public string passport { get; set; }
            public string passportIssueDate { get; set; }
            public string passportExpireDate { get; set; }
            public string religion { get; set; }
            public string bloodGroup { get; set; }
            public string identificationMark { get; set; }
        }

        public class MpRoot
        {
            public int responseCode { get; set; }
            public List<Payload> payload { get; set; }
            public string msg { get; set; }
        }
    }
}