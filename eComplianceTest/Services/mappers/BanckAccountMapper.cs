using eComplianceTest.DAL.Entities;
using eComplianceTest.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eComplianceTest.Services.mappers
{
    public class BanckAccountMapper
    {
        public static BankAccountModel toModel(BankAccount entity)
        {
            BankAccountModel model = new BankAccountModel();
            if (entity != null)
            {
                model.token = entity.token;
                model.CustomerID = entity.accountOwner.CustomerID;
                model.CustomerName = entity.accountOwner.CustomerName;
                model.accountNumber = entity.accountNumber;
                model.initialBalance = entity.initialBalance;
                model.transactionDetails = entity.transactionDetails;//.Select(a => AccountTransactionMapper.toModel(a)).ToList();

                model.currentBalance =(float)Math.Round( model.initialBalance + model.transactionDetails .Sum(a => a.amount),2);
            }
            return model;
        }
        public static BankAccount toEntity(BankAccount entity, BankAccountModel model)
        {
            if (model != null)
            {
                entity.token = model.token;
                entity.accountOwner = new Customer() { CustomerID = model.CustomerID, CustomerName = model.CustomerName };
                entity.accountNumber = model.accountNumber;
                entity.initialBalance = model.initialBalance;
                entity.transactionDetails = model.transactionDetails;
            }
            else
                entity = new BankAccount();

            return entity;
        }
    }
}



 