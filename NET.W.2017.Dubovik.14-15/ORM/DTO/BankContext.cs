using System.Data.Entity;
using DAL.Interface.DTO;

namespace ORM.DTO
{
    public class BankContext : DbContext
    {
        public BankContext() : base("Accounts")
        {
        }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<OwnerAccount> OwnerAccount { get; set; }
    }
}
