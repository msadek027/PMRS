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
    
    public partial class SCEntities : DbContext
    {
        public SCEntities()
            : base("name=SCEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
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
        public virtual DbSet<VW_Login> VW_Login { get; set; }
    }
}