using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrekSurfing.Web.Controllers;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TrekSurfing.Web.Models;

namespace TrekSurfing.Web.Tests.Controllers
{
    [TestClass]
    public class ManageControllerTest
    {
        private ManageController _controller;
        private Mock<ApplicationSignInManager> _signInManager;
        private Mock<ApplicationUserManager> _userManager;

        [TestMethod]
        public async void Index()
        {
            // Arrange
            var _expectedPhoneNumber = "1230498";
            _signInManager = new Mock<ApplicationSignInManager>();
            _userManager = new Mock<ApplicationUserManager>();
            _userManager.Setup(x => x.GetPhoneNumberAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<string>(_expectedPhoneNumber));

            // Act
            var actualResult = (await _controller.Index(null)) as ViewResult;

            // Assert
            Assert.AreEqual((actualResult.Model as IndexViewModel).PhoneNumber , _expectedPhoneNumber);
        }
    }
}
