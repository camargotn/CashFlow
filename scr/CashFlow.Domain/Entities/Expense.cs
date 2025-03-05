using CashFlow.Domain.Enums;

namespace CashFlow.Domain.Entities;
public class Expense
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime DateTransaction { get; set; }
    public decimal Value { get; set; }
    public PaymentType PaymentType { get; set; }

    public long UserId { get; set; }
    public User User { get; set; } = null!;

}
