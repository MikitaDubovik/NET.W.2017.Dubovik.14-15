using System;
using BLL.Interface.Accounts;
using DAL.Interface.DTO;
using NET.W._2017.Dubovik._14_15.Levels;

namespace NET.W._2017.Dubovik._14_15.Mappers
{
    internal static class EntityMapper
    {
        internal static BankAccount ToBankAccount(this Account account) =>
            new BankAccount
            {
                AccountType = account.GetType().Name,
                AccountId = account.Id,
                OwnerFirstName = account.OwnerFirstName,
                OwnerSecondName = account.OwnerSecondName,
                CurrentSum = account.CurrentSum,
                BonusPoints = account.BonusPoints,
            };

        internal static Account ToBllAccount(this BankAccount dalAccount)
            => (Account) Activator.CreateInstance(
                GetAccountType(dalAccount.AccountType),
                dalAccount.AccountId,
                dalAccount.OwnerFirstName,
                dalAccount.OwnerSecondName,
                dalAccount.CurrentSum,
                dalAccount.BonusPoints);

        private static Type GetAccountType(string type)
        {
            if (type.Contains("Gold"))
            {
                return typeof(GoldAccount);
            }

            if (type.Contains("Platinum"))
            {
                return typeof(PlatinumAccount);
            }

            return typeof(BaseAccount);
        }
    }
}
