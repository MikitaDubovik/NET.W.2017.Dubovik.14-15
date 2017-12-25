using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Interface.DTO
{
    public class BankAccount
    {
        public string AccountType { get; set; }

        [Key]
        public string AccountId { get; set; }

        public string OwnerFirstName { get; set; }

        public string OwnerSecondName { get; set; }

        public decimal CurrentSum { get; set; }

        public int BonusPoints { get; set; }

        public int BonusValue { get; set; }

       // public OwnerAccount OwnerAccount { get; set; }
    }
}
