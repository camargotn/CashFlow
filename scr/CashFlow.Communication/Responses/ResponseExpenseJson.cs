﻿using CashFlow.Communication.Enums;

namespace CashFlow.Communication.Responses;
public class ResponseExpenseJson
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public DateTime DateTransaction { get; set; }
    public decimal Value { get; set; }
    public PaymentType PaymentType { get; set; }
}
