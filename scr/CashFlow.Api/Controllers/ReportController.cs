using CashFlow.Application.UseCases.Expense.Reports.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ReportController : ControllerBase
{
    [HttpGet("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel(
        [FromServices] IExpenseReportUseCase useCase,
        [FromHeader] DateOnly month
        )
    {
        byte[] file = await useCase.Execute(month);

        if (file.Length == 0)
        {
            return NoContent();
        }

        return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx");
    }
}
