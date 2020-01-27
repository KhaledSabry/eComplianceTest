using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eComplianceTest.DAL.Entities
{
    public class AccountTransactionDetails : baseEntity
    {
        [Key]
        public Guid token { get; set; }

        [Required]
        public Guid MainToken { get; set; }

        [Required]
        public AccountTransaction MainTransaction { get; set; }
        
        [Required]
        public BankAccount account { get; set; }

        [Required]
        public float amount { get; set; }

    }
}
