using System.Collections.Generic;
using BLL.Interface.Accounts;

namespace NET.W._2017.Dubovik._14_15.AccountService
{
    /// <summary>
    /// The interface describing work of account's service
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Opens base account
        /// </summary>
        /// <param name="onwerFirstName">Account's holder name</param>
        /// <param name="onwerSecondName">Surname of account's holder</param>
        /// <param name="sum">The initial sum on the account</param>
        /// <returns>Account id</returns>
        string OpenAccount(string onwerFirstName, string onwerSecondName, decimal sum);

        /// <summary>
        /// Opens base account
        /// </summary>
        /// <param name="accountType">Type of account</param>
        /// <param name="onwerFirstName">Account's holder name</param>
        /// <param name="onwerSecondName">Surname of account's holder</param>
        /// <param name="sum">The initial sum on the account</param>
        /// <returns>Account id</returns>
        string OpenAccount(AccountsLevel accountType, string onwerFirstName, string onwerSecondName, decimal sum);

        /// <summary>
        /// Deposit money to account
        /// </summary>
        /// <param name="accountId">Account id</param>
        /// <param name="sum">The added sum</param>
        void DepositMoney(string accountId, decimal sum);

        /// <summary>
        /// Withdraw money from account
        /// </summary>
        /// <param name="accountId">Account id</param>
        /// <param name="sum">The written-off sum</param>
        void WithdrawMoney(string accountId, decimal sum);

        /// <summary>
        /// Closes an account with a specific <paramref name="accountId"/>
        /// </summary>
        /// <param name="accountId">Account id</param>
        void CloseAccount(string accountId);

        /// <summary>
        /// Returns information about an account with a specific <paramref name="accountId"/>
        /// </summary>
        /// <param name="accountId">Account id</param>
        /// <returns>Information about an account</returns>
        string GetAccountStatus(string accountId);

        /// <summary>
        /// Returns informations about all accounts
        /// </summary>
        IEnumerable<Account> GetAccounts();

        /// <summary>
        /// Returns type of account
        /// </summary>
        /// <param name="id">Account's id</param>
        /// <returns></returns>
        string GetTypeOfAccount(string id);
    }
}
