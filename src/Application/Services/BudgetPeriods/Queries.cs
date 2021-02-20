using Application.Interfaces.Persistence;
using Application.Queries;
using Application.Responses;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.BudgetPeriods
{
    // Queries
    public class BudgetPeriodQuery : IdQuery, IRequest<DataResponse<DataResponse<DataResponse<BudgetPeriodDto>>>>
    {
    }
    
    public class BudgetPeriodsQuery : BaseQuery, IRequest<ListResponse<BudgetPeriodListDto>>
    {
    }

    // Handlers
    public class BudgetPeriodQueryHandler : IRequestHandler<BudgetPeriodQuery, DataResponse<DataResponse<DataResponse<BudgetPeriodDto>>>>
    {
        private readonly IBudgetPeriodRepository _repository;
        private readonly IMapper _mapper;

        public BudgetPeriodQueryHandler(IBudgetPeriodRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DataResponse<DataResponse<DataResponse<BudgetPeriodDto>>>> Handle(BudgetPeriodQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<DataResponse<DataResponse<DataResponse<BudgetPeriodDto>>>>(await _repository.GetByIdAsync(request.Id, cancellationToken));
        }
    }

    public class BudgetPeriodsQueryHandler : IRequestHandler<BudgetPeriodsQuery, ListResponse<BudgetPeriodListDto>>
    {
        private readonly IBudgetPeriodRepository _repository;
        private readonly IMapper _mapper;

        public BudgetPeriodsQueryHandler(IBudgetPeriodRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ListResponse<BudgetPeriodListDto>> Handle(BudgetPeriodsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<ListResponse<BudgetPeriodListDto>>(await _repository.GetAsync(cancellationToken));
        }
    }

}
