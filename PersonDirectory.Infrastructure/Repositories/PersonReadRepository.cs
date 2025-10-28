using Microsoft.EntityFrameworkCore;
using PersonDirectory.Domain.Entities;
using PersonDirectory.Domain.Enums;
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

        public async Task<IEnumerable<Person>> GetALLRelations(CancellationToken cancellationToken)
        {
            return await _context.Persons
                .Include(p => p.Relations)
                    .ThenInclude(r => r.RelatedPerson)
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<(IEnumerable<Person> Items, int TotalCount)> GetAll(
          int pageNumber,
          int pageSize,
          string searchText,
          string firstName,
          string lastName,
          string personalNumber,
          Gender? gender,
          DateTime? birthDateFrom,
          DateTime? birthDateTo,
          int? cityId)
        {
            var query = _context.Persons
                .Include(p => p.City)
                .Include(p => p.PhoneNumbers)
                .Include(p => p.Relations)
                    .ThenInclude(r => r.RelatedPerson)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                query = query.Where(p =>
                    EF.Functions.Like(p.FirstName, $"%{searchText}%") ||
                    EF.Functions.Like(p.LastName, $"%{searchText}%") ||
                    EF.Functions.Like(p.PersonalNumber, $"%{searchText}%"));
            }

            if (!string.IsNullOrWhiteSpace(firstName))
            {
                query = query.Where(p => EF.Functions.Like(p.FirstName, $"%{firstName}%"));
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                query = query.Where(p => EF.Functions.Like(p.LastName, $"%{lastName}%"));
            }

            if (!string.IsNullOrWhiteSpace(personalNumber))
            {
                query = query.Where(p => EF.Functions.Like(p.PersonalNumber, $"%{personalNumber}%"));
            }

            if (gender.HasValue)
            {
                query = query.Where(p => p.Gender == gender.Value);
            }

            if (birthDateFrom.HasValue)
                query = query.Where(p => p.BirthDate >= birthDateFrom.Value);

            if (birthDateTo.HasValue)
                query = query.Where(p => p.BirthDate <= birthDateTo.Value);

            if (cityId.HasValue)
            {
                query = query.Where(p => p.CityId == cityId.Value);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<bool> PersonalNumberExists(string personalNumber, int? excludePersonId, CancellationToken cancellationToken)
        {
            var query = _context.Persons.Where(p => p.PersonalNumber == personalNumber);

            if (excludePersonId.HasValue)
            {
                query = query.Where(p => p.Id != excludePersonId.Value);
            }

            return await query.AnyAsync(cancellationToken);
        }

        public async Task<bool> RelationExists(int personId, int realatedPersonId, CancellationToken cancellationToken)
        {
            var query = _context.PersonRelations.Where(p => p.PersonId == personId && p.RelatedPersonId == realatedPersonId);

            return await query.AnyAsync(cancellationToken);
        }
    }
}