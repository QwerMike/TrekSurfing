using System.Web.Mvc;
using TrekSurfing.Web.Models;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace TrekSurfing.Web.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        // GET: User/1
        public ActionResult ViewUser(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                ProfileViewModel profile = new ProfileViewModel();
                var data = (from User in context.Users where id.ToString() == User.Id select new { User.UserName, User.Email, User.PhoneNumber}).ToArray();
                profile.UserName = data[0].UserName;
                profile.Email = data[0].Email;
                profile.PhoneNumber = data[0].PhoneNumber;
                return View(profile);
            }
            
        }
    }
}