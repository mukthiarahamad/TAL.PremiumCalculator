using System.Collections.Generic;
using System.Threading.Tasks;
using TAL.PremiumCalculator.BLL.Models;

namespace TAL.PremiumCalculator.BLL.Service
{
    public interface IOccupationBLL
    {
      Task<List<OccupationData>>  getAllOccupations();
      Task<decimal> getOccupationFactor(int occupationId);
    }
}
