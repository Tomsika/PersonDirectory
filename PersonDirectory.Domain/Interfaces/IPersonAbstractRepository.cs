using PersonDirectory.Domain.Entities;

namespace PersonDirectory.Domain.Interfaces
{
    public interface IPersonWriteRepository
    {
        Task Add(Person person, CancellationToken cancellationToken);
    }


    public interface IPersonReadRepository
    {
        Task<bool> PersonalNumberExistsAsync(string personalNumber, int? excludePersonId, CancellationToken cancellationToken);

        Task<Person> GetById(int id, CancellationToken cancellationToken);
    }
}