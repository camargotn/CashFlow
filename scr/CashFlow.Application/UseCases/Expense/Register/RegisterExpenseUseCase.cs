using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expense.Register;
public class RegisterExpenseUseCase
{
    public ResponseRegisterExpensiveJson Execute(RequestRegisterExpenseJson request)
    {
        Validate(request);

        var entity = new ExpenseEntity
        {
            Title = request.Title,
            Description = request.Description,
            DateTransaction = request.DateTransaction,
            Value = request.Value,
            PaymentType = request.PaymentType
        };

        return new ResponseRegisterExpensiveJson();
    }

    private void Validate(RequestRegisterExpenseJson request)
    {
        var validator = new RegisterExpenseValidator();

        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
