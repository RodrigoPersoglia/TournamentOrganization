using Microsoft.EntityFrameworkCore;
using TournamentOrganization.Domain.Entities;

namespace TournamentOrganization.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerTournament> PlayerTournament { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Match> Matches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(p => p.FirstName)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(p => p.LastName)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(p => p.SkillLevel)
                      .IsRequired();

                entity.Property(p => p.Gender)
                      .IsRequired()
                      .HasMaxLength(6);

                entity.Property(p => p.Strength)
                      .IsRequired(false);

                entity.Property(p => p.Speed)
                      .IsRequired(false);

                entity.Property(p => p.ReactionTime)
                      .IsRequired(false);
            });

            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.Property(t => t.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(t => t.Name)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(t => t.StartDate)
                      .IsRequired();

                entity.Property(t => t.PlayerGender)
                      .IsRequired()
                      .HasMaxLength(6);
            });

            modelBuilder.Entity<PlayerTournament>(entity =>
            {
                entity.HasKey(pt => new { pt.PlayerId, pt.TournamentId });

                entity.Property(pt => pt.Id)
                      .ValueGeneratedOnAdd();

                entity.HasOne(pt => pt.Player)
                      .WithMany(p => p.PlayerTournaments)
                      .HasForeignKey(pt => pt.PlayerId);

                entity.HasOne(pt => pt.Tournament)
                      .WithMany(t => t.PlayerTournaments)
                      .HasForeignKey(pt => pt.TournamentId);
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.HasKey(m => m.Id);

                entity.Property(m => m.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(m => m.Date)
                      .IsRequired();

                entity.Property(m => m.Player1Id)
                      .IsRequired();

                entity.Property(m => m.Player2Id)
                      .IsRequired();

                entity.Property(m => m.WinnerId)
                      .IsRequired();

                entity.Property(m => m.TournamentId)
                      .IsRequired();

                entity.Property(m => m.Stage)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasOne(m => m.Player1)
                      .WithMany()
                      .HasForeignKey(m => m.Player1Id)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.Player2)
                      .WithMany()
                      .HasForeignKey(m => m.Player2Id)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.Winner)
                      .WithMany()
                      .HasForeignKey(m => m.WinnerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.Tournament)
                      .WithMany(t => t.Matches)
                      .HasForeignKey(m => m.TournamentId);
            });

        }
    }
}
