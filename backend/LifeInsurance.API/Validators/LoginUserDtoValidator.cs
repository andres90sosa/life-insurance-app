using FluentValidation;
using LifeInsurance.API.DTOs;

namespace LifeInsurance.API.Validators
{
    public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("El email es obligatorio.").EmailAddress().WithMessage("El email no tiene un formato válido.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.")
                .Matches(@"[A-Z]").WithMessage("Debe contener al menos una mayúscula")
                .Matches(@"[a-z]").WithMessage("Debe contener al menos una minúscula")
                .Matches(@"\d").WithMessage("Debe contener al menos un número");
        }
    }
}
