using PersonDirectory.Domain.Interfaces;
using PersonDirectory.Infrastructure.Data;

namespace PersonDirectory.Infrastructure.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PersonDirectoryDbContext _context;

        public UnitOfWork(PersonDirectoryDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}