using System.Web.Mvc;
using TrekSurfing.Web.Models;
using Microsoft.AspNet.Identity;
using TrekSurfing.Web.DAL.Interfaces;
using System.Web;
using System;
using System.IO;
using System.Net;

namespace TrekSurfing.Web.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private IUnitOfWork unitOfWork;

        public EventController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [AllowAnonymous]
        public ActionResult ViewAllEvents()
        {
            ViewBag.Events = unitOfWork.TrekEvents.GetAllConfirmed();
            return View();
        }

        [AllowAnonymous]
        public ActionResult ViewEvent(int id)
        {
            TrekEvent trekEvent = unitOfWork.TrekEvents.Get(id);
            if (!trekEvent.Confirmed) return HttpNotFound();
            return View(trekEvent);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult ViewNotConfirmedEvents()
        {
            ViewBag.Events = unitOfWork.TrekEvents.GetAllNotConfirmed();
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EventCreationModel model)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files.Count != 0 ? Request.Files.Get(0) : null;
                TrekEvent trekEvent = new TrekEvent
                {
                    Created = DateTime.Now,
                    Name = model.Name,
                    OwnerId = System.Web.HttpContext.Current.User.Identity.GetUserId(),
                    Starts = model.Starts,
                    Ends = model.Starts,
                    Route = model.Route,
                    Image = file!=null ? ConvertToBytes(file) : null,
                    Description = model.Description,
                    Confirmed = false
                };
                unitOfWork.TrekEvents.Add(trekEvent);
                unitOfWork.Complete();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public static byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }
        
        [AllowAnonymous]
        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = unitOfWork.TrekEvents.GetImageFor(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult DeleteEvent(int id)
        {
            TrekEvent deletedEvent = unitOfWork.TrekEvents.Get(id);

            if (this.User.Identity.GetUserId() != deletedEvent.Owner.Id)
                throw new Exception("You can't delete other's events");

            unitOfWork.TrekEvents.Remove(deletedEvent);
            unitOfWork.Complete();
            TempData["message"] = string.Format("{0} was deleted!", deletedEvent.Name);
            return RedirectToAction("MyProfile", "User");
        }
        
        [Authorize(Roles = "Administrator")]
        public ActionResult ChangeEventConfirmation(int id, bool confirmed)
        {
            unitOfWork.TrekEvents.ChangeConfirmation(id, confirmed);
            return RedirectToAction("ViewNotConfirmedEvents", "Event");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var trekEvent = unitOfWork.TrekEvents.Get(id ?? -1);
            if (trekEvent == null || !trekEvent.Confirmed)
            {
                return HttpNotFound();
            }

            return View(trekEvent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string img, TrekEvent trekEvent)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files.Count != 0 ? Request.Files.Get(0) : null;
                var ev = unitOfWork.TrekEvents.Get(trekEvent.Id);
                ev.Name = trekEvent.Name;
                ev.Starts = trekEvent.Starts;
                ev.Ends = trekEvent.Ends;
                ev.Description = trekEvent.Description;
                ev.Image = file != null ? ConvertToBytes(file) : null;
                ev.Route = trekEvent.Route;
                unitOfWork.Complete();
                return RedirectToAction("MyProfile", "User");
            }

            return View(trekEvent);
        }
    }
}