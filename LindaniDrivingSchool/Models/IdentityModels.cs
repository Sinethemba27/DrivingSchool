using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LindaniDrivingSchool.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<CarGroup> CarGroups { get; set; }
        public virtual DbSet<CarMake> CarMakes { get; set; }
        public virtual DbSet<CarModel> CarModels { get; set; }
        public virtual DbSet<Fuel> Fuels { get; set; }
        public virtual DbSet<Insurance> Insurances { get; set; }
        public virtual DbSet<Transmission> Transmissions { get; set; }

        public System.Data.Entity.DbSet<LindaniDrivingSchool.Models.CarHiring> CarHirings { get; set; }

        public System.Data.Entity.DbSet<LindaniDrivingSchool.Models.TimeSlots> TimeSlots { get; set; }

        public System.Data.Entity.DbSet<LindaniDrivingSchool.Models.BookingPackage> BookingPackages { get; set; }

        public System.Data.Entity.DbSet<LindaniDrivingSchool.Models.BookingType> BookingTypes { get; set; }
    }
}