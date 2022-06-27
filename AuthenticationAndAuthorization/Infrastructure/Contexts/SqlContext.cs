using AuthenticationAndAuthorization.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAndAuthorization.Infrastructure.Contexts
{
    public class SqlContext : IdentityDbContext<AppUser>
    {
        public SqlContext()
        {

        }
        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {

        }
    }
}
