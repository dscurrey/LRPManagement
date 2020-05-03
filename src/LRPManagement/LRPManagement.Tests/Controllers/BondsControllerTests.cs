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
        public void DetailsTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void CreateTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void CreateTest1()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void DeleteTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void DeleteConfirmedTest()
        {
            throw new NotImplementedException();
        }
    }
}