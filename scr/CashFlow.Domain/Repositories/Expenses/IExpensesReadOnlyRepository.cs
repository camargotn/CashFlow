using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;
public interface IExpensesReadOnlyRepository
{
    Task<List<Expense>> GetAllExpenses(User user);
    Task<Expense?> GetExpenseById(User user, long id);
    Task<List<Expense>> GetExpensesByMonth(User user, DateOnly month);
}
