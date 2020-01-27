using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eComplianceTest.Modules
{
    public class BankAccountModel
    {
        [Key]
        public Guid token { get; set; }
        [Required]
        public string CustomerID { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string accountNumber { get; set; }
        public float initialBalance { get; set; }
        public float currentBalance { get; set; }

        public List<DAL.Entities.AccountTransactionDetails> transactionDetails { get; set; } // in the real world we should use AccountTransactionModel insted of using the Enitity
        public BankAccountModel()
        {
            transactionDetails = new List<DAL.Entities.AccountTransactionDetails>();
        }
    }
}
