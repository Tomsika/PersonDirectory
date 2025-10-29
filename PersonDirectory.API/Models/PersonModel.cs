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

        public string ImageUrl { get; set; }

        public ICollection<AddPersonPhoneNumberModel> PhoneNumbers { get; set; }
    }
    public class AddPersonModel : BasePersonModel
    {
    }

    public class UpdatePersonModel : BasePersonModel
    {
        public int Id { get; set; }
    }

    public class DeletePersonModel
    {
        public int Id { get; set; }
    }

    public class AddPersonRelationModel
    {
        public int PersonId { get; set; }

        public RelationType RelationType { get; set; }

        public int RelatedPersonId { get; set; }
    }

    public class DeletePersonRelationModel
    {
        public int Id { get; set; }

        public int PersonId { get; set; }
    }

    public class AddPersonPhoneNumberModel
    {
        public PhoneType Type { get; set; }

        public string Number { get; set; }
    }
}