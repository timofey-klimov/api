namespace Domain.Exceptions.ErrorCodes
{
    public enum ErrorCode
    {
        UserAlreadyExists = 101,
        UnAuthorized = 102,

        NotAuthorized = 200,
        TokenValidationFailed = 201,
        IncorrectCode = 203,

        EntityNotFound = 400,

        OperationFailed = 600,

        TransactionOperationFail = 7777,
        ErrorInDbWhileSaving = 8888,
        Global = 9999
    }
}
