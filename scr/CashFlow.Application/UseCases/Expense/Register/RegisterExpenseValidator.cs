using CashFlow.Communication.Requests;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expense.Register;
public class RegisterExpenseValidator : AbstractValidator<RequestRegisterExpenseJson>
{
    public RegisterExpenseValidator()
    {
        RuleFor(expense => expense.Title).NotEmpty().WithMessage("The title is required.");
        RuleFor(expense => expense.Value).GreaterThan(0).WithMessage("Value must be greater than 0");
        RuleFor(expense => expense.DateTransaction).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("The expense must be in the past.");
        RuleFor(expense => expense.PaymentType).IsInEnum().WithMessage("Payment Type does not exist.");
    }

}
