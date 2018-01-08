using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Accounts;
using BLL.Interface.IDGenerator;
using DAL.Interface;
using NET.W._2017.Dubovik._14_15.Levels;
using NET.W._2017.Dubovik._14_15.Mappers;

namespace NET.W._2017.Dubovik._14_15.AccountService
{
    /// <inheritdoc />
    public class AccountService : IAccountService
    {
        #region Fields

        private readonly IRepository accountRepository;
        private readonly IIdGenerator accountIdGenerator;

        private readonly List<Account> accounts = new List<Account>();

        #endregion

        #region public

        #region Cotr

        /// <summary>
        /// Initializes the instance of service
        /// </summary>
        /// <param name="otherAccountRepository">Account repository service</param>
        /// <param name="otherAccountIdService">Account ID service</param>
        public AccountService(IRepository otherAccountRepository, IIdGenerator otherAccountIdService)
        {
            if (ReferenceEquals(otherAccountRepository, null))
            {
                throw new ArgumentNullException(nameof(accountRepository));
            }

            if (ReferenceEquals(otherAccountIdService, null))
            {
                throw new ArgumentNullException(nameof(accountIdGenerator));
            }

            accountRepository = otherAccountRepository;
            accountIdGenerator = otherAccountIdService;

            try
            {
                var dalAccounts = accountRepository.GetAccounts();
                accounts.AddRange(dalAccounts.Select(account => account.ToBllAccount()));
            }
            catch (Exception)
            {
                accounts.Clear();
            }
        }

        #endregion

        #region Implementation

        /// <inheritdoc />
        public string OpenAccount(string ownerFirstName, string ownerSecondName, decimal sum) =>
            OpenAccount(AccountsLevel.Base, ownerFirstName, ownerSecondName, sum);

        /// <inheritdoc />
        public string OpenAccount(AccountsLevel accountsLevel, string ownerFirstName, string ownerSecondName, decimal sum)
        {
            CheckInput(ownerFirstName, ownerSecondName, sum);

            try
            {
                string accountId = accountIdGenerator.GenerateAccountId();

                int initialBonuses = GetInitialBonuses(accountsLevel);

                Type typeOfAccount = GetTypeOfAccount(accountsLevel);

                var account = CreateAccount(typeOfAccount, accountId, ownerFirstName, ownerSecondName, sum, initialBonuses);

                accounts.Add(account);
                accountRepository.AddAccount(account.ToBankAccount());

                return accountId;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Something wrong with opening account", e);
            }
        }

        /// <inheritdoc />
        public void DepositMoney(string accountId, decimal sum) =>
            Operation(accountId, sum, DepositOperation);

        /// <inheritdoc />
        public void WithdrawMoney(string accountId, decimal sum) =>
            Operation(accountId, sum, WithdrawOperation);

        /// <inheritdoc />
        public void CloseAccount(string accountId) =>
            Operation(accountId, 1m, CloseOperation);

        /// <inheritdoc />
        public string GetAccountStatus(string accountId)
        {
            if (string.IsNullOrWhiteSpace(accountId))
            {
                throw new ArgumentException(nameof(accountId));
            }

            var account = accounts.FirstOrDefault(ac => ac.Id == accountId);
            if (ReferenceEquals(account, null))
            {
                throw new InvalidOperationException("The specified account isn't found");
            }

            return account.ToString();
        }

        /// <inheritdoc />
        public IEnumerable<Account> GetAccounts()
        {
            if (ReferenceEquals(accounts, null))
            {
                throw new InvalidOperationException("The data base is empty");
            }

            return accounts.ToList();
        }

        /// <inheritdoc />
        public string GetTypeOfAccount(string id)
        {
            var response = from account in accounts
                where account.Id == id
                select account;
            if (ReferenceEquals(response, null))
            {
                throw new ArgumentException($"Account with this ID - {id} don't exist");
            }

            return response.FirstOrDefault().GetType().ToString();
        }

        #endregion 

        #endregion 

        #region private

        private static void CheckInput(string ownerFirstName, string ownerSecondName, decimal sum)
        {
            if (string.IsNullOrWhiteSpace(ownerFirstName))
            {
                throw new ArgumentException(nameof(ownerFirstName));
            }

            if (string.IsNullOrWhiteSpace(ownerSecondName))
            {
                throw new ArgumentException(nameof(ownerSecondName));
            }

            if (sum < 0)
            {
                throw new ArgumentException("Sum must be greater than 0", nameof(sum));
            }
        }

        private static int GetInitialBonuses(AccountsLevel accountsLevel)
        {
            switch (accountsLevel)
            {
                case AccountsLevel.Gold:
                    return 200;
                case AccountsLevel.Platinum:
                    return 300;
                case AccountsLevel.Base:
                default:
                    return 100;
            }
        }

        private static Type GetTypeOfAccount(AccountsLevel accountsLevel)
        {
            switch (accountsLevel)
            {
                case AccountsLevel.Gold:
                    return typeof(GoldAccount);
                case AccountsLevel.Platinum:
                    return typeof(PlatinumAccount);
                case AccountsLevel.Base:
                default:
                    return typeof(BaseAccount);
            }
        }

        private static Account CreateAccount(
            Type accountsLevel, string id, string onwerFirstName, string onwerSecondName, decimal sum, int bonusPoints)
        {
            return (Account)Activator.CreateInstance(accountsLevel, id, onwerFirstName, onwerSecondName, sum, bonusPoints);
        }

        private void Operation(string accountId, decimal sum, Action<Account, decimal> operation)
        {
            if (sum <= 0)
            {
                throw new ArgumentException($"{nameof(sum)} must be greater than 0", nameof(sum));
            }

            var account = accounts.First(ac => ac.Id == accountId);
            if (ReferenceEquals(account, null))
            {
                throw new InvalidOperationException("The specified account isn't found");
            }

            try
            {
                operation(account, sum);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(nameof(e));
            }
        }

        private void DepositOperation(Account account, decimal sum)
        {
            account.DepositMoney(sum);
            accountRepository.UpdateAccount(account.ToBankAccount());
        }

        private void WithdrawOperation(Account account, decimal sum)
        {
            account.WithdrawMoney(sum);
            accountRepository.UpdateAccount(account.ToBankAccount());
        }

        private void CloseOperation(Account account, decimal sum)
        {
            accountRepository.RemoveAccount(account.ToBankAccount());
            accounts.Remove(account);
        }

        #endregion 
    }
}
