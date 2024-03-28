﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class PMRS_BcEntities : DbContext
    {
        public PMRS_BcEntities()
            : base("name=PMRS_BcEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ApprovalStatu> ApprovalStatus { get; set; }
        public virtual DbSet<BallotInfo> BallotInfoes { get; set; }
        public virtual DbSet<ConstitutentInfo> ConstitutentInfoes { get; set; }
        public virtual DbSet<ConstitutentUserMappingInfo> ConstitutentUserMappingInfoes { get; set; }
        public virtual DbSet<DepartmentInfo> DepartmentInfoes { get; set; }
        public virtual DbSet<DesignationInfo> DesignationInfoes { get; set; }
        public virtual DbSet<DocUpInfo> DocUpInfoes { get; set; }
        public virtual DbSet<EmployeeInfo> EmployeeInfoes { get; set; }
        public virtual DbSet<MemberResolutionInfo> MemberResolutionInfoes { get; set; }
        public virtual DbSet<ParliamentSessionInfo> ParliamentSessionInfoes { get; set; }
        public virtual DbSet<ResolutionApproval> ResolutionApprovals { get; set; }
        public virtual DbSet<ResolutionApprovalDTL> ResolutionApprovalDTLs { get; set; }
        public virtual DbSet<ResolutionApprovalMST> ResolutionApprovalMSTs { get; set; }
        public virtual DbSet<Audit_Trail> Audit_Trail { get; set; }
        public virtual DbSet<MailTrack> MailTracks { get; set; }
        public virtual DbSet<MLConf> MLConfs { get; set; }
        public virtual DbSet<MLMng> MLMngs { get; set; }
        public virtual DbSet<MNConf> MNConfs { get; set; }
        public virtual DbSet<RL> RLs { get; set; }
        public virtual DbSet<RLConf> RLConfs { get; set; }
        public virtual DbSet<RPTConf> RPTConfs { get; set; }
        public virtual DbSet<RPTMng> RPTMngs { get; set; }
        public virtual DbSet<SecMH> SecMHs { get; set; }
        public virtual DbSet<SecSM> SecSMs { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<VW_MPInfo> VW_MPInfo { get; set; }
        public virtual DbSet<VW_Summary> VW_Summary { get; set; }
        public virtual DbSet<VW_Tabular> VW_Tabular { get; set; }
        public virtual DbSet<VW_Login> VW_Login { get; set; }
    
        public virtual int ResolutionCountForMP(Nullable<int> empID, Nullable<System.DateTime> today, ObjectParameter r)
        {
            var empIDParameter = empID.HasValue ?
                new ObjectParameter("EmpID", empID) :
                new ObjectParameter("EmpID", typeof(int));
    
            var todayParameter = today.HasValue ?
                new ObjectParameter("Today", today) :
                new ObjectParameter("Today", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ResolutionCountForMP", empIDParameter, todayParameter, r);
        }
    
        public virtual int ResolutionUpdateLog(Nullable<int> resolutionID)
        {
            var resolutionIDParameter = resolutionID.HasValue ?
                new ObjectParameter("ResolutionID", resolutionID) :
                new ObjectParameter("ResolutionID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ResolutionUpdateLog", resolutionIDParameter);
        }
    
        public virtual int SP_TabularSummaryMP(Nullable<int> parlSessID)
        {
            var parlSessIDParameter = parlSessID.HasValue ?
                new ObjectParameter("ParlSessID", parlSessID) :
                new ObjectParameter("ParlSessID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_TabularSummaryMP", parlSessIDParameter);
        }
    }
}
