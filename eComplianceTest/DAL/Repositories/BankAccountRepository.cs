using eComplianceTest.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eComplianceTest.DAL.Repositories
{
    public class BankAccountRepository:baseRepository
    {
        readonly ApplicationDbContext _context;
        public BankAccountRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public BankAccount createAccount(string CustomerName, string CustomerID, string AccountNumber, float InitialBalance)
        {
            Customer accountOwner=this._context.customers.Where(a => a.CustomerID == CustomerID).FirstOrDefault();
            if(accountOwner==null) 
                accountOwner = new Customer() { CustomerID = CustomerID, CustomerName = CustomerName };

            var account = new BankAccount() { token = Guid.NewGuid(), accountOwner = accountOwner, accountNumber = AccountNumber, initialBalance = InitialBalance };
            this._context.bankAccounts.Add(account);
            
            return  this._context.SaveChanges() >0 ?account:null;
        }
        public BankAccount getByAccountNumber(string AccountNumber)
        {
            return this._context.bankAccounts.Where(a => a.accountNumber == AccountNumber).FirstOrDefault (); 
        }
        public List<BankAccount> getByCustomerID(string OwnerCustomerID)
        {
            return this._context.bankAccounts.Where(a => a.accountOwner.CustomerID == OwnerCustomerID).ToList();
        }
        public BankAccount getByAccountToken(Guid token)
        {
            return this._context.bankAccounts.Where(a => a.token == token).FirstOrDefault();
        }
    }

     
}
