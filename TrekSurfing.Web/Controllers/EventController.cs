using System.Web.Mvc;
using TrekSurfing.Web.Models;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

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

        public ActionResult ViewEvent(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                ViewBag.Event = new LinkedList<TrekEvent>();
            }
            return View();
        }
    }
}