using Application.Interfaces.Persistence;
using Application.Queries;
using Application.Services.BudgetYears;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.BudgetPeriods
{
    // Queries
    public class BudgetPeriodQuery : IdQuery, IRequest<BudgetPeriodDto>
    {
    }
    
    public class BudgetPeriodsQuery : Query, IRequest<List<BudgetPeriodListDto>>
    {
    }

    // Handlers
    public class BudgetPeriodQueryHandler : IRequestHandler<BudgetPeriodQuery, BudgetPeriodDto>
    {
        private readonly IBudgetPeriodRepository _repository;
        private readonly IMapper _mapper;

        public BudgetPeriodQueryHandler(IBudgetPeriodRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BudgetPeriodDto> Handle(BudgetPeriodQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<BudgetPeriodDto>(await _repository.GetByIdAsync(request.Id));
        }
    }

    public class BudgetPeriodsQueryHandler : IRequestHandler<BudgetPeriodsQuery, List<BudgetPeriodListDto>>
    {
        private readonly IBudgetPeriodRepository _repository;
        private readonly IMapper _mapper;

        public BudgetPeriodsQueryHandler(IBudgetPeriodRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<BudgetPeriodListDto>> Handle(BudgetPeriodsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<BudgetPeriodListDto>>(await _repository.GetAsync());
        }
    }

}
