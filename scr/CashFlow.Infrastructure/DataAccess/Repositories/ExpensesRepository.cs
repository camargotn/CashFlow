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

    public async Task<List<Expense>> GetAllExpenses(User user)
    {
        return await _dbContext.Expenses.AsNoTracking().Where(expense => expense.UserId == user.Id).ToListAsync();
    }

    async Task<Expense?> IExpensesReadOnlyRepository.GetExpenseById(User user, long id)
    {
        return await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.UserId == user.Id);
    }
    
    async Task<Expense?> IExpensesUpdateOnlyRepository.GetExpenseById(User user, long id)
    {
        return await _dbContext.Expenses.FirstOrDefaultAsync(x => x.Id == id && x.UserId == user.Id);
    }

    public async Task Delete(long id)
    {
        var result = await _dbContext.Expenses.FindAsync(id);

        _dbContext.Expenses.Remove(result!);
    }

    public void Update(Expense expense)
    {
        _dbContext.Update(expense);
    }

    public async Task<List<Expense>> GetExpensesByMonth(User user, DateOnly month)
    {
        var firstDayOfMonth = new DateTime(month.Year, month.Month, 1).Date;
        var firstDayNextMonth = firstDayOfMonth.AddMonths(1).Date;

        return await _dbContext
            .Expenses
            .AsNoTracking()
            .Where(expense => expense.DateTransaction < firstDayNextMonth && expense.DateTransaction >= firstDayOfMonth && expense.UserId == user.Id)
            .OrderBy(expense => expense.DateTransaction)
            .ToListAsync();
    }
}
