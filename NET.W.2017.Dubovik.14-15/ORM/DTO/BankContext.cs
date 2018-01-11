using System.Data.Entity;
using DAL.Interface.DTO;

namespace ORM.DTO
{
    public class BankContext : DbContext
    {
        public BankContext() : base("Bank")
        {
        }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<OwnerAccount> OwnerAccounts { get; set; }
    }
}
