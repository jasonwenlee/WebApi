﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApi.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class myproceduresEntities : DbContext
    {
        public myproceduresEntities()
            : base("name=myproceduresEntities")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<complication> complications { get; set; }
        public virtual DbSet<history> histories { get; set; }
        public virtual DbSet<keypoint> keypoints { get; set; }
        public virtual DbSet<procedure> procedures { get; set; }
        public virtual DbSet<reference> references { get; set; }
        public virtual DbSet<step> steps { get; set; }
        public virtual DbSet<variation> variations { get; set; }
    }
}
