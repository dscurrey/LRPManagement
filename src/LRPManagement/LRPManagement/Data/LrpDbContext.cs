using LRPManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LRPManagement.Data
{
    public partial class LrpDbContext : DbContext
    {
        public LrpDbContext()
        {
        }

        public LrpDbContext(DbContextOptions<LrpDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bond> Bond { get; set; }
        public virtual DbSet<CharacterSkill> CharacterSkills { get; set; }
        public virtual DbSet<Character> Characters { get; set; }
        public virtual DbSet<Craftable> Craftables { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bond>
            (
                entity =>
                {
                    entity.HasKey(e => new {e.CharacterId, e.ItemId});

                    entity.HasOne(d => d.Character)
                        .WithMany(p => p.Bond)
                        .HasForeignKey(d => d.CharacterId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Bond_Characters");

                    entity.HasOne(d => d.Item)
                        .WithMany(p => p.Bond)
                        .HasForeignKey(d => d.ItemId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Bond_Craftables");
                }
            );

            modelBuilder.Entity<CharacterSkill>
            (
                entity =>
                {
                    entity.HasKey(e => e.Id);

                    entity.HasOne(d => d.Character)
                        .WithMany(p => p.CharacterSkills)
                        .HasForeignKey(d => d.CharacterId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CharacterSkills_Characters");

                    entity.HasOne(d => d.Skill)
                        .WithMany(p => p.CharacterSkills)
                        .HasForeignKey(d => d.SkillId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CharacterSkills_Skills");
                }
            );

            modelBuilder.Entity<Character>
            (
                entity =>
                {
                    entity.Property(e => e.Id).ValueGeneratedOnAdd();

                    entity.Property(e => e.CharacterRef)
                        .HasMaxLength(10)
                        .IsFixedLength();

                    entity.Property(e => e.Name).IsRequired();

                    entity.HasOne(d => d.Player)
                        .WithMany(p => p.Characters)
                        .HasForeignKey(d => d.PlayerId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Characters_Players");
                }
            );

            modelBuilder.Entity<Craftable>()
                .Property(c => c.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Player>(entity => { entity.Property(e => e.Id).ValueGeneratedOnAdd(); });

            modelBuilder.Entity<Skill>(entity => { entity.Property(e => e.Id).ValueGeneratedNever(); });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}