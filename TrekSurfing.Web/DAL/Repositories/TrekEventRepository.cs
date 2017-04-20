using TrekSurfing.Web.DAL.Interfaces.Repositories;
using TrekSurfing.Web.Models;

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
    }
}