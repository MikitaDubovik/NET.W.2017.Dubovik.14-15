using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DAL.Interface;
using DAL.Interface.DTO;

namespace DAL
{
    /// <inheritdoc />
    public class BinaryFileRepository : IRepository
    {
        #region private fields

        private readonly string filePath;
        private readonly List<BankAccount> accounts = new List<BankAccount>();

        #endregion

        #region public

        #region Cotr

        /// <summary>
        /// Initializes the instance of repository with the set way to the file
        /// </summary>
        public BinaryFileRepository(string dataFilePath)
        {
            if (string.IsNullOrEmpty(dataFilePath))
            {
                throw new ArgumentException(nameof(dataFilePath));
            }

            filePath = dataFilePath;

            try
            {
                if (File.Exists(filePath))
                {
                    OpenAndReadFromFile(filePath);
                }
            }
            catch (Exception)
            {
                accounts.Clear();
            }
        }

        #endregion

        #region Implementation

        /// <inheritdoc />
        public void AddAccount(BankAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (accounts.Any(account.Equals))
            {
                throw new InvalidOperationException("The entring account already exists");
            }

            try
            {
                AppendAccountToFile(account);
                accounts.Add(account);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Something wrong with adding", e);
            }
        }

        /// <inheritdoc />
        public BankAccount GetAccount(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException(nameof(id));
            }

            return accounts.FirstOrDefault(account => account.AccountId == id);
        }

        /// <inheritdoc />
        public void RemoveAccount(BankAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (!accounts.Any(account.Equals))
            {
                throw new InvalidOperationException("Account does not exists");
            }

            try
            {
                accounts.Remove(account);
                WriteAccountsToFile(accounts);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Something wrong with removing", e);
            }
        }

        /// <inheritdoc />
        public void UpdateAccount(BankAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (accounts.All(dtoAccount => string.Compare(dtoAccount.AccountId, account.AccountId, StringComparison.Ordinal) != 0))
            {
                throw new ArgumentException("Account does not exists");
            }

            try
            {
                accounts.Remove(account);
                accounts.Add(account);
                WriteAccountsToFile(accounts);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Something wrong with updating", e);
            }
        }
        
        /// <inheritdoc />
        public IEnumerable<BankAccount> GetAccounts() => accounts.ToArray();

        #endregion

        #endregion 

        #region private

        /// <summary>
        /// Reads and returns account from file
        /// </summary>
        /// <param name="binaryReader">Binary reader</param>
        /// <returns>Account</returns>
        private static BankAccount ReadAccountFromFile(BinaryReader binaryReader)
        {
            string typeName = binaryReader.ReadString();
            var accountType = Type.GetType(typeName);
            string id = binaryReader.ReadString();
            string ownerFirstName = binaryReader.ReadString();
            string ownerSecondName = binaryReader.ReadString();
            decimal sum = binaryReader.ReadDecimal();
            int bonusPoints = binaryReader.ReadInt32();

            return CreateAccount(accountType, id, ownerFirstName, ownerSecondName, sum, bonusPoints);
        }

        /// <summary>
        /// Writes account to file
        /// </summary>
        /// <param name="binaryWriter">Binary writer</param>
        /// <param name="account">Account which you want to write</param>
        private static void WriteAccountToFile(BinaryWriter binaryWriter, BankAccount account)
        {
            binaryWriter.Write(account.GetType().ToString());
            binaryWriter.Write(account.AccountId);
            binaryWriter.Write(account.OwnerFirstName);
            binaryWriter.Write(account.OwnerSecondName);
            binaryWriter.Write(account.CurrentSum);
            binaryWriter.Write(account.BonusPoints);
        }

        private static BankAccount CreateAccount(Type accountType, string id, string onwerFirstName, string onwerSecondName, decimal sum, int bonusPoints)
        {
            return Activator.CreateInstance(accountType, id, onwerFirstName, onwerSecondName, sum, bonusPoints) as BankAccount;
        }

        private void OpenAndReadFromFile(string otherFilePath)
        {
            using (var binaryReader = new BinaryReader(File.Open(otherFilePath, FileMode.Open, FileAccess.Read, FileShare.Read), Encoding.UTF8, false))
            {
                while (binaryReader.PeekChar() > -1)
                {
                    var account = ReadAccountFromFile(binaryReader);
                    accounts.Add(account);
                }
            }
        }

        private void AppendAccountToFile(BankAccount account)
        {
            using (var binaryWriter = new BinaryWriter(File.Open(filePath, FileMode.Append, FileAccess.Write, FileShare.None), Encoding.UTF8, false))
            {
                WriteAccountToFile(binaryWriter, account);
            }
        }

        private void WriteAccountsToFile(IEnumerable<BankAccount> otherAccounts)
        {
            using (var binaryWriter = new BinaryWriter(File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.None), Encoding.UTF8, false))
            {
                foreach (var account in otherAccounts)
                {
                    WriteAccountToFile(binaryWriter, account);
                }
            }
        }

        #endregion
    }
}
