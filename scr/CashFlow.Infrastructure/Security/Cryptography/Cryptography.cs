using CashFlow.Domain.Security.Cryptograpy;

namespace CashFlow.Infrastructure.Security.Cryptography;
public class Cryptography : IPasswordEncrypter
{
    public string Encrypt(string password)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

        return passwordHash;
    }

    public bool Verify(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}
