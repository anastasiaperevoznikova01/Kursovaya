using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace T__Shop
{
    public partial class Tattoo_ShopContext : DbContext
    {
        public Tattoo_ShopContext()
        {
        }

        public Tattoo_ShopContext(DbContextOptions<Tattoo_ShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Tattoo> Tattoo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-IQE6BBP; Database=Tattoo_Shop; Trusted_Connection=True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.UserId)
                    .HasMaxLength(450);

                entity.HasOne(d => d.Tattoo)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.TattooId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Tattoo");
            });

            modelBuilder.Entity<Tattoo>(entity =>
            {
                entity.Property(e => e.Image).HasColumnType("image");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
