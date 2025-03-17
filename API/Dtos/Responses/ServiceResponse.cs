using API.Enums;

namespace API.Dtos
{
    public class ServiceResponse<T>
    {
        public T? Data {get;set;}
        public bool Success {get;set;} = true;

        public EErrorType ErrorType {get;set;} = EErrorType.SUCCESS;
    }
}