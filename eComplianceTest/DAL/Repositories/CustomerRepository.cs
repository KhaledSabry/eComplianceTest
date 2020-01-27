using eComplianceTest.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eComplianceTest.DAL.Repositories
{
    public class CustomerRepository
    {
        readonly ApplicationDbContext _context;
        public CustomerRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public Customer getByCustomerID(string CustomerID)
        {
            return this._context.customers.Where(a => a.CustomerID == CustomerID).FirstOrDefault();
        }
    }
}
