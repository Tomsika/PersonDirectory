using PersonDirectory.API.Models;
using FluentValidation;
using System.Text.RegularExpressions;

namespace PersonDirectory.API.Validations
{
    public class BasePersonValidator<T> : AbstractValidator<T> where T : BasePersonModel
    {
        private static readonly Regex LatinOnly =
        new Regex(@"^[A-Za-z]{2,50}$", RegexOptions.Compiled);

        private static readonly Regex GeorgianOnly =
        new Regex(@"^[\u10A0-\u10C5\u10D0-\u10FF\u1C90-\u1CBF]{2,50}$",
                 RegexOptions.Compiled);

        private static readonly Regex DigitsOnly =
        new(@"^\d{11}$", RegexOptions.Compiled);

        public BasePersonValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("სახელი სავალდებულოა.")
                .Length(2, 50).WithMessage("სახელი უნდა იყოს 2-დან 50 სიმბოლომდე.")
                .Must(BeLatinOrGeorgianOnly)
                    .WithMessage("სახელი უნდა შედგებოდეს მხოლოდ ქართული ან მხოლოდ ლათინური ასოებისგან.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("გვარი სავალდებულოა.")
                .Length(2, 50).WithMessage("გვარი უნდა იყოს 2-დან 50 სიმბოლომდე.")
                .Must(BeLatinOrGeorgianOnly)
                    .WithMessage("გვარი უნდა შედგებოდეს მხოლოდ ქართული ან მხოლოდ ლათინური ასოებისგან.");

            RuleFor(x => x.PersonalNumber)
            .NotEmpty().WithMessage("პირადი ნომერი სავალდებულოა.")
            .Matches(DigitsOnly)
             .WithMessage("პირადი ნომერი უნდა შედგებოდეს ზუსტად 11 ციფრისგან.");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("დაბადების თარიღი სავალდებულოა")
                .Must(BeAtLeast18YearsOld)
                .WithMessage("მომხმარებელი უნდა იყოს მინიმუმ 18 წლის.");
        }

        private static bool BeLatinOrGeorgianOnly(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;

            return LatinOnly.IsMatch(value) || GeorgianOnly.IsMatch(value);
        }
        private static bool BeAtLeast18YearsOld(DateTime birthDate)
        {
            return birthDate <= DateTime.Today.AddYears(-18);
        }
    }

    public class AddPersonValidation : BasePersonValidator<AddPersonModel>
    {

    }

    public class UpdatePersonValidation : BasePersonValidator<UpdatePersonModel>
    {
    }
}