using Application.Interfaces.Persistence;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.BudgetPeriods
{
    // Commands

    public class BudgetPeriodCommand
    {
        public string Name { get; set; }
        public int No { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class CreateBudgetPeriodCommand : BudgetPeriodCommand, IRequest<OperationResponse>
    {

    } 
    
    public class UpdateBudgetPeriodCommand : BudgetPeriodCommand, IRequest<OperationResponse>
    {
        public int Id { get; set; }
    }
    
    public class DeleteBudgetPeriodCommand : IRequest<OperationResponse>
    {
        public int Id { get; set; }
    }

    // Handlers

    public class CreateBudgetPeriodCommandHandler : IRequestHandler<CreateBudgetPeriodCommand, OperationResponse>
    {
        private readonly IBudgetPeriodRepository _repository;
        private readonly IMapper _mapper;

        public CreateBudgetPeriodCommandHandler(IBudgetPeriodRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OperationResponse> Handle(CreateBudgetPeriodCommand command, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(_mapper.Map<BudgetPeriod>(command), cancellationToken);

            return new OperationResponse();
        }
    }

    public class UpdateBudgetPeriodCommandHandler : IRequestHandler<UpdateBudgetPeriodCommand, OperationResponse>
    {
        private readonly IBudgetPeriodRepository _repository;
        private readonly IMapper _mapper;

        public UpdateBudgetPeriodCommandHandler(IBudgetPeriodRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OperationResponse> Handle(UpdateBudgetPeriodCommand command, CancellationToken cancellationToken)
        {
            var period = await _repository.GetByIdAsync(command.Id, cancellationToken);

            await _repository.UpdateAsync(_mapper.Map<BudgetPeriod>(period), cancellationToken);

            return new OperationResponse();
        }
    }
        
    public class DeleteBudgetPeriodCommandHandler : IRequestHandler<DeleteBudgetPeriodCommand, OperationResponse>
    {
        private readonly IBudgetPeriodRepository _repository;
        private readonly IMapper _mapper;

        public DeleteBudgetPeriodCommandHandler(IBudgetPeriodRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OperationResponse> Handle(DeleteBudgetPeriodCommand command, CancellationToken cancellationToken)
        {
            var period = await _repository.GetByIdAsync(command.Id, cancellationToken);

            await _repository.DeleteAsync(period, cancellationToken);

            return new OperationResponse();
        }
    }
}
