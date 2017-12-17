using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BLL.Interface.Accounts;

namespace BLL.Interface.Owners
{
    public class Owner
    {
        private string firstName;
        private string secondName;
        private string email;
        private List<Account> accounts;

        public List<Account> Accounts
        {
            get => accounts;
            set => accounts = value ?? throw new ArgumentNullException($"The value is null");
        }

        /// <summary>
        /// Owner's first name
        /// </summary>
        public string OwnerFirstName
        {
            get => firstName;

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(nameof(OwnerFirstName));
                }

                firstName = value;
            }
        }

        /// <summary>
        /// Owner's second name
        /// </summary>
        public string OwnerSecondName
        {
            get => secondName;

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(nameof(OwnerSecondName));
                }

                secondName = value;
            }
        }

        public string Email
        {
            get => email;

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(nameof(Email));
                }

                var regex = new Regex("([0-9А-Яа-я]{0,})@([A-Za-z]{1,})([A-Za-z]{2,})");

                if (!regex.IsMatch(value))
                {
                    throw new ArgumentException(nameof(Email));
                }
                
                email = value;
            }
        }
    }
}
