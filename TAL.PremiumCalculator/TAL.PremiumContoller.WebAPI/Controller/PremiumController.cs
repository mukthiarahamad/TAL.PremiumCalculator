using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TAL.PremiumCalculator.BLL.Interface;
using TAL.PremiumCalculator.BLL.Models;
using TAL.PremiumCalculator.BLL.Service;

namespace TAL.PremiumContoller.WebAPI.Controller
{

    public class PremiumController : ApiController
    {
        IOccupationBLL _occupationBLL;
        IPremiumCalculatorBLL _premiumCalculatorBLL;

        public PremiumController(IOccupationBLL occupationBLL, IPremiumCalculatorBLL premiumCalculatorBLL)
        {
            _occupationBLL = occupationBLL;
            _premiumCalculatorBLL = premiumCalculatorBLL;
        }
    }
}
