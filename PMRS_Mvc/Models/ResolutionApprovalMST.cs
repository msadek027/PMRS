//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PMRS_Mvc.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ResolutionApprovalMST
    {
        public ResolutionApprovalMST()
        {
            this.ResolutionApprovalDTLs = new HashSet<ResolutionApprovalDTL>();
        }
    
        public int ResolutionApproveID { get; set; }
        public Nullable<System.DateTime> AdministrativeOfcApproveDate { get; set; }
        public Nullable<int> AdministrativeOfcEmpID { get; set; }
        public string AdministrativeOfcSignature { get; set; }
        public Nullable<System.DateTime> AssitantSccApproveDate { get; set; }
        public Nullable<int> AssitantSccEmpID { get; set; }
        public string AssitantSccSignature { get; set; }
        public Nullable<System.DateTime> SrAssitantSccApproveDate { get; set; }
        public Nullable<int> SrAssitantSccEmpID { get; set; }
        public string SrAssitantSccSignature { get; set; }
        public Nullable<System.DateTime> DeputySecApproveDate { get; set; }
        public Nullable<int> DeputySecEmpID { get; set; }
        public string DeputySecSignature { get; set; }
        public Nullable<System.DateTime> AddSecApproveDate { get; set; }
        public Nullable<int> AddSecEmpID { get; set; }
        public string AddSecSignature { get; set; }
        public Nullable<System.DateTime> SecApproveDate { get; set; }
        public Nullable<int> SecEmpID { get; set; }
        public string SecSignature { get; set; }
        public Nullable<System.DateTime> SpeakerApproveDate { get; set; }
        public Nullable<int> SpeakerEmpID { get; set; }
        public string SpeakerSignature { get; set; }
    
        public virtual ICollection<ResolutionApprovalDTL> ResolutionApprovalDTLs { get; set; }

        public string DataMode { get; set; }

        public string SendTo { get; set; }
    }
}