using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eComplianceTest.DAL.Entities
{
    public class BankAccount : baseEntity
    {
        [Key]
        public Guid token { get; set; }
        [Required]
        public Customer accountOwner { get; set; }
        [Required]
        public string accountNumber { get; set; } 
        public float initialBalance { get; set; }

        public List<AccountTransactionDetails> transactionDetails { get; set; }
        public BankAccount()
        {
            transactionDetails = new List<AccountTransactionDetails>();
        }

    }
}
