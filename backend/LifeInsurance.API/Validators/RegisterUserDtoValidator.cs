using FluentValidation;
using LifeInsurance.API.DTOs;

namespace LifeInsurance.API.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("El email es obligatorio.").EmailAddress().WithMessage("El email no tiene un formato válido.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.")
                .Matches(@"[A-Z]").WithMessage("Debe contener al menos una mayúscula")
                .Matches(@"[a-z]").WithMessage("Debe contener al menos una minúscula")
                .Matches(@"\d").WithMessage("Debe contener al menos un número");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("El nombre es obligatorio.").MaximumLength(100).WithMessage("El nombre no debe superar los 100 caracteres");
            RuleFor(x=>x.LastName).NotEmpty().WithMessage("El apellido es obligatorio.").MaximumLength(100).WithMessage("El apellido no debe superar los 100 caracteres");

        }
    }
}
