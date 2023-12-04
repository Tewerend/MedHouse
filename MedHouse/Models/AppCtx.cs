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
        public DbSet<UniqueStorage> UniqueStorages { get; set; }
        public DbSet<MeasuringMedication> MeasuringMedications { get; set; }
    }
}
