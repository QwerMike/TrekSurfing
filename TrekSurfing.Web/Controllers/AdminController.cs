using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TrekSurfing.Web.DAL.Interfaces;
using TrekSurfing.Web.Models;

namespace TrekSurfing.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private IUnitOfWork unitOfWork;
        private ApplicationUserManager userManager;

        public AdminController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public AdminController(IUnitOfWork unitOfWork, ApplicationUserManager userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

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
        
        public ActionResult ViewEvent(int id)
        {
            TrekEvent trekEvent = unitOfWork.TrekEvents.Get(id);
            if (trekEvent == null) return HttpNotFound();
            return View("ViewEvent", trekEvent);
        }

        [HttpPost]
        public ActionResult DeleteEvent(int id)
        {
            TrekEvent deletedEvent = unitOfWork.TrekEvents.Get(id);
            unitOfWork.TrekEvents.Remove(deletedEvent);
            unitOfWork.Complete();
            TempData["message"] = string.Format("{0} was deleted!", deletedEvent.Name);
            return RedirectToAction("ViewAllEvents", "Event");
        }

        private ApplicationUserManager UserManager
        {
            get
            {
                return userManager ?? HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>();
            }
        }
    }
}