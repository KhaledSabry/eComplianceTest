using eComplianceTest.DAL;
using eComplianceTest.DAL.Repositories;
using eComplianceTest.Modules;
using eComplianceTest.Services.mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eComplianceTest.Services
{
    public class BanckAccountService
    {
        readonly ApplicationDbContext _context;
        public BanckAccountService(ApplicationDbContext context)
        {
            this._context = context;
        }
        public BankAccountModel getByAccountNumber(string AccountNumber)
        {
            BankAccountRepository repository = new BankAccountRepository(this._context);
            return BanckAccountMapper.toModel(repository.getByAccountNumber(AccountNumber));
        }
        public List<BankAccountModel> getByCustomerID(string OwnerCustomerID)
        {
            BankAccountRepository repository = new BankAccountRepository(this._context);
            return repository.getByCustomerID(OwnerCustomerID).Select(a=> BanckAccountMapper.toModel(a)).ToList();
        }
        public BankAccountModel getByAccountToken(Guid token)
        {
            BankAccountRepository repository = new BankAccountRepository(this._context);
            return BanckAccountMapper.toModel(repository.getByAccountToken(token));
        }
        public BankAccountModel createAccount(string CustomerName,string CustomerID,string AccountNumber,float InitialBalance )
        {
            BankAccountRepository repository = new BankAccountRepository(this._context);

            var account = repository.getByAccountNumber(AccountNumber);
            if (account != null)
                throw new Exception("there's another bank account with this Bank Number."); 
          
            account = repository.createAccount(CustomerName, CustomerID, AccountNumber, InitialBalance);
            return BanckAccountMapper.toModel(account);
        }
        public async void addTransaction(string CustomerID, TransactionTypes transactionType, DateTime transactionTime, float amount, Currency currency, string sourceAccountNumber, string destnationAccountNumber=null)
        {
            AccountTransactionRepository transactionRepository = new AccountTransactionRepository(this._context); 

            CustomerRepository customerRepository = new CustomerRepository(this._context);
            BankAccountRepository accountRepository = new BankAccountRepository(this._context);
             
            var customer = customerRepository.getByCustomerID(CustomerID);
            if (customer == null)
                throw new Exception(string.Format("there's no customer with that customerID'{0}'", CustomerID));

            var sourceAccount = accountRepository.getByAccountNumber(sourceAccountNumber);
            if (customer == null)
                throw new Exception(string.Format("Source Bank Account Error:there's no BankAccount with that AccountNumber'{0}'", sourceAccountNumber));

            DAL.Entities.BankAccount destnationAccount = null;
            if (transactionType == TransactionTypes.transfer)
            {
                destnationAccount = accountRepository.getByAccountNumber(destnationAccountNumber);
                if (destnationAccount == null)
                    throw new Exception(string.Format("Destnation Bank Account Error:there's no BankAccount with that AccountNumber'{0}'", destnationAccountNumber));
            }

            await transactionRepository.addTransaction(customer, transactionType, transactionTime, amount, currency, sourceAccount, destnationAccount);
        }
    }
}
