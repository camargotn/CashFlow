using CashFlow.Communication.Requests;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expense.Register;
public class RegisterExpenseValidator : AbstractValidator<RequestRegisterExpenseJson>
{
    public RegisterExpenseValidator()
    {
        RuleFor(expense => expense.Title).NotEmpty().WithMessage(ResourceErrorMessages.TITLE_REQUIRED);
        RuleFor(expense => expense.Value).GreaterThan(0).WithMessage(ResourceErrorMessages.VALUE_MUST_BE_GREATER_THAN_ZERO);
        RuleFor(expense => expense.DateTransaction).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMessages.EXPENSE_MUST_BE_IN_THE_PAST);
        RuleFor(expense => expense.PaymentType).IsInEnum().WithMessage(ResourceErrorMessages.PAYMENT_TYPE_DOES_NOT_EXIST);
    }

}
