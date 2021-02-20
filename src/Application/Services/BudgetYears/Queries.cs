using Application.Interfaces.Persistence;
using Application.Queries;
using Application.Responses;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.BudgetYears
{
    // Queries
    public class BudgetYearQuery : IdQuery, IRequest<DataResponse<DataResponse<BudgetYearDto>>>
    {
    }

    public class BudgetYearsQuery : BaseQuery, IRequest<ListResponse<BudgetYearListDto>>
    {
    }

    // Handlers
    public class BudgetYearQueryHandler : IRequestHandler<BudgetYearQuery, DataResponse<DataResponse<BudgetYearDto>>>
    {
        private readonly IBudgetYearRepository _repository;
        private readonly IMapper _mapper;

        public BudgetYearQueryHandler(IBudgetYearRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DataResponse<DataResponse<BudgetYearDto>>> Handle(BudgetYearQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<DataResponse<DataResponse<BudgetYearDto>>>(await _repository.GetByIdAsync(request.Id, cancellationToken));
        }
    }

    public class BudgetYearsQueryHandler : IRequestHandler<BudgetYearsQuery, ListResponse<BudgetYearListDto>>
    {
        private readonly IBudgetYearRepository _repository;
        private readonly IMapper _mapper;

        public BudgetYearsQueryHandler(IBudgetYearRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ListResponse<BudgetYearListDto>> Handle(BudgetYearsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<ListResponse<BudgetYearListDto>>(await _repository.GetAsync(cancellationToken));
        }
    }
}
