using System.ComponentModel.DataAnnotations.Schema;

namespace Population.SQLite.App.Domain.Entities
{
    [Table("Estimate")]
    public class Estimates
    {
        public int State { get; set; }
        public decimal ActualPopulation { get; set; }
        public decimal ActualHouseholds { get; set; }
        public decimal EstimatesHouseholds { get; set; }
    }
}
