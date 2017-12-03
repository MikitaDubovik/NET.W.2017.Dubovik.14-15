using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Interface.DTO
{
    public class BankAccount
    {
        public string AccountType { get; set; }

        [Key]
        public string Id { get; set; }

        public string OwnerFirstName { get; set; }

        public string OwnerSecondName { get; set; }

        public decimal CurrentSum { get; set; }

        public int BonusPoints { get; set; }

        protected int BonusValue { get; set; }
    }
}
