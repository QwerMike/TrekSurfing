using System.Web.Mvc;
using TrekSurfing.Web.Models;
using System.Data.Entity;
using System.Linq;

namespace TrekSurfing.Web.Controllers
{
    public class EventController : Controller
    {
        // GET: View
        public ActionResult ViewAllEvents()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                ViewBag.Events = context.TrekEvents.Include(_ => _.Owner).ToList<TrekEvent>();
            }
            return View();
        }
    }
}