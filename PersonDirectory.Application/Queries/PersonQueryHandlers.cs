using MediatR;
using PersonDirectory.Application.Dtos;
using PersonDirectory.Domain.Entities;
using PersonDirectory.Domain.Interfaces;

namespace PersonDirectory.Application.Queries
{
    public class PersonQueryHandlers :
        IRequestHandler<GetByIdQuery, PersonDto>,
        IRequestHandler<GetAllQuery, PersonListItemDtoWithTotalCount>,
        IRequestHandler<GetRelationsReportQuery, List<RelatedPersonsReportDto>>
    {
        private readonly IPersonReadRepository _personReadRepository;

        public PersonQueryHandlers(
            IPersonReadRepository personReadRepository)
        {
            _personReadRepository = personReadRepository;
        }
        public async Task<PersonDto> Handle(GetByIdQuery query, CancellationToken cancellationToken)
        {
            var person = await _personReadRepository.GetById(query.Id, cancellationToken)
              ?? throw new KeyNotFoundException($"Person with id {query.Id} not found");

            var personDto = MapToDto(person, cancellationToken);
            return personDto;
        }

        public async Task<PersonListItemDtoWithTotalCount> Handle(GetAllQuery query, CancellationToken cancellationToken)
        {
            var persons = await _personReadRepository.GetAll(
                query.PageNumber,
                query.PageSize,
                query.SearchText,
                query.FirstName,
                query.LastName,
                query.PersonalNumber,
                query.Gender,
                query.BirthDateFrom,
                query.BirthDateTo,
                query.CityId);

            return new PersonListItemDtoWithTotalCount { Persons = MapToListItemDto(persons.Items), TotalCount = persons.TotalCount };
        }

        public async Task<List<RelatedPersonsReportDto>> Handle(GetRelationsReportQuery query, CancellationToken cancellationToken)
        {
            var persons = await _personReadRepository.GetALLRelations(cancellationToken);
            var report = new List<RelatedPersonsReportDto>();

            foreach (var person in persons)
            {
                var groupedRelation = person.Relations.GroupBy(x => x.RelationType.ToString())
                      .ToDictionary(g => g.Key, g => g.Count());

                var personReport = new RelatedPersonsReportDto
                {
                    PersonId = person.Id,
                    PersonFullName = $"{person.FirstName} {person.LastName}",
                    RelationTypeCounts = groupedRelation
                };

                report.Add(personReport);
            }

            return report;
        }

        #region Private

        private PersonDto MapToDto(Person person, CancellationToken cancellationToken)
        {
            return new PersonDto
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Gender = person.Gender.ToString(),
                PersonalNumber = person.PersonalNumber,
                BirthDate = person.BirthDate,
                CityId = person.CityId,
                CityName = person.City?.Name ?? string.Empty,
                ImagePath = person.ImageUrl,
                PhoneNumbers = person.PhoneNumbers.Select(p => new PhoneNumberDto
                {
                    Id = p.Id,
                    Type = p.Type.ToString(),
                    Number = p.Number
                }).ToList(),
                RelatedPersons = person.Relations.Select(r => new RelatedPersonDto
                {
                    Id = r.Id,
                    RelatedPersonId = r.RelatedPersonId,
                    RelatedPersonFirstName = r.RelatedPerson.FirstName,
                    RelatedPersonLastName = r.RelatedPerson.LastName,
                    RelationType = r.RelationType.ToString()
                }).ToList()
            };
        }

        private IEnumerable<PersonListItemDto> MapToListItemDto(IEnumerable<Person> person)
        {
            return person.Select(x =>
             {
                 return new PersonListItemDto
                 {
                     Id = x.Id,
                     FirstName = x.FirstName,
                     LastName = x.LastName,
                     PersonalNumber = x.PersonalNumber,
                     Gender = x.Gender.ToString(),
                     BirthDate = x.BirthDate,
                     City = x.City.Name,
                     ImageUrl = x.ImageUrl,
                     PhoneNumbers = x.PhoneNumbers.Select(y => $"{y.Type.ToString()}-{y.Number}")
                 };
             });
        }

        #endregion
    }
}