using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Domain;

namespace DataAccess
{
    public class DatabaseContext: DbContext
    {
        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ExchangeRate>()
                .Property(rate => rate.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ExchangeRate>().Property(rate => rate.Rate).HasPrecision(19, 9);
        }
    }
}
