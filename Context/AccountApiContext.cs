using AccountApi.Authentication;
using AccountApi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AccountApi.Context
{
    public class AccountApiContext: IdentityDbContext<ApplicationUser>
    {
        public AccountApiContext(DbContextOptions<AccountApiContext> options) : base(options) { }
        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<ImageEntity> Images { get; set; }
    }
}
