using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TrekSurfing.Web.DAL.Interfaces;
using TrekSurfing.Web.Models;

namespace TrekSurfing.Web.Controllers
{
    public class NotificationController : Controller
    {
        private IUnitOfWork unitOfWork;

        public NotificationController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ActionResult CurrentUserNotifications()
        {
            string receiverId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            IEnumerable<Notification> notifications = unitOfWork.Notifications.GetAll().AsEnumerable();
            ViewBag.Notifications = notifications;
            ViewBag.Number = notifications.Count();
            return PartialView();
        }
    }
}