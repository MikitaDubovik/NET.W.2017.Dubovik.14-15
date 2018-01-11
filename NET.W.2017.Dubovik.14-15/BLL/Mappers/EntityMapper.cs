using System;
using BLL.Interface.Accounts;
using BLL.Interface.Owners;
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
                BonusPoints = account.BonusPoints
            };

        internal static Account ToBllAccount(this BankAccount dalAccount)
            => (Account)Activator.CreateInstance(
                GetAccountType(dalAccount.AccountType),
                dalAccount.AccountId,
                dalAccount.OwnerFirstName,
                dalAccount.OwnerSecondName,
                dalAccount.CurrentSum,
                dalAccount.BonusPoints);

        internal static OwnerAccount ToOwnerAccount(this Owner owner) =>
            new OwnerAccount()
            {
                Email = owner.Email,
                Password = owner.Password
            };

        internal static Owner ToBllOwner(this OwnerAccount dalOwner)
            => (Owner)Activator.CreateInstance(
                typeof(Owner),
                dalOwner.Email,
                dalOwner.Password);

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
