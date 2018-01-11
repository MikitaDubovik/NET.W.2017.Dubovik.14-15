using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interface;
using DAL.Interface.DTO;
using ORM.DTO;

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

            using (var db = new BankContext())
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

            using (var db = new BankContext())
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

            using (var db = new BankContext())
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
            using (var db = new BankContext())
            {
                accounts.AddRange(db.Set<BankAccount>());
            }

            return accounts;
        }

        /// <inheritdoc />
        public void UpdateAccount(BankAccount account)
        {
            using (var db = new BankContext())
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

        public IEnumerable<OwnerAccount> GetOwners()
        {
            var owners = new List<OwnerAccount>();
            using (var db = new BankContext())
            {
                owners.AddRange(db.Set<OwnerAccount>());
            }

            return owners;
        }

        public void AddOwner(OwnerAccount owner)
        {
            if (owner == null)
            {
                throw new ArgumentNullException(nameof(owner));
            }

            using (var db = new BankContext())
            {
                db.OwnerAccounts.Add(owner);
                db.SaveChanges();
            }
        }
    }
}
