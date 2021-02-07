using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Hosting;
using TAL.PremiumCalculator.BLL.Interface;
using TAL.PremiumCalculator.BLL.Models;
using TAL.PremiumCalculator.BLL.Service;
using TAL.PremiumCalculator.WebAPI.Controllers;

namespace TAL.PremiumCalculator.WebAPI.Tests.Controllers
{
    [TestClass]
    public class PremiumControllerUnitTest
    {

        [TestMethod]
        public async Task GetOccupations_UnitTest()
        {
            // Arrange
            List<OccupationData> occupations = new List<OccupationData>();
            OccupationData occupationData = new OccupationData();
            occupationData.Id = 1;
            occupationData.OccupationName = "Dentist";
            occupations.Add(occupationData);

            // Interface Mock setup
            var IOccupationBLLMock = new Mock<IOccupationBLL>();
            IOccupationBLLMock.Setup(x => x.getAllOccupations())
                .Returns(Task.FromResult(occupations));


            var IPremiumCalculatorBLLMock = new Mock<IPremiumCalculatorBLL>();

            var controller = new PremiumController(IOccupationBLLMock.Object, IPremiumCalculatorBLLMock.Object);

            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey,
                                              new HttpConfiguration());
            // Act
            var response = await controller.GetOccupations();

            var controllerResponse = response.ExecuteAsync(CancellationToken.None).Result;

            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(controllerResponse.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, controllerResponse.StatusCode);

            List<OccupationData> occupation;
            Assert.IsTrue(controllerResponse.TryGetContentValue(out occupation));
            Assert.AreEqual(1, occupations.Count);

        }

        [TestMethod]
        public async Task GetOccupations_InternalServerError()
        {
            // Arrange
            List<OccupationData> occupations = new List<OccupationData>();
            OccupationData occupationData = new OccupationData();
            occupationData.Id = 1;
            occupationData.OccupationName = "Dentist";
            occupations.Add(occupationData);

            // Interface Mock setup
            var IOccupationBLLMock = new Mock<IOccupationBLL>();
            IOccupationBLLMock.Setup(x => x.getAllOccupations())
               .Throws(new Exception("exception message"));


            var IPremiumCalculatorBLLMock = new Mock<IPremiumCalculatorBLL>();

            var controller = new PremiumController(IOccupationBLLMock.Object, IPremiumCalculatorBLLMock.Object);

            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey,
                                              new HttpConfiguration());
            // Act
            var response = await controller.GetOccupations();

            var controllerResponse = response.ExecuteAsync(CancellationToken.None).Result;

            // Assert
            Assert.IsNotNull(response);
            Assert.IsFalse(controllerResponse.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.InternalServerError, controllerResponse.StatusCode);

        }

        [TestMethod]
        public async Task GetPremiumValue_UnitTest()
        {
            // Arrange
            // Test data
            var premiumParams = new PremiumParametersData();
            premiumParams.Age = 25;
            premiumParams.OccupationId = 1;
            premiumParams.SumInsured = 150000;

            // Interface Mock
            var IOccupationBLLMock = new Mock<IOccupationBLL>();
            var IPremiumCalculatorBLLMock = new Mock<IPremiumCalculatorBLL>();
            IPremiumCalculatorBLLMock.Setup(x => x.getPremiumValue(premiumParams))
               .Returns(Task.FromResult((decimal)468.75));

            var controller = new PremiumController(IOccupationBLLMock.Object, IPremiumCalculatorBLLMock.Object);

            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey,
                                              new HttpConfiguration());
            // Act
            var response = await controller.GetPremiumValue(premiumParams);
            var controllerResponse = response.ExecuteAsync(CancellationToken.None).Result;

            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(controllerResponse.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, controllerResponse.StatusCode);
            Assert.IsNotNull(controllerResponse.Content);

            decimal premiumValue;
            Assert.IsTrue(controllerResponse.TryGetContentValue<decimal>(out premiumValue));
            Assert.AreEqual((decimal)468.75, premiumValue);
        }

        [TestMethod]
        public async Task GetPremiumValue_InternalServerError()
        {
            // Arrange
            // Test data
            var premiumParams = new PremiumParametersData();
            premiumParams.Age = 25;
            premiumParams.OccupationId = 1;
            premiumParams.SumInsured = 150000;

            // Interface Mock
            var IOccupationBLLMock = new Mock<IOccupationBLL>();
            var IPremiumCalculatorBLLMock = new Mock<IPremiumCalculatorBLL>();
            IPremiumCalculatorBLLMock.Setup(x => x.getPremiumValue(premiumParams))
                 .Throws(new Exception("exception message"));

            var controller = new PremiumController(IOccupationBLLMock.Object, IPremiumCalculatorBLLMock.Object);

            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey,
                                              new HttpConfiguration());
            // Act
            var response = await controller.GetPremiumValue(premiumParams);
            var controllerResponse = response.ExecuteAsync(CancellationToken.None).Result;

            // Assert
            Assert.IsNotNull(response);
            Assert.IsFalse(controllerResponse.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.InternalServerError, controllerResponse.StatusCode);
        }
    }
}
