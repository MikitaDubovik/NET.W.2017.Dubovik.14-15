using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DTO
{
    public class OwnerAccount
    {
        [Key]
        public string Email { get; set; }

        public string Password { get; set; }
        
        ////public List<BankAccount> Accounts { get; set; }
    }
}
