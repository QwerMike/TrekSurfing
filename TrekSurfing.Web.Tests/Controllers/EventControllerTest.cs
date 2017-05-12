using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TrekSurfing.Web.Models;
using TrekSurfing.Web.DAL;
using TrekSurfing.Web.DAL.Repositories;
using TrekSurfing.Web.DAL.Interfaces;
using TrekSurfing.Web.DAL.Interfaces.Repositories;
using TrekSurfing.Web.Controllers;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Web.Mvc;

namespace TrekSurfing.Web.Tests.Controllers
{
    [TestClass]
    public class EventControllerTest
    {
        [TestMethod]
        public void DeleteEventTest()
        {
            TrekEvent prod = new TrekEvent { Name = "Test", Starts = DateTime.Now,
                Ends = DateTime.Now, Route = "testRoute", OwnerId = null,
                Owner = new ApplicationUser() { Id = null } };
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            Mock<ITrekEventRepository> trekEventRepository = new Mock<ITrekEventRepository>();
            uow.Setup(m => m.TrekEvents).Returns(trekEventRepository.Object);

            Mock<IIdentity> identityMoq = new Mock<IIdentity>();
            identityMoq.Setup(x => x.Name).Returns("test_name");

            Mock<IPrincipal> user = new Mock<IPrincipal>();
            user.SetupGet(_ => _.Identity).Returns(identityMoq.Object);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User).Returns(user.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext).Returns(contextMock.Object);         

            bool isDeleted = false;
            bool isCompleted = false;
            trekEventRepository.Setup(_ => _.Remove(prod)).Callback(() => isDeleted = true);
            trekEventRepository.Setup(_ => _.Get(prod.Id)).Returns(prod);
            uow.Setup(_ => _.Complete()).Callback(() => isCompleted = true);

            EventController controller = new EventController(uow.Object);

            controller.ControllerContext = controllerContextMock.Object;

            controller.DeleteEvent(prod.Id);
            Assert.IsTrue(isDeleted);
            Assert.IsTrue(isCompleted);
        }

        [TestMethod]
        public void RetrieveImageTest()
        {

        }
    }
}
