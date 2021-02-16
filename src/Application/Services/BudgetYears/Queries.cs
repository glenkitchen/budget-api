using Application.Interfaces.Persistence;
using Application.Queries;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.BudgetYears
{
    // Queries
    public class BudgetYearQuery : IdQuery, IRequest<BudgetYearDto>
    {
    }

    public class BudgetYearsQuery : Query, IRequest<List<BudgetYearListDto>>
    {
    }

    // Handlers
    public class BudgetYearQueryHandler : IRequestHandler<BudgetYearQuery, BudgetYearDto>
    {
        private readonly IBudgetYearRepository _repository;
        private readonly IMapper _mapper;

        public BudgetYearQueryHandler(IBudgetYearRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BudgetYearDto> Handle(BudgetYearQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<BudgetYearDto>(await _repository.GetByIdAsync(request.Id));
        }
    }

    public class BudgetYearsQueryHandler : IRequestHandler<BudgetYearsQuery, List<BudgetYearListDto>>
    {
        private readonly IBudgetYearRepository _repository;
        private readonly IMapper _mapper;

        public BudgetYearsQueryHandler(IBudgetYearRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<BudgetYearListDto>> Handle(BudgetYearsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BudgetYearListDto>>(await _repository.GetAsync());
        }
    }
}
