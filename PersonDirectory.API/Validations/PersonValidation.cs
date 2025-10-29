using FluentValidation;
using Microsoft.Extensions.Localization;
using PersonDirectory.API.Localization;
using PersonDirectory.API.Models;
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

        public BasePersonValidator(IStringLocalizer<ValidationMessages> localizer)
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(localizer["FirstNameRequired"])
                .Length(2, 50).WithMessage(localizer["FirstNameLength"])
                .Must(BeLatinOrGeorgianOnly)
                    .WithMessage(localizer["FirstNameAlphabet"]);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(localizer["LastNameRequired"])
                .Length(2, 50).WithMessage(localizer["LastNameLength"])
                .Must(BeLatinOrGeorgianOnly)
                    .WithMessage(localizer["LastNameAlphabet"]);

            RuleFor(x => x.PersonalNumber)
                .NotEmpty().WithMessage(localizer["PersonalNumberRequired"])
                .Matches(DigitsOnly)
                .WithMessage(localizer["PersonalNumberDigits"]);

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage(localizer["BirthDateRequired"])
                .Must(BeAtLeast18YearsOld)
                .WithMessage(localizer["BirthDateMinAge"]);

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage(localizer["GenderRequired"])
                .IsInEnum()
                .WithMessage(localizer["GenderInvalid"]);

            RuleForEach(x => x.PhoneNumbers)
                .SetValidator(new AddPersonPhoneNumberValidation(localizer));
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
        public AddPersonValidation(IStringLocalizer<ValidationMessages> localizer)
            : base(localizer)
        {

        }
    }

    public class UpdatePersonValidation : BasePersonValidator<UpdatePersonModel>
    {
        public UpdatePersonValidation(IStringLocalizer<ValidationMessages> localizer)
            : base(localizer)
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(localizer["IdRequired"]);
        }
    }

    public class DeletePersonValidation : AbstractValidator<DeletePersonModel>
    {
        public DeletePersonValidation(IStringLocalizer<ValidationMessages> localizer)
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(localizer["IdRequired"]);
        }
    }
    public class AddPersonRelationValidation : AbstractValidator<AddPersonRelationModel>
    {
        public AddPersonRelationValidation(IStringLocalizer<ValidationMessages> localizer)
        {
            RuleFor(x => x.PersonId)
                .NotEmpty().WithMessage(localizer["PersonIdRequired"])
                .GreaterThan(0);

            RuleFor(x => x.RelationType)
                .NotEmpty()
                .IsInEnum()
                .WithMessage(localizer["RelationTypeRequired"]);

            RuleFor(x => x.RelatedPersonId)
                .NotEmpty().WithMessage(localizer["RelatedPersonIdRequired"])
                .GreaterThan(0);
        }
    }

    public class DeletePersonRelationValidation : AbstractValidator<DeletePersonRelationModel>
    {
        public DeletePersonRelationValidation(IStringLocalizer<ValidationMessages> localizer)
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(localizer["IdRequired"]);
            RuleFor(x => x.PersonId)
                .NotEmpty().WithMessage(localizer["PersonIdRequired"]);
        }
    }

    public class AddPersonPhoneNumberValidation : AbstractValidator<AddPersonPhoneNumberModel>
    {
        public AddPersonPhoneNumberValidation(IStringLocalizer<ValidationMessages> localizer)
        {
            RuleFor(x => x.Type)
                .NotEmpty()
                .IsInEnum()
                .WithMessage(localizer["PhoneTypeRequired"]);
            RuleFor(x => x.Number)
                .NotEmpty().WithMessage(localizer["PhoneNumberRequired"])
                .Length(4, 50).WithMessage(localizer["PhoneNumberLength"]);
        }
    }
}