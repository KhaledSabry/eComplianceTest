using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eComplianceTest.DAL.Entities
{
    public class Customer : baseEntity
    {
        [Key]
        public string CustomerID { get; set; }
        [Required]
        public string CustomerName { get; set; }

    }
}
