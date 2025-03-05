using CashFlow.Communication.Requests;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UseCases.User.Register;
public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_REQUIRED);
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMAIL_REQUIRED)
            .EmailAddress()
            .WithMessage(ResourceErrorMessages.EMAIL_INVALID);
        RuleFor(user => user.Password).SetValidator(new UserPasswordValidator<RequestRegisterUserJson>());
    }
}
