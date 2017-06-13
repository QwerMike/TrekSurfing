using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrekSurfing.Web.DAL.Interfaces;
using TrekSurfing.Web.Models;

namespace TrekSurfing.Web.Controllers
{
    public class ReviewController : Controller
    {
        private IUnitOfWork unitOfWork;

        public ReviewController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpPost]
        public ActionResult Index(ReviewModel model)
        {
            Review review = new Review {
                AuthorId = User.Identity.GetUserId(),
                TargetId = model.TargetId,
                Created = DateTime.Now,
                Message = model.Message,
                Score = model.Score
            };
            unitOfWork.Reviews.Add(review);
            unitOfWork.Complete();

            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}