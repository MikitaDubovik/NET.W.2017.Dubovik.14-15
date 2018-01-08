using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DTO
{
    public class OwnerAccount
    {
        public int OwnerId { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Email { get; set; }

        public List<BankAccount> Accounts { get; set; }
    }
}
