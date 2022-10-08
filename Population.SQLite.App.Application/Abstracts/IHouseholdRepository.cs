using Population.SQLite.App.Application.Household.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Population.SQLite.App.Application.Abstracts
{
    public interface IHouseholdRepository
    {
        Task<List<HouseholdModel>> GetTotalNumberOfHouseHold(int[] states);
    }
}
