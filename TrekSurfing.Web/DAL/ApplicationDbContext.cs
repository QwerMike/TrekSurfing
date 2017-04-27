using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using TrekSurfing.Web.Infrastructure;
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

        public void PerformInitialSetup(ApplicationDbContext context)
        {
            var userMgr = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleMgr = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));
            string roleName = "Administrator";
            string userName = "Admin";
            string password = "Password!1";
            string email = "admin@mail.com";

            if (!roleMgr.RoleExists(roleName))
            {
                roleMgr.Create(new ApplicationRole(roleName));
            }

            ApplicationUser user = userMgr.FindByName(userName);
            if (user == null)
            {
                userMgr.Create(new ApplicationUser { UserName = userName, Email = email },
                password);
                user = userMgr.FindByName(userName);
            }
        }
    }
}