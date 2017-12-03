using System.Data.Entity;

namespace DAL.Interface.DTO
{
    public class BankAccountContext : DbContext
    {
        public BankAccountContext() : base("Accounts")
        {
        }

        public DbSet<BankAccount> BankAccounts { get; set; }
    }
}
