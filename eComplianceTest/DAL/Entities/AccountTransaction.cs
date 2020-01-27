using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eComplianceTest.DAL.Entities
{
    public class AccountTransaction:baseEntity
    {
        [Key]
        public Guid token { get; set; }

        public DateTime transactionTime { get; set; }
        [Required]
        public Customer customer { get; set; }
        [Required]
        public BankAccount sourceAccount { get; set; }
        public BankAccount destinationAccount { get; set; }
        [Required]
        public TransactionTypes transactionType { get; set; }
        [Required]
        public float amount { get; set; }
        public Currency currency { get; set; }

        public List<AccountTransactionDetails> transactionDetails { get; set; }
        public AccountTransaction()
        {
            this.transactionDetails = new List<AccountTransactionDetails>();
        }
    }
}
