using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;

namespace CommonTestsUtilities.Requests;
public class RequestRegisterExpenseJsonBuilder
{
    public static RequestRegisterExpenseJson Build()
    {
        return new Faker<RequestRegisterExpenseJson>()
            .RuleFor(x => x.Title, f => f.Commerce.ProductName())
            .RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
            .RuleFor(x => x.DateTransaction, f => f.Date.Past())
            .RuleFor(x => x.Value, f => f.Finance.Amount())
            .RuleFor(x => x.PaymentType, f => f.PickRandom<PaymentType>())
            .Generate();
    }
}
