using AccountApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountApi.Context
{
    public class AccountContext: DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options) : base(options) { }
        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<ImageEntity> Images { get; set; }
    }
}
