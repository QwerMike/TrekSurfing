using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using TrekSurfing.Web.DAL;
using TrekSurfing.Web.Models;

namespace TrekSurfing.Web.Infrastructure
{
    public class ApplicationRoleManager :
        RoleManager<ApplicationRole>, IDisposable
    {
        public ApplicationRoleManager(RoleStore<ApplicationRole> store)
            : base(store)
        {
        }

        public static ApplicationRoleManager Create(
            IdentityFactoryOptions<ApplicationRoleManager> options,
            IOwinContext context)
        {
            return new ApplicationRoleManager(
                new RoleStore<ApplicationRole>(
                    context.Get<ApplicationDbContext>()));
        }
    }
}