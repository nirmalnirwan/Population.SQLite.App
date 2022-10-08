using MediatR;
using Population.SQLite.App.Application.Abstracts;
using Population.SQLite.App.Application.Population.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Population.SQLite.App.Application.Population.Queries.GetPopulation
{
    public class GetPopulationQuery : IRequest<List<PopulationModel>>
    {
        public int[] States { get; set; }

        public class GetPopulationsQueryHandler : IRequestHandler<GetPopulationQuery, List<PopulationModel>>
        {
            private readonly IPopulationRepository _populationRepository;

            public GetPopulationsQueryHandler(IPopulationRepository populationRepository)
            {
                _populationRepository = populationRepository;
            }

            public async Task<List<PopulationModel>> Handle(GetPopulationQuery request, CancellationToken cancellationToken)
            {
                return await _populationRepository.GetTotalNumberOfPopulation(request.States);
            }
        }
    }
}

