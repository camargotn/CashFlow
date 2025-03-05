namespace CashFlow.Domain.Security.Cryptograpy;
public interface IPasswordEncrypter
{
    string Encrypt(string password);
}
