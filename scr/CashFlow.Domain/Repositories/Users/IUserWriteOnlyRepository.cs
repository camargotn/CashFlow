using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Users;
public interface IUserWriteOnlyRepository
{
    public Task Add(User user);
}
