using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrekSurfing.Web.DAL.Interfaces;
using TrekSurfing.Web.DAL.Interfaces.Repositories;
using TrekSurfing.Web.DAL.Repositories;

namespace TrekSurfing.Web.DAL
{
    class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext context;
        private ITrekEventRepository trekEvents;
        private IReviewRepository reviews;

        public UnitOfWork(
            ApplicationDbContext context)
        {
            this.context = context;
            //this.trekEvents = trekEvents;
            //this.reviews = reviews; 
            
            trekEvents = new TrekEventRepository(context);
            reviews = new ReviewRepository(context);
        }

        public ITrekEventRepository TrekEvents
        {
            get { return trekEvents; }
        }

        public IReviewRepository Reviews
        {
            get { return reviews; }
        }        

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}