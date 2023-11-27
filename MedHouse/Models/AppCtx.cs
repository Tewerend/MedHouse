using MedHouse.Models.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MedHouse.Models
{
    public class AppCtx : IdentityDbContext<User>
    {
        public AppCtx(DbContextOptions<AppCtx> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Provider> Providers { get; set; }

        internal static Task<string?> ToListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
