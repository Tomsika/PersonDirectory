using PersonDirectory.Domain.Entities;

namespace PersonDirectory.Domain.Interfaces
{
    public interface IPersonWriteRepository
    {
        Task Add(Person person, CancellationToken cancellationToken);
    }

    public interface IPersonReadRepository
    {
        Task<Person> GetById(int id, CancellationToken cancellationToken);

        Task<bool> PersonalNumberExists(string personalNumber, int? excludePersonId, CancellationToken cancellationToken);

        Task<bool> RelationExists(int personId, int realatedPersonId, CancellationToken cancellationToken);
    }
}