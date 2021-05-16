namespace Logic.Dto
{
    public class OperationResult
    {
        public bool Succsess { get; private set; }

        public string ErrorMessage { get; private set; }

        public OperationResult(bool success, string errorMessage)
        {
            Succsess = success;
            ErrorMessage = errorMessage;
        }

        public static OperationResult Success() => new OperationResult(true, string.Empty);

        public static OperationResult Fail(string message) => new OperationResult(false, message);
    }
}
