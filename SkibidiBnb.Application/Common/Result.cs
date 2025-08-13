namespace SkibidiBnb.Application.Common
{
    public class Result<T>(bool isSuccess, T value, string error)
    {
        public bool IsSuccess { get; set; } = isSuccess;
        public T Value { get; set; } = value;
        public string Error { get; set; } = error;

        public static Result<T> Success(T value)
        {
            return new Result<T>(true, value, string.Empty);
        }

        public static Result<T> Failure(string error)
        {
            return new Result<T>(false, default!, error);
        }
    }
}
