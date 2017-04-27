using System.Web.Mvc;
using TrekSurfing.Web.Models;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using TrekSurfing.Web.DAL;
using Microsoft.AspNet.Identity;
using TrekSurfing.Web.DAL.Interfaces;
using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace TrekSurfing.Web.Controllers
{
    public class UserController : Controller
    {
        private IUnitOfWork unitOfWork { get; set; }
        private ApplicationUserManager _userManager;

        public UserController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult MyProfile()
        {
            string userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            return View(getProfileById(userId));
        }

        // GET: User/ViewProfile/1
        public ActionResult ViewProfile(string id)
        {
            ProfileViewModel profile = getProfileById(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // GET: User/EditProfile/1
        public ActionResult EditProfile(string id = "0")
        {
            ProfileViewModel profile = getProfileById(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: User/EditProfile/1
        [HttpPost]
        public ActionResult EditProfile(ProfileViewModel profile)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                if (ModelState.IsValid)
                {
                    var user = context.Users.Find(profile.Id);

                    user.UserName = profile.UserName;
                    user.Email = profile.Email;
                    user.PhoneNumber = profile.PhoneNumber;

                    context.Entry(user).State = EntityState.Modified;
                    context.SaveChanges();

                    return RedirectToAction("ViewProfile", new { id = profile.Id });
                }
                else return View(profile);
            }
        }

        // вчився видаляти
        [HttpPost]
        public ActionResult DeleteProfile(string id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                ApplicationUser user = context.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                context.Users.Remove(user);
                context.SaveChanges();
                return RedirectToAction("ViewProfile", new { id = id });
            }

        }

        private ProfileViewModel getProfileById(string id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                ProfileViewModel profile = new ProfileViewModel();
                var data = UserManager.FindById(id);
                if (data == null) return null; 

                IEnumerable<TrekEvent> events = unitOfWork.TrekEvents.Find(trekEvent => trekEvent.OwnerId.Equals(profile.Id));
                return new ProfileViewModel {
                    Id = id,
                    UserName = data.UserName,
                    Email = data.Email,
                    PhoneNumber = data.PhoneNumber,
                    FirstName = data.FirstName,
                    SecondName = data.SecondName,
                    About = data.About,
                    TrekEvents = events != null ? events : new LinkedList<TrekEvent>()
                };
            }
        }
    }
}