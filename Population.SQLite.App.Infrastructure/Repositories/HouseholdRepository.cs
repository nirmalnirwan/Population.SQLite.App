using Microsoft.EntityFrameworkCore;
using Population.SQLite.App.Application.Abstracts;
using Population.SQLite.App.Application.Common.Interfaces;
using Population.SQLite.App.Application.Household.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Population.SQLite.App.Infrastructure.Repositories
{
    public class HouseholdRepository : IHouseholdRepository
    {
        private IApplicationDBContext _applicationDBContext;

        public HouseholdRepository(IApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }
        public async Task<List<HouseholdModel>> GetTotalNumberOfHouseHold(int[] states)
        {
            List<HouseholdModel> result = null;
            if (states != null)
            {
                var actualCount = await _applicationDBContext.Actual.Where(w => states.Contains(w.State)).ToListAsync();
                if (actualCount.Count > 0)
                {
                    result = actualCount.Select(x => new HouseholdModel() { State = x.State, Household = x.ActualHouseholds }).ToList();
                }
                else
                {
                    var estimatedCount = await _applicationDBContext.Estimate.Where(w => states.Contains(w.State)).ToListAsync();
                    if (estimatedCount.Count > 0)
                    {
                        result = new List<HouseholdModel>()
                        {
                            new HouseholdModel()
                            {
                                State =string.Join(",", states.Select(x=>x.ToString())),
                                Household = estimatedCount.Sum(x => x.ActualHouseholds)
                            }
                        };
                    }
                }
            }
            return result;
        }
    }
}
