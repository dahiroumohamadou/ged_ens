using backend.Model;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using System.Text;

namespace backend.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {
           
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Personnel> Personnels { get; set; }
        public DbSet<Membre> Membres { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Paiement> Paiements { get; set; }
        public DbSet<Assistance> Assistances { get; set; }
        public DbSet<Periodicite> Periodicites { get; set; }
        public DbSet<Don> Dons { get; set; }
        public DbSet<Structure> Structures { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u=>u.Id);
                entity.Property(u => u.UserName);
                entity.Property(u => u.Password);
                entity.Property(u => u.UserEmail);
                entity.Property(u => u.SaltPassword);
                entity.Property(u => u.Token);
                entity.Property(u => u.KeepLoginIn);
            });
            modelBuilder.Entity<Categorie>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.libele);
                entity.Property(c => c.MontantAdhesion);
                entity.Property(c => c.MontantCotisation);

                entity.HasMany(c => c.Membres)
                    .WithOne(m => m.Categorie)
                    .HasForeignKey(m => m.CategorieId)
                    .OnDelete(DeleteBehavior.NoAction);

            });
            modelBuilder.Entity<Membre>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Noms);
                entity.Property(m => m.Prenoms);
                entity.Property(m => m.Matricule);
                entity.Property(m => m.MontantAdhesion);
                entity.Property(m => m.PhoneNumber);
                entity.Property(m => m.Fonction);
                entity.Property(m => m.StrcutureAffectation);


                entity.HasOne(m => m.Categorie)
                    .WithMany(c => c.Membres)
                    .HasForeignKey(m => m.CategorieId)
                    .OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(m => m.Structure)
                     .WithMany(s => s.Membres)
                     .HasForeignKey(m => m.StructureId)
                     .OnDelete(DeleteBehavior.NoAction);


                entity.HasMany(m => m.Paiements)
                    .WithOne(p => p.Membre)
                    .HasForeignKey(p => p.MembreId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(m => m.Assistances)
                    .WithOne(d => d.Membre)
                    .HasForeignKey(d => d.MembreId)
                    .OnDelete(DeleteBehavior.NoAction);
                entity.HasMany(m=>m.Dons)
                    .WithOne(d=>d.membre)
                    .HasForeignKey(d=>d.MembreId)
                    .OnDelete(DeleteBehavior.NoAction);


            });
            modelBuilder.Entity<Structure>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Code);
                entity.Property(s => s.Libele);
                entity.HasMany(s => s.Membres)
                    .WithOne(m => m.Structure)
                    .HasForeignKey(m => m.StructureId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Paiement>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Montant);              
                entity.Property(p => p.datePaiement);


                entity.HasOne(p => p.Membre)
                    .WithMany(b => b.Paiements)
                    .HasForeignKey(p => p.MembreId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(p => p.Periodicite)
                    .WithMany(b => b.Paiements)
                    .HasForeignKey(p => p.PeriodiciteId)
                    .OnDelete(DeleteBehavior.NoAction);


            });

            modelBuilder.Entity<Assistance>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Objet);
                entity.Property(d => d.Type);
                entity.Property(d => d.Proposition);
                entity.Property(d=>d.Montant);
                entity.Property(d => d.Date);

                entity.HasOne(d=>d.Membre)
                .WithMany(m=>m.Assistances)
                .HasForeignKey(d=>d.MembreId)
                .OnDelete(DeleteBehavior.NoAction);

            });
            modelBuilder.Entity<Don>(entity =>
            {
                entity.HasKey(d=>d.Id);
                entity.Property(d => d.Montant);
                entity.Property(d => d.Date);
                entity.Property(d => d.Type);
                entity.Property(d => d.Description);
                entity.Property(d => d.MembreId);
                entity.HasOne(d => d.membre)
                   .WithMany(m => m.Dons)
                   .HasForeignKey(d => d.MembreId)
                   .OnDelete(DeleteBehavior.NoAction);
            });


            modelBuilder.Entity<Periodicite>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.mois);
                entity.Property(p => p.annee);

            });
        }

    }
}
