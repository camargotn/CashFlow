
using AutoMapper;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories;
using CashFlow.Exception.ExceptionBase;
using System.Runtime.Versioning;
using CashFlow.Exception;
using CashFlow.Domain.Services.LoggedUser;

namespace CashFlow.Application.UseCases.Expense.Delete;
public class DeleteExpenseUseCase : IDeleteExpenseUseCase
{
    private readonly IExpensesWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;
    private readonly IExpensesReadOnlyRepository _readOnlyRepository;

    public DeleteExpenseUseCase(
        IExpensesWriteOnlyRepository repository,
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser,
        IExpensesReadOnlyRepository readOnlyRepository)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
        _readOnlyRepository = readOnlyRepository;
    }

    public async Task Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();

        var expense = await _readOnlyRepository.GetExpenseById(loggedUser, id);

        if (expense is null)
        {
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        }

        await _repository.Delete(id);

        await _unitOfWork.Commit();

    }
}
