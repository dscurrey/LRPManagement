using System;
using System.Net.Cache;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using LRPManagement.Controllers;
using LRPManagement.Data.Bonds;
using LRPManagement.Data.Characters;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;

namespace LRPManagement.Tests.Controllers
{
    [TestClass]
    public class BondsControllerTests
    {

        private Mock<HttpMessageHandler> CreateHttpMock(HttpResponseMessage expected)
        {
            var mock = new Mock<HttpMessageHandler>();
            mock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expected)
                .Verifiable();
            return mock;
        }

        [TestMethod]
        public void IndexTest()
        {
            throw new NotImplementedException();
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