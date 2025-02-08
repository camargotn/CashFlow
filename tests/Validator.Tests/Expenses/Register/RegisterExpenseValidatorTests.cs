using CashFlow.Application.UseCases.Expense;
using CashFlow.Application.UseCases.Expense.Register;
using CashFlow.Communication.Enums;
using CashFlow.Exception;
using CommonTestsUtilities.Requests;
using FluentAssertions;

namespace Validator.Tests.Expenses.Register;
public class RegisterExpenseValidatorTests
{
    [Fact]
    public void Success()
    {
        // Arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();

        // Act
        var result = validator.Validate(request);

        // Assert
       result.IsValid.Should().BeTrue();

    }

    [Theory]
    [InlineData("")]
    [InlineData("     ")]
    [InlineData(null)]
    public void ErrorTitleEmpty(string title)
    {
        // Arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Title = title;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage == ResourceErrorMessages.TITLE_REQUIRED);
    }

    [Fact]
    public void ErrorDateFuture()
    {
        // Arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.DateTransaction = DateTime.UtcNow.AddDays(1);

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage == ResourceErrorMessages.EXPENSE_MUST_BE_IN_THE_PAST);
    }

    [Fact]
    public void ErrorInvalidPaymentType()
    {
        // Arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.PaymentType = (PaymentType)int.MaxValue;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage == ResourceErrorMessages.PAYMENT_TYPE_DOES_NOT_EXIST);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-7)]
    public void ErrorValueInvalid(decimal value)
    {
        // Arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Value = value;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage == ResourceErrorMessages.VALUE_MUST_BE_GREATER_THAN_ZERO);
    }
}
