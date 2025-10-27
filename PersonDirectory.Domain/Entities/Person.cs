using PersonDirectory.Domain.Enums;

namespace PersonDirectory.Domain.Entities
{
    public class Person
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string PersonalNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public int CityId { get; set; }

        public ICollection<PhoneNumber> PhoneNumbers { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<PersonRelation> Relations { get; set; }
    }
}