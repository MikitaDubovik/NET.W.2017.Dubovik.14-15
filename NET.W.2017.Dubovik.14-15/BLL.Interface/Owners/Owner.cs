using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using BLL.Interface.Accounts;

namespace BLL.Interface.Owners
{
    public class Owner
    {
        private string email;
        private string password;
       //// private List<Account> accounts;

        ////public List<Account> Accounts
        ////{
        ////    get => accounts;
        ////    set => accounts = value ?? throw new ArgumentNullException($"The value is null");
        ////}

        public Owner(string email, string password)
        {
            CheckInput(email, password);

            Email = email;
            Password = password;
        }

        #region properties

        /// <summary>
        /// Owner's email
        /// </summary>
        public string Email
        {
            get => email;

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(nameof(Email));
                }

                email = value;
            }
        }

        /// <summary>
        /// Owner's password
        /// </summary>
        public string Password
        {
            get => password;

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(nameof(Password));
                }

                password = value;
            }
        }

        #endregion
        
        #region private

        private static void CheckInput(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }
        }

        #endregion
    }
}
