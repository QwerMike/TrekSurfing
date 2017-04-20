using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using TrekSurfing.Web.Models;

namespace TrekSurfing.Web.DAL
{
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

        public DbSet<TrekEvent> TrekEvents { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}