using Application.Exceptions;
using Application.Interfaces.Persistence;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.BudgetYears
{
    // Commands

    public class BudgetYearCommand 
    {
        public string Name { get; set; }
    }

    public class CreateBudgetYearCommand : BudgetYearCommand, IRequest<OperationResponse<BudgetYear>>
    {
        
    }
        
    public class UpdateBudgetYearCommand : BudgetYearCommand, IRequest<OperationResponse>
    {
        public int Id { get; set; }
    }
    
    public class DeleteBudgetYearCommand : IRequest<OperationResponse>
    {
        public int Id { get; set; }
    }

    // Handlers

    public class CreateBudgetYearCommandHandler : IRequestHandler<CreateBudgetYearCommand, OperationResponse<BudgetYear>>
    {
        private readonly IBudgetYearRepository _repository;
        private readonly IMapper _mapper;

        public CreateBudgetYearCommandHandler(IBudgetYearRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OperationResponse<BudgetYear>> Handle(CreateBudgetYearCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateBudgetYearCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (validationResult.Errors.Count > 0)
            {
                //throw new ValidationException(validationResult);
                return new OperationResponse<BudgetYear>() {
                    Success = false,
                    ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
                
            }

            var year = await _repository.AddAsync(_mapper.Map<BudgetYear>(command), cancellationToken);

            return new OperationResponse<BudgetYear> { 
                OperationId = year.Id,
                Data = year,                
            }; 
        }
    }

    public class UpdateBudgetYearCommandHandler : IRequestHandler<UpdateBudgetYearCommand, OperationResponse>
    {
        private readonly IBudgetYearRepository _repository;
        private readonly IMapper _mapper;

        public UpdateBudgetYearCommandHandler(IBudgetYearRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OperationResponse> Handle(UpdateBudgetYearCommand command, CancellationToken cancellationToken)
        {
            var year = _repository.GetEntityAsync(command.Id, cancellationToken);

            await _repository.UpdateAsync(_mapper.Map<BudgetYear>(year), cancellationToken);

            return new OperationResponse();
        }
    }  
    
    public class DeleteBudgetYearCommandHandler : IRequestHandler<DeleteBudgetYearCommand, OperationResponse>
    {
        private readonly IBudgetYearRepository _repository;
        private readonly IMapper _mapper;

        public DeleteBudgetYearCommandHandler(IBudgetYearRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OperationResponse> Handle(DeleteBudgetYearCommand command, CancellationToken cancellationToken)
        {
            //var year = await _repository.GetEntityAsync(command.Id, cancellationToken);

            await _repository.DeleteAsync(command.Id, cancellationToken);

            return new OperationResponse();
        }
    }

}
