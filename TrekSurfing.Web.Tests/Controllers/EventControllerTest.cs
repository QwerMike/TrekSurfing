using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TrekSurfing.Web.Models;
using TrekSurfing.Web.DAL;
using TrekSurfing.Web.DAL.Repositories;

namespace TrekSurfing.Web.Tests.Controllers
{
    [TestClass]
    public class EventControllerTest
    {
        [TestMethod]
        public void DeleteEventTest()
        {
            /* ERORRRRRRRRR
            TrekEvent prod = new TrekEvent { Name = "Test", Starts = DateTime.Now, Ends = DateTime.Now, Route= "testRoute", OwnerId = "1" };
            Mock<UnitOfWork> mock = new Mock<UnitOfWork>();
            Mock<TrekEventRepository> trekEventRepository = new Mock<TrekEventRepository>();

            trekEventRepository.Setup(t => t.Add(prod));
            mock.SetupProperty(m => m.TrekEvents).Returns(trekEventRepository.Object);
            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);
            // Act - delete the product
            target.Delete(prod.ProductID);
            // Assert - ensure that the repository delete method was
            // called with the correct Product
            mock.TrekEvents.Add(prod);
            Assert.AreEqual();
            mock.TrekEvents.Delete(prod);
            Assert.AreEqual();
            */
        }
    }
}
