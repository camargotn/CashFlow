using CashFlow.Domain.Enums;

namespace CashFlow.Domain.Entities;
public class Expense
{
    public long Id { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public DateTime DateTransaction { get; private set; }
    public decimal Value { get; private set; }
    public PaymentType PaymentType { get; private set; }

}
