using System.Data.Entity;
using DAL.Interface.DTO;

namespace ORM
{
    public class BankAccountContext : DbContext
    {
        public BankAccountContext() : base("Accounts")
        {
        }

        public DbSet<BankAccount> BankAccounts { get; set; }
       // public DbSet<OwnerAccount> OwnerAccount { get; set; }
    }
}
