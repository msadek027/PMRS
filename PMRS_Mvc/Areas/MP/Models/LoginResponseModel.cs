using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMRS_Mvc.Areas.MP.Models
{
    public class LoginResponseModel
    {
        public int responseCode { get; set; }
        public string payload { get; set; }
        public string msg { get; set; }
    }
}