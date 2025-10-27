using PersonDirectory.Domain.Entities;
using PersonDirectory.Domain.Interfaces;
using PersonDirectory.Infrastructure.Data;

namespace PersonDirectory.Infrastructure.Repositories
{
    public class PersonWriteRepository : IPersonWriteRepository
    {
        protected readonly PersonDirectoryDbContext _context;

        public PersonWriteRepository(PersonDirectoryDbContext context)
        {
            _context = context;
        }

        public async Task Add(Person person, CancellationToken cancellationToken)
        {
            await _context.Persons.AddAsync(person, cancellationToken);
        }
    }
}