﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NetaDAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class NetaDBEntities : DbContext
    {
        public NetaDBEntities()
            : base("name=NetaDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<county> counties { get; set; }
        public virtual DbSet<library_board_performance_NI> library_board_performance_NI { get; set; }
        public virtual DbSet<local_authority_data> local_authority_data { get; set; }
        public virtual DbSet<population> populations { get; set; }
        public virtual DbSet<postcode_speed> postcode_speed { get; set; }
        public virtual DbSet<school_performance> school_performance { get; set; }
        public virtual DbSet<view_passrates_by_broadband> view_passrates_by_broadband { get; set; }
        public virtual DbSet<average_performance_broadband> average_performance_broadband { get; set; }
    }
}