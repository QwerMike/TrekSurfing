using TrekSurfing.Web.DAL.Interfaces.Repositories;
using TrekSurfing.Web.Models;
using System.Data.Entity;
using System.Linq;

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

        public byte[] GetImageFor(int id)
        {
            var q = from temp in AppDbContext.TrekEvents where temp.Id == id select temp.Image;
            byte[] cover = q.First();
            return cover;
        }
    }
}