using Microsoft.EntityFrameworkCore;
using PersonDirectory.Domain.Entities;
using PersonDirectory.Domain.Interfaces;
using PersonDirectory.Infrastructure.Data;

namespace PersonDirectory.Infrastructure.Repositories
{
    public class PersonReadRepository : IPersonReadRepository
    {
        protected readonly PersonDirectoryDbContext _context;

        public PersonReadRepository(PersonDirectoryDbContext context)
        {
            _context = context;
        }

        public async Task<Person> GetById(int id, CancellationToken cancellationToken)
        {
            return await _context.Persons
                .Include(p => p.City)
                .Include(p => p.PhoneNumbers)
                .Include(p => p.Relations)
                    .ThenInclude(r => r.RelatedPerson)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> PersonalNumberExistsAsync(string personalNumber, int? excludePersonId, CancellationToken cancellationToken)
        {
            var query = _context.Persons.Where(p => p.PersonalNumber == personalNumber);

            if (excludePersonId.HasValue)
            {
                query = query.Where(p => p.Id != excludePersonId.Value);
            }

            return await query.AnyAsync(cancellationToken);
        }
    }
}
