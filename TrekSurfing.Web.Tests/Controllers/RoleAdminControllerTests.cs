using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrekSurfing.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrekSurfing.Web.Models;
using Moq;
using Microsoft.AspNet.Identity;
using TrekSurfing.Web.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;

namespace TrekSurfing.Web.Controllers.Tests
{
    [TestClass()]
    public class RoleAdminControllerTests
    {
        private List<ApplicationRole> roles = new List<ApplicationRole>()
        {
            new ApplicationRole() { Id = "id1", Name= "User"}
        };

        private Mock<ApplicationRoleManager> roleManager =
                new Mock<ApplicationRoleManager>(
                    new Mock<RoleStore<ApplicationRole>>().Object);

        public RoleAdminControllerTests()
        {
            roleManager.Setup(_ => _.Roles).Returns(roles.AsQueryable());
            roleManager.Setup(_ => _.FindByIdAsync("id1"))
                .Returns(Task.Run(() => roles[0]));
        }

        [TestMethod()]
        public void IndexTest()
        {
            var controller = new RoleAdminController();
            controller.GetType().GetField("roleManager",
                System.Reflection.BindingFlags.SetField
                | System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.Instance)
                .SetValue(controller, roleManager.Object);

            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
            var actualRoles = result.Model as IEnumerable<ApplicationRole>;
            Assert.IsTrue(actualRoles.SequenceEqual(roles));
        }

        [TestMethod()]
        public void CreateTest()
        {
            var controller = new RoleAdminController();
            var result = controller.Create() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}