using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRPManagement.Controllers;
using LRPManagement.Data.Bonds;
using LRPManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LRPManagement.Tests.Controllers
{
    [TestClass]
    public class BondsControllerTests
    {
        private static class TestData
        {
            public static List<Bond> Bonds()
            {
                return new List<Bond>
                {
                    new Bond {Id = 1, CharacterId = 1, ItemId = 1},
                    new Bond {Id = 2, CharacterId = 1, ItemId = 22},
                    new Bond {Id = 3, CharacterId = 2, ItemId = 4}
                };
            }
        }

        [TestMethod]
        public async Task IndexTest()
        {
            // Arrange
            var mockBondRepo = new Mock<IBondRepository>();
            var mockBondServ = new Mock<IBondService>();

            mockBondRepo.Setup(repo => repo.GetAll()).ReturnsAsync(TestData.Bonds());

            var controller = new BondsController(mockBondRepo.Object, null, null, mockBondServ.Object);

            // Act
            var result = await controller.Index();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            var model = viewResult.ViewData.Model;
            Assert.IsNotNull(model);
            var modelList = model as IEnumerable<Bond>;
            Assert.AreEqual(TestData.Bonds().Count, modelList.Count());
        }

        [TestMethod]
        public async Task DetailsTest()
        {
            // Arrange
            var mockBondRepo = new Mock<IBondRepository>();
            var mockBondServ = new Mock<IBondService>();

            var charId = 2;

            mockBondRepo.Setup(repo => repo.Get(charId)).ReturnsAsync(TestData.Bonds().FirstOrDefault(b => b.Id == charId));

            var controller = new BondsController(mockBondRepo.Object, null, null, mockBondServ.Object);

            // Act
            var result = await controller.Details(charId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            var model = viewResult.ViewData.Model;
            Assert.IsNotNull(model);
            
            var bond = model as Bond;
            var testBond = TestData.Bonds().FirstOrDefault(b => b.Id == charId);
            Assert.AreEqual(testBond.Id, bond.Id);
            Assert.AreEqual(testBond.ItemId, bond.ItemId);
            Assert.AreEqual(testBond.CharacterId, bond.CharacterId);
        }

        [TestMethod]
        public async Task DeleteTest()
        {
            // Arrange
            var mockBondRepo = new Mock<IBondRepository>();
            var mockBondServ = new Mock<IBondService>();
            var charId = 1;
            mockBondRepo.Setup(repo => repo.Get(charId)).ReturnsAsync(TestData.Bonds().FirstOrDefault(b => b.Id == charId));
            var controller = new BondsController(mockBondRepo.Object, null, null, mockBondServ.Object);

            // Act
            var result = await controller.Delete(charId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.ViewData.Model;
            Assert.IsNotNull(model);
            var bond = model as Bond;
            Assert.IsNotNull(bond);
            Assert.AreEqual(charId, bond.Id);
        }

        [TestMethod]
        public async Task DeleteConfirmedTest()
        {
            // Arrange
            var mockBondRepo = new Mock<IBondRepository>();
            var mockBondServ = new Mock<IBondService>();
            var charId = 1;
            mockBondRepo.Setup(repo => repo.Get(charId)).ReturnsAsync(TestData.Bonds().FirstOrDefault(b => b.Id == charId));
            var controller = new BondsController(mockBondRepo.Object, null, null, mockBondServ.Object);

            // Act
            var result = await controller.DeleteConfirmed(charId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}