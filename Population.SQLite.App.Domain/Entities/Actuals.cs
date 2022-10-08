using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Population.SQLite.App.Domain.Entities
{
    [Table("Actuals")]
    public class Actuals
    {
        public int State { get; set; }
        public Decimal ActualPopulation { get; set; }
        public decimal ActualHouseholds { get; set; }
    }
}
