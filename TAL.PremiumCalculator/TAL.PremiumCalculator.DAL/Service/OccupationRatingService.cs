using System.Data.Entity;
using System.Linq;
using TAl.PremiumCalculator.DAL.Interface;
using TAL.PremiumCalculator.DAL.DataModel;

namespace TAL.PremiumCalculator.DAL.Services
{
    public class OccupationRatingDAL : IOccupationRatingDAL
    {

        private DbContext _dbContext;

        public OccupationRatingDAL(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

     
        public IQueryable<OccupationRating> GetOccupaitonRatings()
        {
            return _dbContext.Set<OccupationRating>().AsQueryable();
        }
    }
}
