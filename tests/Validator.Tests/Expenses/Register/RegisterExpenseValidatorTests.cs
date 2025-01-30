using CashFlow.Application.UseCases.Expense.Register;
using CashFlow.Communication.Requests;

namespace Validator.Tests.Expenses.Register;
public class RegisterExpenseValidatorTests
{
    [Fact]
    public void Success()
    {
        // Arrange
        var validator = new RegisterExpenseValidator();

        var request = new RequestRegisterExpenseJson
        {
            Title = "Title",
            Description = "Description",
            DateTransaction = DateTime.Now,
            Value = 100,
            PaymentType = CashFlow.Communication.Enums.PaymentType.CreditCard,
        };

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.True(result.IsValid);

    }
}
