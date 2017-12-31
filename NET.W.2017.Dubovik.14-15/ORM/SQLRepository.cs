using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interface;
using DAL.Interface.DTO;

namespace ORM
{
    public class SQLRepository : IRepository
    {
        /// <inheritdoc />
        public void AddAccount(BankAccount account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            using (var db = new BankAccountContext())
            {
                db.BankAccounts.Add(account);
                db.SaveChanges();
            }
        }

        /// <inheritdoc />
        public BankAccount GetAccount(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            using (var db = new BankAccountContext())
            {
                return db.BankAccounts.Find(id);
            }
        }

        /// <inheritdoc />
        public void RemoveAccount(BankAccount account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            using (var db = new BankAccountContext())
            {
                var tempAccount = db.Set<BankAccount>().FirstOrDefault(acc => acc.AccountId == account.AccountId);
                if (ReferenceEquals(tempAccount, null))
                {
                    throw new InvalidOperationException($"Account - {nameof(account)} doesn't exist");
                }

                db.Set<BankAccount>().Remove(tempAccount);
                db.SaveChanges();
            }
        }

        /// <inheritdoc />
        public IEnumerable<BankAccount> GetAccounts()
        {
            var accounts = new List<BankAccount>();
            using (var db = new BankAccountContext())
            {
                accounts.AddRange(db.Set<BankAccount>());
            }

            return accounts;
        }

        /// <inheritdoc />
        public void UpdateAccount(BankAccount account)
        {
            using (var db = new BankAccountContext())
            {
                var temp = GetAccount(account.AccountId);
                if (temp == null)
                {
                    throw new ArgumentNullException($"{nameof(account)} does not exist");
                }

                temp = account;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
