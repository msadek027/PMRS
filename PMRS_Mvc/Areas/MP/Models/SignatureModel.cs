using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMRS_Mvc.Areas.MP.Models
{
    public class SignatureModel
    {
        public class Payload
        {
            public sbyte[] photo { get; set; }
            public sbyte[] sign { get; set; }
        }

        public class SignatureRoot
        {
            public int responseCode { get; set; }
            public Payload payload { get; set; }
            public string msg { get; set; }
        }
    }
}