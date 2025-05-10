namespace API.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(DataContext context) : base(context)
        {
        }
    }
}
