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
using System;
using System.IO;

namespace TrekSurfing.Web.Controllers
{
    public class EventController : Controller
    {
        private IUnitOfWork unitOfWork;

        public EventController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

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
            TrekEvent trekEvent = unitOfWork.TrekEvents.Get(id);
            return View(trekEvent);
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
                    Description = model.Description
                };
                unitOfWork.TrekEvents.Add(trekEvent);
                unitOfWork.Complete();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        private byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

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
    }
}