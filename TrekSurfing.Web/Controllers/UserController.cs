using System.Web.Mvc;
using TrekSurfing.Web.Models;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using TrekSurfing.Web.DAL;
using Microsoft.AspNet.Identity;
using TrekSurfing.Web.DAL.Interfaces;

namespace TrekSurfing.Web.Controllers
{
    public class UserController : Controller
    {
        private IUnitOfWork unitOfWork { get; set; }

        public UserController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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
                return View(profile);
            }
        }

        // вчився видаляти
        /*[HttpPost]
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

        }*/

        private ProfileViewModel getProfileById(string id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                ProfileViewModel profile = new ProfileViewModel();
                var data1 = (from User in context.Users where id == User.Id select new { User.UserName, User.Email, User.PhoneNumber });
                if (data1 == null)
                {
                    return null;
                }
                var data = data1.First();
                profile.Id = id;
                profile.UserName = data.UserName;
                profile.Email = data.Email;
                profile.PhoneNumber = data.PhoneNumber;
                IEnumerable<TrekEvent> events = unitOfWork.TrekEvents.Find(trekEvent => trekEvent.OwnerId.Equals(profile.Id));
                profile.TrekEvents = events != null ? events : new LinkedList<TrekEvent>();
                return profile;
            }
        }
    }
}