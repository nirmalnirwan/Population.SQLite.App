using MediatR;
using Population.SQLite.App.Application.Abstracts;
using Population.SQLite.App.Application.Household.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Household.SQLite.App.Application.Household.Queries.GetHousehold
{
    public class GetHouseholdQuery : IRequest<List<HouseholdModel>>
    {
        public int[] States { get; set; }

        public class GetHouseholdsQueryHandler : IRequestHandler<GetHouseholdQuery, List<HouseholdModel>>
        {
            private readonly IHouseholdRepository _householdRepository;

            public GetHouseholdsQueryHandler(IHouseholdRepository householdRepository)
            {
                _householdRepository = householdRepository;
            }

            public async Task<List<HouseholdModel>> Handle(GetHouseholdQuery request, CancellationToken cancellationToken)
            {
                return await _householdRepository.GetTotalNumberOfHouseHold(request.States);
            }
        }
    }
}

