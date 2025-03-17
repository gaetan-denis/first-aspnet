namespace API.Repositories
{
    public class DomainRepository : IDomainRepository
    {
        private readonly DataContext _context;

        public DomainRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Domain?> GetByIdAsync(int id)
        {
            return await _context.Domains.FindAsync(id);  
        }

        public async Task<IEnumerable<Domain>> GetAllAsync()
        {
            return await _context.Domains.ToListAsync();  
        }

        public async Task<Domain> AddAsync(Domain domain)
        {
            await _context.Domains.AddAsync(domain);
            await _context.SaveChangesAsync();
            return domain;
        }

        public async Task<Domain> UpdateAsync(Domain domain)
        {
            _context.Domains.Update(domain);
            await _context.SaveChangesAsync();
            return domain;
        }

        public async Task DeleteAsync(int id)
        {
            var domain = await GetByIdAsync(id);
            if (domain != null)
            {
                _context.Domains.Remove(domain);
                await _context.SaveChangesAsync();
            }
        }
    }
}
