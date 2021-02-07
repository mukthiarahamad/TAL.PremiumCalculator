using System.Linq;
using TAL.PremiumCalculator.DAL.DataModel;

namespace TAL.PremiumCalculator.DAL.Interface
{
    public interface IOccupationDAL
    {
        IQueryable<Occupation>  GetOccupations();
    }
}
