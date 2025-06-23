using FluentValidation;
using LifeInsurance.API.DTOs;

namespace LifeInsurance.API.Validators
{
    public class CreatePersonDtoValidator : AbstractValidator<CreatePersonDto>
    {
        public CreatePersonDtoValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("El nombre completo es obligatorio.").MaximumLength(100).WithMessage("El nombre completo no puede superar los 100 caracteres.");
            RuleFor(x => x.Identification).NotEmpty().WithMessage("La identificación es obligatoria.").MaximumLength(50).WithMessage("La identificación no puede superar los 50 caracteres.");
            RuleFor(x => x.Age).GreaterThan(0).WithMessage("La edad es obligatoria y debe ser mayor que cero.").InclusiveBetween(18, 110).WithMessage("la edad debe estar entre 18 y 110 años.");
            RuleFor(x => x.Gender).NotEmpty().WithMessage("El género es obligatorio.").MaximumLength(20).WithMessage("El género no puede superar los 20 caracteres");
            RuleFor(x => x.OtherDiseases).MaximumLength(200).WithMessage("Otras enfermedades no puede superar los 200 caracteres");
        }
    }
}
