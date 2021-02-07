using System.Threading.Tasks;
using TAL.PremiumCalculator.BLL.Models;

namespace TAL.PremiumCalculator.BLL.Interface
{
    public interface IPremiumCalculatorBLL
    {
       Task<decimal> getPremiumValue(PremiumParametersData premiumParamData);
    }
}
