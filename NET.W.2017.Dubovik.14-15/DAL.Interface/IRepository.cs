using System.Collections.Generic;
using DAL.Interface.DTO;

namespace DAL.Interface
{
    /// <summary>
    /// Interface for record and data read-out
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Adds <paramref name="account"/> to the repository
        /// </summary>
        /// <param name="account">Account which you want to add</param>
        void AddAccount(BankAccount account);

        /// <summary>
        /// Returns the account with the <paramref name="id"/>
        /// </summary>
        /// <param name="id">Account's id</param>
        /// <returns>Required account or null if there is no such account in the repository</returns>
        BankAccount GetAccount(string id);

        /// <summary>
        /// Removes <param name="account"></param>.
        /// </summary>
        /// <param name="account">Account wich you want to delete</param>
        void RemoveAccount(BankAccount account);

        /// <summary>
        /// Returns all accounts stored in the repository.
        /// </summary>
        /// <returns>All accounts contained in the repository.</returns>
        IEnumerable<BankAccount> GetAccounts();

        /// <summary>
        /// Updates information about account
        /// </summary>
        /// <param name="account">Account which you want to update</param>
        void UpdateAccount(BankAccount account);

        /// <summary>
        /// Returns all owners stored in the repository.
        /// </summary>
        /// <returns>All owners contained in the repository.</returns>
        IEnumerable<OwnerAccount> GetOwners();

        /// <summary>
        /// Adds <paramref name="owner"/> to the repository
        /// </summary>
        /// <param name="owner">Owner which you want to add</param>
        void AddOwner(OwnerAccount owner);
    }
}
