using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;
internal class ExpensesRepository : IExpensesReadOnlyRepository, IExpensesWriteOnlyRepository, IExpensesUpdateOnlyRepository
{
    private readonly CashFlowDbContext _dbContext;

    public ExpensesRepository(CashFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Expense expense)
    {
        await _dbContext.Expenses.AddAsync(expense);
    }

    public async Task<List<Expense>> GetAllExpenses()
    {
        return await _dbContext.Expenses.AsNoTracking().ToListAsync();
    }

    async Task<Expense?> IExpensesReadOnlyRepository.GetExpenseById(long id)
    {
        return await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }
    
    async Task<Expense?> IExpensesUpdateOnlyRepository.GetExpenseById(long id)
    {
        return await _dbContext.Expenses.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> Delete(long id)
    {
        var result = await _dbContext.Expenses.FirstOrDefaultAsync(x => x.Id == id);

        if (result is null)
        {
            return false;
        }

        _dbContext.Expenses.Remove(result);
        return true;
    }

    public void Update(Expense expense)
    {
        _dbContext.Update(expense);
    }

    public async Task<List<Expense>> GetExpensesByMonth(DateOnly month)
    {
        var firstDayOfMonth = new DateTime(month.Year, month.Month, 1).Date;
        var firstDayNextMonth = firstDayOfMonth.AddMonths(1).Date;

        return await _dbContext
            .Expenses
            .AsNoTracking()
            .Where(expense => expense.DateTransaction < firstDayNextMonth && expense.DateTransaction >= firstDayOfMonth)
            .OrderBy(expense => expense.DateTransaction)
            .ToListAsync();
    }
}
