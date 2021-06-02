namespace Logic.Dto
{
    public class OperationResult
    {
        public bool IsSuccess { get; private set; }

        public string ErrorMessage { get; private set; }

        public OperationResult(bool success, string errorMessage)
        {
            IsSuccess = success;
            ErrorMessage = errorMessage;
        }

        public static OperationResult Success() => new OperationResult(true, string.Empty);

        public static OperationResult Fail(string message) => new OperationResult(false, message);
    }

    public class OperationResult<T> : OperationResult
    {
        public T Data { get; private set; }

        public OperationResult(bool success, string errorMessage, T data)
            :base(success, errorMessage)
        {
            Data = data;
        }

        public static OperationResult<T> Success(T data) => new OperationResult<T>(true, string.Empty, data);

        public static OperationResult<T> Fail(string message) => new OperationResult<T>(false, message, default);
    }
}
