using System.Threading.Tasks;
using TAL.PremiumCalculator.BLL.Interface;
using TAL.PremiumCalculator.BLL.Models;

namespace TAL.PremiumCalculator.BLL.Service
{
    public class PremiumCalculatorBLL : IPremiumCalculatorBLL
    {
        IOccupationBLL _occupationBLL;
        public PremiumCalculatorBLL(IOccupationBLL occupationBLL)
        {
            _occupationBLL = occupationBLL;
        }

        public async Task<decimal> getPremiumValue(PremiumParametersData premiumParamData)
        {
            var occupationFactor = await _occupationBLL.getOccupationFactor(premiumParamData.OccupationId);

            return (premiumParamData.SumInsured * occupationFactor * premiumParamData.Age) / (1000 * 12);
        }
    }
}
