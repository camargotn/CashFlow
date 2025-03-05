using System.Text.RegularExpressions;
using CashFlow.Exception;
using FluentValidation;
using FluentValidation.Validators;

namespace CashFlow.Application.UseCases.User;
public class UserPasswordValidator<T> : PropertyValidator<T, string>
{
    private const string ERROR_MESSAGE_KEY = "Error Message";
    public override string Name => "UserPasswordValidator";

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return $"{{{ERROR_MESSAGE_KEY}}}";
    }

    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.PASSWORD_INVALID);
            return false;
        }

        if (password.Length < 8)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.PASSWORD_INVALID);
            return false;
        }

        if (!Regex.IsMatch(password, @"[a-z]+"))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.PASSWORD_INVALID);
            return false;
        }
        
        if (!Regex.IsMatch(password, @"[A-Z]+"))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.PASSWORD_INVALID);
            return false;
        }
        
        if (!Regex.IsMatch(password, @"[0-9]+"))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.PASSWORD_INVALID);
            return false;
        }
        
        if (!Regex.IsMatch(password, @"[\!\?\*\.\@\#\$\%\&]+"))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.PASSWORD_INVALID);
            return false;
        }

        return true;
    }
}
