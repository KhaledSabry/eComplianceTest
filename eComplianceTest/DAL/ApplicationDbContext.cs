
using eComplianceTest.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure; 
using System;
using System.Collections.Generic;
 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eComplianceTest.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(): base()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        { }

         
        public DbSet<CurrencyExchangeRate> exchangeRates { get; set; }
        public DbSet<Customer> customers { get; set; } 
        public DbSet<AccountTransaction> accountTransactions { get; set; }
        public DbSet<AccountTransactionDetails> transactionDetails { get; set; }
        public DbSet<BankAccount> bankAccounts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountTransactionDetails>()
            .HasOne(x => x.MainTransaction)
            .WithMany(a=>a.transactionDetails)
            .HasForeignKey(x => x.MainToken);

            modelBuilder.Entity<AccountTransactionDetails>()
            .HasOne(x => x.account)
            .WithMany(a => a.transactionDetails);
             

            base.OnModelCreating(modelBuilder);
        }



    }
}
