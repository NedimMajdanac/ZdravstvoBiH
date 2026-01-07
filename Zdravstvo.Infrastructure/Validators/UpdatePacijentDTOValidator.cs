using FluentValidation;
using Zdravstvo.Core.DTOs;

namespace Zdravstvo.Infrastructure.Validators
{
    public class UpdatePacijentDTOValidator : AbstractValidator<PacijentDTO.UpdatePacijentDTO>
    {
        public UpdatePacijentDTOValidator()
        {
            RuleFor(x => x.Ime)
                .NotEmpty().WithMessage("Ime je obavezno")
                .MaximumLength(100);

            RuleFor(x => x.Prezime)
                .NotEmpty().WithMessage("Prezime je obavezno")
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email je obavezan")
                .EmailAddress().WithMessage("Neispravan format email adrese");

            RuleFor(x => x.BrojTelefona)
                .NotEmpty().WithMessage("Broj telefona je obavezan")
                .Matches(@"^\+?\d{6,15}$").WithMessage("Neispravan format broja telefona");
        }
    }
}
