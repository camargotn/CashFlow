using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expense.Reports.Excel;
public interface IExpenseReportUseCase
{
    Task<byte[]> Execute(DateOnly month);
}
