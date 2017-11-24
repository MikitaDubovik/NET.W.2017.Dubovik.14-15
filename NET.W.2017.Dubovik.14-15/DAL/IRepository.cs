using System;
using System.Collections.Generic;
using NET.W._2017.Dubovik._14_15;

namespace DAL
{
    namespace DAL
    {
        /// <summary>
        /// Interface describing repository contract.
        /// </summary>
        public interface IRepository
        {
            /// <summary>
            /// Adds <paramref name="account"/> to the repository.
            /// </summary>
            /// <param name="account"></param>
            /// <exception cref="RepositoryException">
            /// Thrown when an exception occurred in repository or
            /// <paramref name="account"/> already exists.
            /// </exception>
            void AddAccount(Account account);

            /// <summary>
            /// Returns the account with the specified <paramref name="id"/>.
            /// </summary>
            /// <param name="id">account id</param>
            /// <returns>Account or null if there is no such account in the repository.</returns>
            Account GetAccount(string id);

            /// <summary>
            /// Updates account information.
            /// </summary>
            /// <param name="account">account</param>
            /// <exception cref="RepositoryException">
            /// Thrown when an exception occurred in repository or
            /// <paramref name="account"/> does not exists.
            /// </exception>
            void UpdateAccount(Account account);

            /// <summary>
            /// Removes <param name="account"></param>.
            /// </summary>
            /// <param name="account">account</param>
            /// <exception cref="RepositoryException">
            /// Thrown when an exception occurred in repository or
            /// <paramref name="account"/> does not exists.
            /// </exception>
            void RemoveAccount(Account account);

            /// <summary>
            /// Returns all accounts stored in the repository.
            /// </summary>
            /// <returns>All accounts contained in the repository.</returns>
            /// <exception cref="RepositoryException">
            /// Thrown when an exception occurred in repository.
            /// </exception>
            IEnumerable<Account> GetAccounts();
        }
    }

}
