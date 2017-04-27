using TrekSurfing.Web.DAL.Interfaces.Repositories;
using TrekSurfing.Web.Models;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace TrekSurfing.Web.DAL.Repositories
{
    public class TrekEventRepository : Repository<TrekEvent>, ITrekEventRepository
    {
        public TrekEventRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public ApplicationDbContext AppDbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public new TrekEvent Get(int id)
        {
            return AppDbContext.Set<TrekEvent>()
                .Include(_ => _.Owner)
                .SingleOrDefault(_ => _.Id == id);
        }

        public new IEnumerable<TrekEvent> GetAll()
        {
            return AppDbContext.Set<TrekEvent>()
                .Include(_ => _.Owner);
        }

        public void SaveProduct(TrekEvent trekEvent)
        {
            TrekEvent dbEntry = AppDbContext.Set<TrekEvent>().Find(trekEvent.Id);
            if (dbEntry != null)
            {
                dbEntry.Name = trekEvent.Name;
                dbEntry.Description = trekEvent.Description;
                dbEntry.OwnerId = trekEvent.OwnerId;
                dbEntry.Created = trekEvent.Created;
                dbEntry.Image = trekEvent.Image;
                dbEntry.Starts = trekEvent.Starts;
                dbEntry.Ends = trekEvent.Starts;
                dbEntry.Route = trekEvent.Route;
            }
        }

        public byte[] GetImageFor(int id)
        {
            var q = from temp in AppDbContext.TrekEvents where temp.Id == id select temp.Image;
            byte[] cover = q.First();
            return cover;
        }
    }
}