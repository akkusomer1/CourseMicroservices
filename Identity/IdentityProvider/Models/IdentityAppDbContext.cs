using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityProvider.Models
{
    public class IdentityAppDbContext:IdentityDbContext<AppUser,IdentityRole,string>
    { 
        public IdentityAppDbContext(DbContextOptions<IdentityAppDbContext> options):base(options) 
        {         
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
