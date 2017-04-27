using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TrekSurfing.Web.Infrastructure;
using TrekSurfing.Web.Models;

namespace TrekSurfing.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RoleAdminController : Controller
    {
        public ActionResult Index()
        {
            return View(RoleManager.Roles);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create([Required]string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result 
                    = await RoleManager.CreateAsync(new ApplicationRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(name);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            ApplicationRole role = await RoleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await RoleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("CustomError", result.Errors);
                }
            }
            else
            {
                return View("CustomError", new string[] { "Role Not Found" });
            }
        }

        public async Task<ActionResult> Edit(string id)
        {
            ApplicationRole role = await RoleManager.FindByIdAsync(id);
            string[] memberIDs = role.Users.Select(x => x.UserId).ToArray();
            IEnumerable<ApplicationUser> members = 
                UserManager.Users
                .Where(x => memberIDs.Any(y => y == x.Id));
            IEnumerable<ApplicationUser> nonMembers = UserManager.Users.Except(members);
            return View(new RoleEditModel
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RoleModificationModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.IdsToAdd ?? new string[] { })
                {
                    result = await UserManager.AddToRoleAsync(userId, model.RoleName);
                    if (!result.Succeeded)
                    {
                        return View("CustomError", result.Errors);
                    }
                }
                foreach (string userId in model.IdsToDelete ?? new string[] { })
                {
                    result = await UserManager.RemoveFromRoleAsync(userId,
                    model.RoleName);
                    if (!result.Succeeded)
                    {
                        return View("CustomError", result.Errors);
                    }
                }
                return RedirectToAction("Index");
            }
            return View("CustomError", new string[] { "Role Not Found" });
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>();
            }
        }

        private ApplicationRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationRoleManager>();
            }
        }
    }

    public static class IdentityHelpers
    {
        public static MvcHtmlString GetUserEmail(this HtmlHelper html, string id)
        {
            ApplicationUserManager mgr = 
                HttpContext.Current.GetOwinContext()
                .GetUserManager<ApplicationUserManager>();

            return new MvcHtmlString(mgr.FindByIdAsync(id).Result.Email);
        }
    }
}