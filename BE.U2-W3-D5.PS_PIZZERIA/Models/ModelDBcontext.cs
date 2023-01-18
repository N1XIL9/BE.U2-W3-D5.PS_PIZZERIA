using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BE.U2_W3_D5.PS_PIZZERIA.Models
{
    public partial class ModelDBcontext : DbContext
    {
        public ModelDBcontext()
            : base("name=ModelDBcontext")
        {
        }

        public virtual DbSet<DETTAGLIO> DETTAGLIO { get; set; }
        public virtual DbSet<ORDINE> ORDINE { get; set; }
        public virtual DbSet<PIZZA> PIZZA { get; set; }
        public virtual DbSet<USER> USER { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ORDINE>()
                .Property(e => e.TotaleImporto)
                .IsFixedLength();

            modelBuilder.Entity<ORDINE>()
                .HasMany(e => e.DETTAGLIO)
                .WithRequired(e => e.ORDINE)
                .HasForeignKey(e => e.IdOrdine)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PIZZA>()
                .Property(e => e.Prezzo);
                

            modelBuilder.Entity<PIZZA>()
                .HasMany(e => e.DETTAGLIO)
                .WithRequired(e => e.PIZZA)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.ORDINE)
                .WithRequired(e => e.USER)
                .WillCascadeOnDelete(false);
        }
    }
}
