using System;
using DAL.Interface.DTO;

namespace NET.W._2017.Dubovik._14_15.Mappers
{
    internal static class EntityMapper
    {
        internal static BankAccount ToBankAccount(this Account account) =>
            new BankAccount
            {
                AccountType = account.GetType(),
                Id = account.Id,
                OwnerFirstName = account.OwnerFirstName,
                OwnerSecondName = account.OwnerSecondName,
                CurrentSum = account.CurrentSum,
                BonusPoints = account.BonusPoints,
            };

        internal static Account ToBllAccount(this BankAccount dalAccount) =>
            (Account)Activator.CreateInstance(
                dalAccount.AccountType,
                dalAccount.Id,
                dalAccount.OwnerFirstName,
                dalAccount.OwnerSecondName,
                dalAccount.CurrentSum,
                dalAccount.BonusPoints);
    }
}
