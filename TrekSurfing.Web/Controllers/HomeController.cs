using System.Linq;
using System.Web.Mvc;
using TrekSurfing.Web.Models;
using System.Data.Entity;
using TrekSurfing.Web.DAL;
using TrekSurfing.Web.DAL.Interfaces;
using TrekSurfing.Web.DAL.Interfaces.Repositories;

namespace TrekSurfing.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("ViewAllEvents", "Event");
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