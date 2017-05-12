using TrekSurfing.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;
using System.Collections.Generic;
using TrekSurfing.Web.DAL.Interfaces;
using TrekSurfing.Web.Models;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Moq.Protected;
using TrekSurfing.Web.DAL.Interfaces.Repositories;

namespace TrekSurfing.Web.Controllers.Tests
{
    [TestClass()]
    public class AdminControllerTests
    {
        private List<ApplicationUser> users = new List<ApplicationUser>()
        {
            new ApplicationUser() { Id = "id1", FirstName = "User1" },
            new ApplicationUser() { Id = "id2", FirstName = "User2" }
        };

        private Mock<ApplicationUserManager> userManager =
                new Mock<ApplicationUserManager>(
                    new Mock<IUserStore<ApplicationUser>>().Object);

        public AdminControllerTests()
        {
            userManager.Setup(_ => _.Users).Returns(users.AsQueryable());
            userManager.Setup(_ => _.FindByIdAsync("id1"))
                .Returns(Task.Run(() => users[0]));
            userManager.Setup(_ => _.FindByIdAsync("id2"))
                .Returns(Task.Run(() => users[1]));
            userManager.Setup(_ => _.FindByIdAsync("badId"))
                .Returns(Task.Run(() => null as ApplicationUser));
        }

        [TestMethod()]
        public void IndexTest()
        {
            AdminController controller = new AdminController(
                null, userManager.Object);

            var result = controller.Index() as ViewResult;
            var expectedUsers = result.Model as IEnumerable<ApplicationUser>;

            Assert.IsNotNull(expectedUsers);
            Assert.IsTrue(expectedUsers.SequenceEqual(users));
        }

        [TestMethod()]
        public void DeleteSucceededTest()
        {
            // invokes protected constructor
            Mock<IdentityResult> identityResult =
                new Mock<IdentityResult>(true);

            userManager.Setup(_ => _.DeleteAsync(users[0]))
                .Returns(Task.Run(() => identityResult.Object));

            AdminController controller = new AdminController(
                null, userManager.Object);
            var result = controller.Delete("id1").Result as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod()]
        public void DeleteNotSucceededTest()
        {
            // invokes protected constructor
            Mock<IdentityResult> identityResult =
                new Mock<IdentityResult>(false);

            userManager.Setup(_ => _.DeleteAsync(users[1]))
                .Returns(Task.Run(() => identityResult.Object));

            AdminController controller = new AdminController(
                null, userManager.Object);
            var result = controller.Delete("id2").Result as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("CustomError", result.ViewName);
            var errors = result.Model as string[];
            Assert.IsNotNull(errors);
            Assert.AreEqual(0, errors.Length);
        }

        [TestMethod()]
        public void DeleteNotFoundUserTest()
        {
            AdminController controller = new AdminController(
                null, userManager.Object);
            var result = controller.Delete("badId").Result as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("CustomError", result.ViewName);
            var errors = result.Model as string[];
            Assert.IsNotNull(errors);
            Assert.AreEqual(1, errors.Length);
            Assert.AreEqual("User Not Found", errors[0]);
        }

        [TestMethod()]
        public void DeleteEventTest()
        {
            var teRepository = new Mock<ITrekEventRepository>();
            var te = new TrekEvent()
            {
                Id = 0,
                Name = "Event"
            };

            bool isRemoved = false;
            teRepository.Setup(_ => _.Get(0)).Returns(te);
            teRepository.Setup(_ => _.Remove(te))
                .Callback(() => isRemoved = true);

            var uow = new Mock<IUnitOfWork>();
            uow.SetupGet(_ => _.TrekEvents).Returns(teRepository.Object);
            
            var controller = new AdminController(uow.Object);
            var result = controller.DeleteEvent(0) as RedirectToRouteResult;

            Assert.AreEqual("Event was deleted!", controller.TempData["message"]);
            Assert.IsTrue(isRemoved);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.RouteValues.Count);
            Assert.AreEqual("ViewAllEvents", result.RouteValues["action"]);
            Assert.AreEqual("Event", result.RouteValues["controller"]);
        }
    }
}
