//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BroadageSportsCase.Entity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BroadageSportsEntities : DbContext
    {
        public BroadageSportsEntities()
            : base("name=BroadageSportsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AwayTeam> AwayTeam { get; set; }
        public virtual DbSet<HomeTeam> HomeTeam { get; set; }
        public virtual DbSet<Match> Match { get; set; }
        public virtual DbSet<Round> Round { get; set; }
        public virtual DbSet<Score> Score { get; set; }
        public virtual DbSet<Stage> Stage { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Tournament> Tournament { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
