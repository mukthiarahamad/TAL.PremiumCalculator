using System.Data.Entity;
using System.Linq;
using TAL.PremiumCalculator.DAL.DataModel;
using TAL.PremiumCalculator.DAL.Interface;

namespace TAL.PremiumCalculator.DAL.Services
{
    public class OccupationDAL : IOccupationDAL
    {

        private DbContext _dbContext;

        public OccupationDAL(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Occupation> GetOccupations()
        {
           return _dbContext.Set<Occupation>().AsQueryable();
        }
    }
}
