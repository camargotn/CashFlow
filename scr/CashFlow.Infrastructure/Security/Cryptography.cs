﻿using CashFlow.Domain.Security.Cryptograpy;

namespace CashFlow.Infrastructure.Security;
public class Cryptography : IPasswordEncrypter
{
    public string Encrypt(string password)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

        return passwordHash;
    }
}
