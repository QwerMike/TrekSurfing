using System.Collections.Generic;
using TrekSurfing.Web.Models;

namespace TrekSurfing.Web.DAL.Interfaces.Repositories
{
    public interface ITrekEventRepository : IRepository<TrekEvent>
    {
        byte[] GetImageFor(int id);
        IEnumerable<TrekEvent> GetAllConfirmed();
        IEnumerable<TrekEvent> GetAllNotConfirmed();
        IEnumerable<Review> GetReviews(int id);
        double GetAverageScore(int id);
        void ChangeConfirmation(int id, bool confirmed);
    }
}
