using Population.SQLite.App.Application.Household.Models;
using Population.SQLite.App.Application.Population.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Population.SQLite.App.Application.Abstracts
{
    public interface IPopulationRepository
    {
        Task<List<PopulationModel>> GetTotalNumberOfPopulation(int[] states);
    }
}
