using System.Collections.Generic;
using TrekSurfing.Web.Models;

namespace TrekSurfing.Web.DAL.Interfaces.Repositories
{
    public interface ITrekEventRepository : IRepository<TrekEvent>
    {
        byte[] GetImageFor(int id);
        IEnumerable<TrekEvent> GetAllConfirmed();
        IEnumerable<TrekEvent> GetAllNotConfirmed();
        void ChangeConfirmation(int id, bool confirmed);
    }
}
