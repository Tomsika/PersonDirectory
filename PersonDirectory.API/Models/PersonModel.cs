using PersonDirectory.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PersonDirectory.API.Models
{
    public class BasePersonModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string PersonalNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public int CityId { get; set; }

        public ICollection<AddPersonPhoneNumberModel> PhoneNumbers { get; set; }
    }
    public class AddPersonModel : BasePersonModel
    {
    }

    public class UpdatePersonModel : BasePersonModel
    {
        [Required]
        public int? Id { get; set; }
    }

    public class DeletePersonModel
    {
        [Required]
        public int? Id { get; set; }
    }

    public class AddPersonRelationModel
    {
        [Required]
        public int? PersonId { get; set; }

        [Required]
        public RelationType RelationType { get; set; }

        [Required]
        public int? RelatedPersonId { get; set; }
    }

    public class DeletePersonRelationModel
    {
        [Required]
        public int? Id { get; set; }

        [Required]
        public int? PersonId { get; set; }
    }

    public class AddPersonPhoneNumberModel
    {
        [Required]
        public PhoneType Type { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "ნომერი უნდა იყოს 4-დან 50 სიმბოლომდე")]
        public string Number { get; set; }
    }
}
