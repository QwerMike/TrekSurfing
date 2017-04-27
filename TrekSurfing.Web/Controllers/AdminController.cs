using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TrekSurfing.Web.Models;

namespace TrekSurfing.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View(UserManager.Users);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
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
                return View("CustomError", new string[] { "User Not Found" });
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
    }
}