using FluentValidation;
using Zdravstvo.Core.DTOs;
using Zdravstvo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Zdravstvo.Infrastructure.Validators
{
    public class CreatePacijentDTOValidator : AbstractValidator<PacijentDTO.CreatePacijentDTO>
    {
        private readonly ZdravstvoContext _db;

        public CreatePacijentDTOValidator(ZdravstvoContext db)
        {
            _db = db;

            RuleFor(x => x.Ime)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Ime je obavezno")
                .MaximumLength(100);

            RuleFor(x => x.Prezime)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Prezime je obavezno")
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email je obavezan")
                .EmailAddress().WithMessage("Neispravan format email adrese")
                .MustAsync(async (email, ct) => !await _db.Pacijenti.AnyAsync(p => p.Email == email, ct))
                .WithMessage("Pacijent sa unesenom email adresom veæ postoji");

            RuleFor(x => x.BrojTelefona)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Broj telefona je obavezan")
                .Matches(@"^\+?\d{6,15}$").WithMessage("Neispravan format broja telefona");

            RuleFor(x => x.JMBG)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("JMBG je obavezan")
                .Length(13).WithMessage("JMBG mora imati 13 cifara")
                .Matches(@"^\d{13}$").WithMessage("JMBG mora sadržavati samo cifre")
                .MustAsync(async (jmbg, ct) => !await _db.Pacijenti.AnyAsync(p => p.JMBG == jmbg, ct))
                .WithMessage("Pacijent sa unesenim JMBG-om veæ postoji");

            RuleFor(x => x.DatumRodjenja)
                .LessThan(DateTime.Now).WithMessage("Datum rodjenja mora biti u prošlosti");
        }
    }
}
