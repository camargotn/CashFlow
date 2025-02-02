using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expense.Register;
public class RegisterExpenseUseCase : IRegisterExpenseUseCase
{
    private readonly IExpensesRepository _expenseRepository;
    public RegisterExpenseUseCase(IExpensesRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public ResponseRegisteredExpensiveJson Execute(RequestRegisterExpenseJson request)
    {
        Validate(request);

        var entity = new Domain.Entities.Expense
        {
            Title = request.Title,
            Description = request.Description,
            DateTransaction = request.DateTransaction,
            Value = request.Value,
            PaymentType = (Domain.Enums.PaymentType)request.PaymentType
        };

        _expenseRepository.Add(entity);

        return new ResponseRegisteredExpensiveJson();
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
