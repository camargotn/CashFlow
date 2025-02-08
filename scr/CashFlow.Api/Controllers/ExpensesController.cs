﻿using CashFlow.Application.UseCases.Expense.Register;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredExpensiveJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson),StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterExpenseUseCase useCase,
        [FromBody] RequestRegisterExpenseJson request
        )
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
