using System;
using TrekSurfing.Web.DAL.Interfaces;
using TrekSurfing.Web.DAL.Interfaces.Repositories;
using TrekSurfing.Web.DAL.Repositories;

namespace TrekSurfing.Web.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext context;

        private ITrekEventRepository trekEvents;

        private IReviewRepository reviews;

        private INotificationRepository notifications;

        public UnitOfWork(
            ApplicationDbContext context)
        {
            this.context = context;
            trekEvents = new TrekEventRepository(context);
            reviews = new ReviewRepository(context);
            notifications = new NotificationRepository(context);
        }

        public UnitOfWork(
            ApplicationDbContext context,
            ITrekEventRepository trekEventRepository,
            IReviewRepository reviewRepository,
            INotificationRepository notificationRepository)
        {
            this.context = context;
            trekEvents = new TrekEventRepository(context);
            reviews = new ReviewRepository(context);
            notifications = new NotificationRepository(context);
        }

        public ITrekEventRepository TrekEvents
        {
            get { return trekEvents; }
        }

        public IReviewRepository Reviews
        {
            get { return reviews; }
        }        

        public INotificationRepository Notifications
        {
            get { return notifications; }
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