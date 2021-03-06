﻿using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interface;
using BLL.Interface.Owners;
using DAL.Interface;
using NET.W._2017.Dubovik._14_15.Mappers;

namespace NET.W._2017.Dubovik._14_15.OwnerService
{
    public class OwnerService : IOwnerService
    {
        private readonly IRepository accountRepository;
        private readonly List<Owner> owners = new List<Owner>();

        /// <summary>
        /// Initializes the instance of service
        /// </summary>
        /// <param name="otherAccountRepository">Account repository service</param>
        /// <param name="otherAccountIdService">Account ID service</param>
        public OwnerService(IRepository otherAccountRepository)
        {
            if (ReferenceEquals(otherAccountRepository, null))
            {
                throw new ArgumentNullException(nameof(accountRepository));
            }

            accountRepository = otherAccountRepository;

            try
            {
                var dalOwners = accountRepository.GetOwners();
                owners.AddRange(dalOwners.Select(owners => owners.ToBllOwner()));
            }
            catch (Exception)
            {
                owners.Clear();
            }
        }

        /// <inheritdoc />
        public void RegisterOwner(string email, string password)
        {
            CheckInput(email, password);

            try
            {
                var owner = CreateOwner(typeof(Owner), email, password);
                owners.Add(owner);
                accountRepository.AddOwner(owner.ToOwnerAccount());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <inheritdoc />
        public Owner GetOwner(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException(nameof(email));
            }

            var owner = owners.FirstOrDefault(ac => ac.Email == email);

            return owner;
        }

        /// <inheritdoc />
        public IEnumerable<Owner> GetOwners()
        {
            if (ReferenceEquals(owners, null))
            {
                throw new InvalidOperationException("The data base is empty");
            }

            return owners.ToList();
        }

        #region private

        private static Owner CreateOwner(Type owner, string email, string password)
        {
            return (Owner)Activator.CreateInstance(owner, email, password);
        }

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
