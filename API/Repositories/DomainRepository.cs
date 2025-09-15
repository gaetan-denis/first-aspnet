namespace API.Repositories
{
    public class DomainRepository : BaseRepository<Domain>, IDomainRepository
    {
        public DomainRepository(DataContext context) : base(context)
        {
        }
    }
}
