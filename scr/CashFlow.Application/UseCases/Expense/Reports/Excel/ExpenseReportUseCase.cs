using CashFlow.Domain.Enums;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Expense.Reports.Excel;

public class ExpenseReportUseCase : IExpenseReportUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private readonly IExpensesReadOnlyRepository _repository;

    public ExpenseReportUseCase(IExpensesReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<byte[]> Execute(DateOnly month)
    {
        var expenses = await _repository.GetExpensesByMonth(month);

        if (expenses.Count == 0)
        {
            return [];
        }

        var workbook = new XLWorkbook();

        workbook.Author = "CashFlow";
        workbook.Properties.Company = "CashFlow";
        workbook.Properties.Subject = "Expense Report";
        workbook.Properties.Title = "Expense Report";
        workbook.Properties.Created = DateTime.Now;
        workbook.Style.Font.FontName = "Calibri";
        workbook.Style.Font.FontSize = 12;

        var worksheet = workbook.Worksheets.Add(month.ToString("Y"));

        CreateHeader(worksheet);

        var row = 2;
        foreach (var expense in expenses)
        {
            worksheet.Cell($"A{row}").Value = expense.Title;
            worksheet.Cell($"B{row}").Value = expense.DateTransaction;
            worksheet.Cell($"C{row}").Value = GetPaymentType(expense.PaymentType);
            worksheet.Cell($"D{row}").Value = expense.Description;
            worksheet.Cell($"E{row}").Value = expense.Value;
            worksheet.Cell($"E{row}").Style.NumberFormat.Format = $"-{CURRENCY_SYMBOL} #,##0.00";

            row++;
        }

        worksheet.Columns().AdjustToContents();

        var file = new MemoryStream();
        workbook.SaveAs(file);

        return file.ToArray();
    }

    private string GetPaymentType(PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.Cash => "Cash",
            PaymentType.CreditCard => "Credit Card",
            PaymentType.DebitCard => "Debit Card",
            _ => string.Empty
        };
    }

    private void CreateHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReport.TITLE;
        worksheet.Cell("B1").Value = ResourceReport.DATE_TRANSACTION;
        worksheet.Cell("C1").Value = ResourceReport.PAYMENT_TYPE;
        worksheet.Cell("D1").Value = ResourceReport.DESCRIPTION;
        worksheet.Cell("E1").Value = ResourceReport.VALUE;

        worksheet.Cells("A1:E1").Style.Font.Bold = true;

        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#d4d700");

        worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
    }
}
