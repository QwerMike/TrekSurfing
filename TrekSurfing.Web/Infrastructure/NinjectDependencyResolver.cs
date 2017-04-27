using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TrekSurfing.Web.DAL;
using TrekSurfing.Web.DAL.Interfaces;
using TrekSurfing.Web.DAL.Interfaces.Repositories;
using TrekSurfing.Web.DAL.Repositories;

namespace TrekSurfing.Web.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument<ApplicationDbContext>(new ApplicationDbContext());
            //kernel.Bind<ITrekEventRepository>().To<TrekEventRepository>();
            //kernel.Bind<IReviewRepository>().To<ReviewRepository>();
        }
    }
}