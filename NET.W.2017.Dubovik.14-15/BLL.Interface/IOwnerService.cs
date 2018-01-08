using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Owners;

namespace BLL.Interface
{
    public interface IOwnerService
    {
        /// <summary>
        /// Creates new owner by using his email and password
        /// </summary>
        /// <param name="email">Owner's email</param>
        /// <param name="password">Owner's password</param>
        void RegisterOwner(string email, string password);

        /// <summary>
        /// Deletes owner with a specific <paramref name="email"/>
        /// </summary>
        /// <param name="email"></param>
        void DeleteOwner(string email);

        /// <summary>
        /// Returns owner with a specific <paramref name="email"/>
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Owner with a specific <paramref name="email"/></returns>
        Owner GetOwner(string email);

        /// <summary>
        /// Returns informations about all owners
        /// </summary>
        IEnumerable<Owner> GetOwners();
    }
}
