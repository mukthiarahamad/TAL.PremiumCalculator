using System.Linq;
using TAL.PremiumCalculator.DAL.DataModel;

namespace TAl.PremiumCalculator.DAL.Interface
{
    public interface IOccupationRatingDAL
    {
        IQueryable<OccupationRating> GetOccupaitonRatings();
    }
}
