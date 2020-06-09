using EntityLayer.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Models
{

    public partial class GrubNowDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public GrubNowDbContext(DbContextOptions<GrubNowDbContext> options)
            : base(options)
        {

        }
        // Db set Of User And Role
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Cuisine> Cuisines { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<OtherLocation> OtherLocations { get; set; }
        public DbSet<DriverWithArea> DriverWithAreas { get; set; }
        public DbSet<Blogs> Blogs { get; set; }

        public DbSet<TokenForResetPassword> TokenForResetPasswords { get; set; }
        public DbSet<VendorWithArea> VendorWithAreas { get; set; }
        public DbSet<VendorWithCuisine> VendorWithCuisines { get; set; }
        public DbSet<Area> Areas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionStringHelper.Connectionstring);
            }
        }

        public GrubNowDbContext()
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AppUser>().ToTable("Users");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            modelBuilder.Entity<AppRole>().ToTable("Roles");
        }
    }
}
