﻿using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;
public interface IExpensesReadOnlyRepository
{
    Task<List<Expense>> GetAllExpenses();
    Task<Expense?> GetExpenseById(long id);
    Task<List<Expense>> GetExpensesByMonth(DateOnly month);
}
