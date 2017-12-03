using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface;
using DAL.Interface.DTO;

namespace DAL
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
                db.BankAccounts.Remove(account);
                db.SaveChanges();
            }
        }

        /// <inheritdoc />
        public IEnumerable<BankAccount> GetAccounts()
        {
            using (var db = new BankAccountContext())
            {
                return db.BankAccounts.AsEnumerable();
            }
        }

        /// <inheritdoc />
        public void UpdateAccount(BankAccount account)
        {
            using (var db = new BankAccountContext())
            {
                var temp = GetAccount(account.Id);
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
