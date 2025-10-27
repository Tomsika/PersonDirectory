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

        public City City { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<PhoneNumber> PhoneNumbers { get; set; }

        public ICollection<PersonRelation> Relations { get; set; }

        public PersonStatus Status { get; set; }

        public Person()
        {

        }

        public Person(
           string firstName,
           string lastName,
           Gender gender,
           string personalNumber,
           DateTime birthDate,
           int cityId,
           ICollection<PhoneNumber> phoneNumbers
           )
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            PersonalNumber = personalNumber;
            BirthDate = birthDate;
            CityId = cityId;
            PhoneNumbers = phoneNumbers;
            Status = PersonStatus.Active;
        }

        public static Person Create(
            string firstName,
            string lastName,
            Gender gender,
            string personalNumber,
            DateTime birthDate,
            int cityId,
            ICollection<PhoneNumber> phoneNumbers)
            => new Person(firstName, lastName, gender, personalNumber, birthDate, cityId, phoneNumbers);

        public void Update(
            string firstName,
            string lastName,
            Gender gender,
            string personalNumber,
            DateTime birthDate,
            int cityId,
            ICollection<PhoneNumber> phoneNumbers)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            PersonalNumber = personalNumber;
            BirthDate = birthDate;
            CityId = cityId;
            PhoneNumbers = phoneNumbers;
        }

        public void Delete()
        {
            Status = PersonStatus.Deleted;
        }

        public void AddRelation(int relatedPersonId, RelationType relationType)
        {
            var relation = PersonRelation.Create(Id, relatedPersonId, relationType);

            Relations.Add(relation);
        }

        public void DeleteRelation(PersonRelation relation)
        {
            Relations.Remove(relation);
        }
    }
}