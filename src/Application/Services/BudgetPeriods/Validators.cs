using FluentValidation;

namespace Application.Services.BudgetPeriods
{
    public class BudgetPeriodCommandValidator<T> : AbstractValidator<T> where T : BudgetPeriodCommand
    {
        public BudgetPeriodCommandValidator()
        {
            RuleFor(e => e.Name).NotEmpty().MaximumLength(250);
            RuleFor(e => e.No).NotEmpty().GreaterThan(0);
            RuleFor(e => e.StartDate).NotEmpty();
            RuleFor(e => e.EndDate).NotEmpty().GreaterThan(e => e.StartDate);
        }
    }

    public class CreateBudgetPeriodCommandValidator : BudgetPeriodCommandValidator<CreateBudgetPeriodCommand>
    {

    }

    public class UpdateBudgetPeriodCommandValidator : BudgetPeriodCommandValidator<UpdateBudgetPeriodCommand>
    {
        public UpdateBudgetPeriodCommandValidator()
        {
            RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        }
    }

    public class DeleteBudgetPeriodCommandValidator : AbstractValidator<DeleteBudgetPeriodCommand>
    {
        public DeleteBudgetPeriodCommandValidator()
        {
            RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        }
    }
}
