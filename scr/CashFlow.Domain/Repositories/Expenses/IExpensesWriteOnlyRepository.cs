using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;
public interface IExpensesWriteOnlyRepository
{
    Task Add(Expense expense);

    /// <summary>
    /// Delete an expense by id. Return true if the expense was deleted, false if not found.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> Delete(long id);
}
