using PersonDirectory.Domain.Entities;
using PersonDirectory.Domain.Enums;

namespace PersonDirectory.Domain.Interfaces
{
    public interface IPersonWriteRepository
    {
        Task Add(Person person, CancellationToken cancellationToken);
    }

    public interface IPersonReadRepository
    {
        Task<Person> GetById(int id, CancellationToken cancellationToken);

        Task<(IEnumerable<Person> Items, int TotalCount)> GetAll(
          int pageNumber,
          int pageSize,
          string searchText,
          string firstName,
          string lastName,
          string personalNumber,
          Gender? gender,
          DateTime? birthDateFrom,
          DateTime? birthDateTo,
          int? cityId);

        Task<IEnumerable<Person>> GetALLRelations(CancellationToken cancellationToken);

        Task<bool> PersonalNumberExists(string personalNumber, int? excludePersonId, CancellationToken cancellationToken);

        Task<bool> RelationExists(int personId, int realatedPersonId, CancellationToken cancellationToken);
    }
}