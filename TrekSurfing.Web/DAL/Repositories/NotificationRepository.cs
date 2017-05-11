using TrekSurfing.Web.DAL.Interfaces.Repositories;
using TrekSurfing.Web.Models;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace TrekSurfing.Web.DAL.Repositories
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        public NotificationRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public ApplicationDbContext AppDbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public new Notification Get(int id)
        {
            return AppDbContext.Set<Notification>().Include(_ => _.Receiver).SingleOrDefault(_ => _.Id == id);
        }

        public new IEnumerable<Notification> GetAll()
        {
            return AppDbContext.Set<Notification>().Include(_ => _.Receiver);
        }
    }
}