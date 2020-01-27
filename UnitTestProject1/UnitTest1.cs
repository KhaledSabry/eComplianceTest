using System;
using eComplianceTest.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using eComplianceTest.DAL.Entities;
using Xunit;
using eComplianceTest.Services;
using eComplianceTest.Modules;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {

        ApplicationDbContext _context;
        public UnitTest1()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            this._context = new ApplicationDbContext(builder.Options);

            //add seed data  
            this._context.exchangeRates.AddRange(
                new List<CurrencyExchangeRate> {
                new CurrencyExchangeRate(){token=Guid.NewGuid(), currency=eComplianceTest.Currency.CAD ,rate=1.0f},
                new CurrencyExchangeRate(){token=Guid.NewGuid(), currency=eComplianceTest.Currency.USD ,rate=0.5f},
                new CurrencyExchangeRate(){token=Guid.NewGuid(),currency=eComplianceTest.Currency.MXN , rate=10.0f}
                });

            this._context.SaveChanges();

        }



        [TestMethod]
        //  I should Use [MemberData("Data")] to pass all input data of all cases though it - but it didn't work for some reason and I didn't had time to investigate 
        // thus I had to create a function to test each case 
        public void Case1()
        {

            var service = new BanckAccountService(this._context);
            service.createAccount("Stewie Griffin", "777", "1234", 100.00f);

            service.addTransaction("777", eComplianceTest.TransactionTypes.deposit, DateTime.Now, 300, eComplianceTest.Currency.USD, "1234");
            BankAccountModel account = service.getByAccountNumber("1234");


            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(account.currentBalance, 700f);
        }
        [TestMethod]
        public void Case2()
        {

            var service = new BanckAccountService(this._context);
            service.createAccount("Glenn Quagmire", "504", "2001", 35000.00f);

            service.addTransaction("504", eComplianceTest.TransactionTypes.withdraw, DateTime.Now, 5000, eComplianceTest.Currency.MXN, "2001");//Glenn Quagmire withdraws $5,000.00 MXN from account number 2001. 
            service.addTransaction("504", eComplianceTest.TransactionTypes.withdraw, DateTime.Now, 12500, eComplianceTest.Currency.USD, "2001"); //Glenn Quagmire withdraws $12,500.00 USD from account number 2001. 
            service.addTransaction("504", eComplianceTest.TransactionTypes.deposit, DateTime.Now, 300, eComplianceTest.Currency.CAD, "2001"); //Glenn Quagmire deposits $300.00 CAD to account number 2001. 

            BankAccountModel account = service.getByAccountNumber("2001");


            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(account.currentBalance, 9800f);
        }
        [TestMethod]
        public void Case3()
        {
            var service = new BanckAccountService(this._context);
            service.createAccount("Joe Swanson", "002", "1010", 7425);
            service.createAccount("Joe Swanson", "002", "5500", 15000);

            service.addTransaction("002", eComplianceTest.TransactionTypes.withdraw, DateTime.Now, 5000, eComplianceTest.Currency.CAD, "5500");//Joe Swanson withdraws $5,000.00 CAD from account number 5500.
            service.addTransaction("002", eComplianceTest.TransactionTypes.transfer, DateTime.Now, 7300, eComplianceTest.Currency.CAD, "1010", "5500"); //Joe Swanson transfers $7,300.00 CAD from account number 1010 to account number 5500.
            service.addTransaction("002", eComplianceTest.TransactionTypes.deposit, DateTime.Now, 13726, eComplianceTest.Currency.MXN, "1010"); //Joe Swanson deposits $13,726.00 MXN to account number 1010. 

            BankAccountModel account1 = service.getByAccountNumber("1010");
            BankAccountModel account2 = service.getByAccountNumber("5500");

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(new { Balance1 = account1.currentBalance, Balance2 = account2.currentBalance },
                new { Balance1 = 1497.60f, Balance2 = 17300.00f });
        }
        [TestMethod]
        public void Case4()
        {
            var service = new BanckAccountService(this._context);
            service.createAccount("Peter Griffin", "123", "0123", 150);//Customer: Peter Griffin Customer ID: 123 Account Number: 0123 Initial balance for account number 0123: $150.00 CAD
            service.createAccount("Lois Griffin", "456", "0456", 65000);//Customer:  Customer ID: 456 Account Number: 0456 Initial balance for account number 0456: $65, 000.00 CAD

            service.addTransaction("123", eComplianceTest.TransactionTypes.withdraw, DateTime.Now, 70, eComplianceTest.Currency.USD, "0123"); //  Peter Griffin withdraws $70.00 USD from account number 0123.
            service.addTransaction("456", eComplianceTest.TransactionTypes.deposit, DateTime.Now, 23789, eComplianceTest.Currency.USD, "0456"); // Lois Griffin deposits $23,789.00 USD to account number 0456.
            service.addTransaction("456", eComplianceTest.TransactionTypes.transfer, DateTime.Now, 23.75f, eComplianceTest.Currency.CAD, "0456", "0123"); // Lois Griffin transfers $23.75 CAD from account number 0456 to Peter Griffin(account number 0123).


            BankAccountModel account1 = service.getByAccountNumber("0123");
            BankAccountModel account2 = service.getByAccountNumber("0456");


            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(new { Balance1 = account1.currentBalance, Balance2 = account2.currentBalance },
                new { Balance1 = 33.75f, Balance2 = 112554.25f });
        }
        

    }
}

