using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expense.Register;
public class RegisterExpenseUseCase
{
    public ResponseRegisterExpensiveJson Execute(RequestRegisterExpenseJson request)
    {
        Validate(request);

        return new ResponseRegisterExpensiveJson();
    }

    private void Validate(RequestRegisterExpenseJson request)
    {
        var isTitleEmpty = string.IsNullOrWhiteSpace(request.Title);
        if (isTitleEmpty)
        {
            throw new ArgumentException("The title is required.");
        }

        if (request.Value <= 0)
        {
            throw new ArgumentException("Value must be greater than 0");
        }

        var isFutureDate = DateTime.Compare(request.DateTransaction, DateTime.UtcNow);
        if (isFutureDate > 0)
        {
            throw new ArgumentException("The expense must be in the past."
        }

        var isValidPaymentType Enum.IsDefined(typeof(PaymentType), request.PaymentType);
        if (isValidPaymentType == false) 
        {
            throw new ArgumentException("Payment Type does not exist.");
        }
    }
}
