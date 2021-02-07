using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TAl.PremiumCalculator.DAL.Interface;
using TAL.PremiumCalculator.BLL.Models;
using TAL.PremiumCalculator.DAL.Interface;

namespace TAL.PremiumCalculator.BLL.Service
{
    public class OccupationBLL : IOccupationBLL
    {
        IOccupationDAL _occupationDAL;

        IOccupationRatingDAL _occupationRatingDAL;

        public OccupationBLL()
        {
        }

        public OccupationBLL(IOccupationDAL occupationDAL, IOccupationRatingDAL occupationRatingDAL)
        {
            _occupationDAL = occupationDAL;
            _occupationRatingDAL = occupationRatingDAL;
        }

        public async Task<List<OccupationData>> getAllOccupations()
        {
            return await _occupationDAL.GetOccupations().Select(
                     oc => new OccupationData()
                     {
                         OccupationName = oc.OccupationName,
                         Id = oc.Id
                     }
                 ).ToListAsync();
        }

        public async Task<decimal> getOccupationFactor(int occupationId)
        {
            return await _occupationDAL.GetOccupations().Where(o => o.Id == occupationId).Join(
           _occupationRatingDAL.GetOccupaitonRatings(), o => o.OccupationRatingId, or => or.Id, (o, or) => or.Factor)
            .SingleOrDefaultAsync();
        }
    }
}
