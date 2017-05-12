using System;
using TrekSurfing.Web.DAL.Interfaces.Repositories;

namespace TrekSurfing.Web.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ITrekEventRepository TrekEvents { get; }
        IReviewRepository Reviews { get; }
        INotificationRepository Notifications { get; }
        int Complete();
    }
}