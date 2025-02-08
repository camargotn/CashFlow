using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    [HttpGet("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel()
    {
        byte[] file = new byte[1];

        if (file.Length == 0)
        {
            return NoContent();
        }

        return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx");
    }
}
