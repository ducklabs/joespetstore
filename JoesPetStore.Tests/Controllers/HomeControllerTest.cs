using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using FluentAssert;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JoesPetStore;
using JoesPetStore.Controllers;

namespace JoesPetStore.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            result.ShouldNotBeNull();
        }
    }
}
