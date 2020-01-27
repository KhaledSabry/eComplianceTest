using eComplianceTest.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eComplianceTest.DAL.Repositories
{
    public class AccountTransactionRepository : baseRepository
    {
        readonly ApplicationDbContext _context;
        public AccountTransactionRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<AccountTransaction> addTransaction(Customer customer , TransactionTypes transactionType, DateTime transactionTime, float amount, Currency currency, BankAccount sourceAccount, BankAccount destnationAccount )
        {  

            var mainTransaction = new AccountTransaction() { token = Guid.NewGuid(),customer= customer, amount = amount, transactionTime = transactionTime, sourceAccount = sourceAccount,destinationAccount = destnationAccount, transactionType = transactionType, currency = currency };
            float exchangeRate = 1.0f/(this._context.exchangeRates.Where(a=>a.currency== currency).Single()).rate;
            switch(transactionType)
            {
                case TransactionTypes.deposit:
                    mainTransaction.transactionDetails.Add(
                        new AccountTransactionDetails()
                        {
                            token = Guid.NewGuid(),
                            account = sourceAccount,
                            amount = amount * exchangeRate,// to convert to lacal curreny 
                        }
                        );

                    break;
                case TransactionTypes.withdraw:
                    mainTransaction.transactionDetails.Add(
                        new AccountTransactionDetails()
                        {
                            token = Guid.NewGuid(),
                            account = sourceAccount,
                            amount = -1*amount * exchangeRate,// to convert to lacal curreny 
                        }
                        );
                    break;
                case TransactionTypes.transfer:
                    mainTransaction.transactionDetails.Add(
                         new AccountTransactionDetails()
                         {
                             token = Guid.NewGuid(),
                             account = sourceAccount,
                             amount = -1 * amount * exchangeRate,// to convert to lacal curreny 
                        }
                         );
                    mainTransaction.transactionDetails.Add(
                        new AccountTransactionDetails()
                        {
                            token = Guid.NewGuid(),
                            account = destnationAccount,
                            amount = amount * exchangeRate,// to convert to lacal curreny 
                        }
                        );
                    break;
            }
            
            this._context.accountTransactions.Add(mainTransaction);

            return await this._context.SaveChangesAsync() > 0 ? mainTransaction : null;
        }
    }
}

 