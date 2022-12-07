using ContactApplication.Data;
using ContactApplication.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;


namespace ContactApplication.Validators
{
    public class CreateContactValidator : AbstractValidator<Contact>
    {
        public CreateContactValidator(ContactContext dbContext)
        {
            //Walidacja encji Email 
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("To pole nie może być puste!")
                .EmailAddress();

            //Walidacja encji Password
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Twoje hasło nie może być puste!")
                .MinimumLength(8).WithMessage("Twoje hasło musi zawierać przynajmniej 8 znaków")
                .Matches(@"[A-Z]+").WithMessage("Twoje hasło musi zawierać przynajmniej 1 wielką literę")
                .Matches(@"[a-z]+").WithMessage("Twoje hasło musi zawierać przynajmniej 1 małą literę")
                .Matches(@"[0-9]+").WithMessage("Twoje hasło musi zawierać przynajmniej 1 cyfrę")
                .Matches(@"[\!\?\*\.]+").WithMessage("Twoje hasło musi zawierać przynajmniej 1 znak specjalny");

            //Walidacja encji PhoneNumber
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("To pole nie może być puste!");

            //Walidacja encji FirstName
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("To pole nie może być puste!");

            //Walidacja encji LastName
            RuleFor(x => x.LaseName)
                .NotEmpty().WithMessage("To pole nie może być puste!");

            //Walidacja encji Email, sprawdzenie czy email jest już w bazie danych
            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Contacts.Any(u => u.Email == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "This email is taken");
                    }
                });
        }
    }
}
