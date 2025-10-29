namespace PersonDirectory.Application.Dtos
{
    public class PersonDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string PersonalNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public int CityId { get; set; }

        public string CityName { get; set; }

        public string ImagePath { get; set; }

        public List<PhoneNumberDto> PhoneNumbers { get; set; }

        public List<RelatedPersonDto> RelatedPersons { get; set; }
    }

    public class PersonListItemDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string PersonalNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public string City { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<string> PhoneNumbers { get; set; }
    }

    public class PersonListItemDtoWithTotalCount
    {
        public int TotalCount { get; set; }

        public IEnumerable<PersonListItemDto> Persons { get; set; }
    }

    public class PhoneNumberDto
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Number { get; set; }
    }

    public class RelatedPersonDto
    {
        public int Id { get; set; }

        public int RelatedPersonId { get; set; }

        public string RelatedPersonFirstName { get; set; }

        public string RelatedPersonLastName { get; set; }

        public string RelationType { get; set; }
    }

    public class RelatedPersonsReportDto
    {
        public int PersonId { get; set; }

        public string PersonFullName { get; set; }

        public Dictionary<string, int> RelationTypeCounts { get; set; }
    }
}