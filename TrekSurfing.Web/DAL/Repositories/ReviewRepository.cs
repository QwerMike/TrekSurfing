using TrekSurfing.Web.DAL.Interfaces.Repositories;
using TrekSurfing.Web.Models;

namespace TrekSurfing.Web.DAL.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public ApplicationDbContext AppDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}