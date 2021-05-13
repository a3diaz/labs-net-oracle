using Labs.NET.Oracle.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Labs.NET.Oracle.Infrastructure.Data
{
    public sealed class LabsContext:DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Phone> Phones { get; set; }

        public LabsContext(DbContextOptions<LabsContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(people =>
            {
                people.ToTable("Persons");
                people.HasKey(p => p.PersonId);

                people.Property(p => p.Name)
                    .HasMaxLength(60);

                people.Property(p => p.Lastname)
                    .HasMaxLength(60);

                people.HasMany(p => p.Phones)
                    .WithOne(p => p.Owner)
                    .HasPrincipalKey(p => p.PersonId)
                    .HasForeignKey(p => p.OwnerId);
            });

            modelBuilder.Entity<Phone>(Phones =>
            {
                Phones.ToTable("Phones");
                Phones.HasKey(p => p.PhoneId);
                Phones.HasAlternateKey(p => p.Number);

                Phones.Property(p => p.Number)
                    .HasMaxLength(12);
            });
        }
    }
}
