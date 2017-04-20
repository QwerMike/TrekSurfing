using System.Linq;
using System.Web.Mvc;
using TrekSurfing.Web.Models;
using System.Data.Entity;
using TrekSurfing.Web.DAL;

namespace TrekSurfing.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                ViewBag.Events = context.TrekEvents.Include(_ => _.Owner).ToList<TrekEvent>();
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}