using System.ComponentModel.DataAnnotations;

namespace TAL.PremiumCalculator.BLL.Models
{
    public class PremiumParametersData
    {
        [Required]
        public int Age { get; set; }
        [Required]
        public int OccupationId { get; set; }
        [Required]
        public long SumInsured { get; set; }
    }
}
