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
            TrekEvent prod = new TrekEvent { Name = "Test", Starts = DateTime.Now, Ends = DateTime.Now, Route= "testRoute", OwnerId = "1" };

        }
    }
}
