namespace API.Dtos.Responses
{
    public class Pagination<T>
    {
        public List<T> Data { get; set; } = new List<T>();
        public int Page { get; set; }
        public int Total { get; set; }
        public EErrorType ErrorType { get; set; } = EErrorType.SUCCESS;
    }
}