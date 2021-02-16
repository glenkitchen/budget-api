using FluentValidation;

namespace Application.Services.BudgetYears
{
    public class BudgetYearCommandValidator<T> : AbstractValidator<T> where T: BudgetYearCommand
    {
        public BudgetYearCommandValidator()
        {
            RuleFor(e => e.Name).NotEmpty().MaximumLength(250);
        }
    }

    public class CreateBudgetYearCommandValidator : BudgetYearCommandValidator<CreateBudgetYearCommand>
    {
        
    }

    public class UpdateBudgetYearCommandValidator: BudgetYearCommandValidator<UpdateBudgetYearCommand>
    {
        public UpdateBudgetYearCommandValidator()
        {
            RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        }
    }

    public class DeleteBudgetYearCommandValidator : AbstractValidator<DeleteBudgetYearCommand>
    {
        public DeleteBudgetYearCommandValidator()
        {
            RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        }
    }
}
