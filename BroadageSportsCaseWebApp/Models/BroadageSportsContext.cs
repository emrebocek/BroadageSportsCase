using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BroadageSportsCaseWebApp.Models
{
    public partial class BroadageSportsContext : DbContext
    {
        public BroadageSportsContext()
        {
        }

        public BroadageSportsContext(DbContextOptions<BroadageSportsContext> options)
            : base(options)
        {

        }

        public virtual DbSet<AwayTeam> AwayTeams { get; set; } = null!;
        public virtual DbSet<HomeTeam> HomeTeams { get; set; } = null!;
        public virtual DbSet<Match> Matches { get; set; } = null!;
        public virtual DbSet<Round> Rounds { get; set; } = null!;
        public virtual DbSet<Score> Scores { get; set; } = null!;
        public virtual DbSet<Stage> Stages { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<Tournament> Tournaments { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=BroadageSports;Trusted_Connection=True;");
            }
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AwayTeam>(entity =>
            {
                entity.ToTable("AwayTeam");

                entity.HasOne(d => d.Score)
                    .WithMany(p => p.AwayTeams)
                    .HasForeignKey(d => d.ScoreId)
                    .HasConstraintName("FK_AwayTeam_Score");
            });

            modelBuilder.Entity<HomeTeam>(entity =>
            {
                entity.ToTable("HomeTeam");

                entity.HasOne(d => d.Score)
                    .WithMany(p => p.HomeTeams)
                    .HasForeignKey(d => d.ScoreId)
                    .HasConstraintName("FK_HomeTeam_Score");
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.ToTable("Match");

                entity.Property(e => e.MatchId).ValueGeneratedNever();

                entity.Property(e => e.MatchDate).HasColumnType("date");

                entity.HasOne(d => d.AwayTeam)
                    .WithMany(p => p.Matches)
                    .HasForeignKey(d => d.AwayTeamId)
                    .HasConstraintName("FK_Match_AwayTeam");

                entity.HasOne(d => d.HomeTeam)
                    .WithMany(p => p.Matches)
                    .HasForeignKey(d => d.HomeTeamId)
                    .HasConstraintName("FK_Match_HomeTeam");

                entity.HasOne(d => d.Round)
                    .WithMany(p => p.Matches)
                    .HasForeignKey(d => d.RoundId)
                    .HasConstraintName("FK_Match_Round");

                entity.HasOne(d => d.Stage)
                    .WithMany(p => p.Matches)
                    .HasForeignKey(d => d.StageId)
                    .HasConstraintName("FK_Match_Stage");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Matches)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Match_Status");

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.Matches)
                    .HasForeignKey(d => d.TournamentId)
                    .HasConstraintName("FK_Match_Tournament");
            });

            modelBuilder.Entity<Round>(entity =>
            {
                entity.ToTable("Round");

                entity.Property(e => e.RoundId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Score>(entity =>
            {
                entity.ToTable("Score");
            });

            modelBuilder.Entity<Stage>(entity =>
            {
                entity.ToTable("Stage");

                entity.Property(e => e.StageId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.StatusId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.ToTable("Tournament");

                entity.Property(e => e.TournamentId).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
