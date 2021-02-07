using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using TAl.PremiumCalculator.DAL.Interface;
using TAL.PremiumCalculator.BLL.Interface;
using TAL.PremiumCalculator.BLL.Models;
using TAL.PremiumCalculator.BLL.Service;
using TAL.PremiumCalculator.DAL.DataModel;
using TAL.PremiumCalculator.DAL.Interface;
using TAL.PremiumCalculator.DAL.Services;
using TAL.PremiumCalculator.WebAPI.Controllers;

namespace TAL.PremiumCalculator.WebAPI.Tests.Controllers
{
    [TestClass]
    public class PremiumControllerTest
    {
        IOccupationBLL _occupationBLL;
        IPremiumCalculatorBLL _premiumCalculatorBLL;

        public PremiumControllerTest()
        {
            setupFixtures();
        }

        public void setupFixtures()
        {
            PremiumDBEntities premiumDbEntities = new PremiumDBEntities();
            IOccupationDAL _occupationalDAL = new OccupationDAL(premiumDbEntities);
            IOccupationRatingDAL _occupationRatingDAL = new OccupationRatingDAL(premiumDbEntities);
            _occupationBLL = new OccupationBLL(_occupationalDAL, _occupationRatingDAL);
            _premiumCalculatorBLL = new PremiumCalculatorBLL(_occupationBLL);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetOccupationsTestAsync()
        {
            var controller = new PremiumController(_occupationBLL, _premiumCalculatorBLL)
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };

            var response = await controller.GetOccupations();

            var controllerResponse = response.ExecuteAsync(CancellationToken.None).Result;

            Assert.IsNotNull(response);
            Assert.IsTrue(controllerResponse.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, controllerResponse.StatusCode);

            List<OccupationData> occupations;
            Assert.IsTrue(controllerResponse.TryGetContentValue(out occupations));
            Assert.AreEqual(6, occupations.Count);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetPremiumValueTestAsync()
        {
            var controller = new PremiumController(_occupationBLL, _premiumCalculatorBLL)
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };

            // Test data
            var premiumParams = new PremiumParametersData();
            premiumParams.Age = 25;
            premiumParams.OccupationId = 1;
            premiumParams.SumInsured = 150000;

            var response = await controller.GetPremiumValue(premiumParams);
            var controllerResponse = response.ExecuteAsync(CancellationToken.None).Result;

            Assert.IsNotNull(response);
            Assert.IsTrue(controllerResponse.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, controllerResponse.StatusCode);
            Assert.IsNotNull(controllerResponse.Content);           

            decimal premiumValue;
            Assert.IsTrue(controllerResponse.TryGetContentValue<decimal>(out premiumValue));
            Assert.AreEqual((decimal)468.75, premiumValue);
        }
    }
}
