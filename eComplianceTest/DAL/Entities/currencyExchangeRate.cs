using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eComplianceTest.DAL.Entities
{
    public class CurrencyExchangeRate : baseEntity
    { 
        [Key]
        public Guid token { get; set; }
        [Required]
        public Currency currency { get; set; } 
        [Required]
        public float rate { get; set; }
    }
}
