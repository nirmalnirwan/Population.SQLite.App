using Microsoft.EntityFrameworkCore;
using Population.SQLite.App.Application.Abstracts;
using Population.SQLite.App.Application.Common.Interfaces;
using Population.SQLite.App.Application.Population.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Population.SQLite.App.Infrastructure.Repositories
{
    public class PopulationRepository : IPopulationRepository
    {
        private IApplicationDBContext _applicationDBContext;

        public PopulationRepository(IApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }
        public async Task<List<PopulationModel>> GetTotalNumberOfPopulation(int[] states)
        {
            List<PopulationModel> result = null;
            if (states != null)
            {
                var actualCount = await _applicationDBContext.Actual.Where(w => states.Contains(w.State)).ToListAsync();
                if (actualCount.Count > 0)
                {
                    result = actualCount.Select(x => new PopulationModel() { State = x.State, Population = x.ActualPopulation }).ToList();
                }
                else
                {
                    var estimatedCount = await _applicationDBContext.Estimate.Where(w => states.Contains(w.State)).ToListAsync();
                    if (estimatedCount.Count > 0)
                    {
                        result = new List<PopulationModel>()
                        {
                            new PopulationModel()
                            {
                                State =string.Join(",", states.Select(x=>x.ToString())),
                                Population = estimatedCount.Sum(x => x.ActualPopulation)
                            }
                        };
                    }
                }
            }
            return result;
        }
    }
}
