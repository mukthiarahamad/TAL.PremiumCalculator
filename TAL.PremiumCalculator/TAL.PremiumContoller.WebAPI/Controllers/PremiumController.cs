using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TAL.PremiumCalculator.BLL.Interface;
using TAL.PremiumCalculator.BLL.Models;
using TAL.PremiumCalculator.BLL.Service;

namespace TAL.PremiumCalculator.WebAPI.Controllers
{
    public class PremiumController : ApiController
    {
        IOccupationBLL _occupationBLL;
        IPremiumCalculatorBLL _premiumCalculatorBLL;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public PremiumController(IOccupationBLL occupationBLL, IPremiumCalculatorBLL premiumCalculatorBLL)
        {
            _occupationBLL = occupationBLL;
            _premiumCalculatorBLL = premiumCalculatorBLL;
        }


        [HttpGet]
        [ActionName("Index")]
        public HttpResponseMessage Index()
        {
            var response = new HttpResponseMessage()
            {
                Content = new StringContent("The Web API has started, Please start the UI Interface."),
            };
            return response;
        }

        [HttpGet]
        [ActionName("GetOccupations")]
        public async Task<IHttpActionResult> GetOccupations()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _occupationBLL.getAllOccupations();
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, result));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex));
            }
        }

        [HttpPost]
        [ActionName("GetPremiumValue")]
        public async Task<IHttpActionResult> GetPremiumValue([FromBody]PremiumParametersData premiumParametersData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _premiumCalculatorBLL.getPremiumValue(premiumParametersData);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, result));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex));
            }
        }
    }
}
