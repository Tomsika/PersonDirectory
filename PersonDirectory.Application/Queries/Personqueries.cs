using MediatR;
using PersonDirectory.Application.Dtos;
using PersonDirectory.Domain.Enums;

namespace PersonDirectory.Application.Queries
{
    public class GetByIdQuery : IRequest<PersonDto>
    {
        public int Id { get; set; }
    }

    public class GetAllQuery : IRequest<PersonListItemDtoWithTotalCount>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string SearchText { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender? Gender { get; set; }

        public string PersonalNumber { get; set; }

        public DateTime? BirthDateFrom { get; set; }

        public DateTime? BirthDateTo { get; set; }

        public int? CityId { get; set; }
    }

    public class GetRelationsReportQuery : IRequest<List<RelatedPersonsReportDto>>;
}