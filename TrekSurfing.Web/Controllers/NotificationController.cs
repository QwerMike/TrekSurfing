using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TrekSurfing.Web.DAL.Interfaces;
using TrekSurfing.Web.Models;

namespace TrekSurfing.Web.Controllers
{
    [Authorize]
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
            IEnumerable<Notification> notifications = unitOfWork.Notifications.GetAll().Where(n => n.ReceiverId.Equals(receiverId)).AsEnumerable();
            ViewBag.Notifications = notifications;
            ViewBag.Number = notifications.Count();
            return PartialView();
        }

        [HttpPost]
        public ActionResult DeleteNotification(int id)
        {
            Notification notification = unitOfWork.Notifications.Get(id);

            if (this.User.Identity.GetUserId() != notification.Receiver.Id)
                throw new Exception("You can't delete other's notifications");

            unitOfWork.Notifications.Remove(notification);
            unitOfWork.Complete();
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }
    }
}