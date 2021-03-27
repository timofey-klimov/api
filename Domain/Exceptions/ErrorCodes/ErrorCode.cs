namespace Domain.Exceptions.ErrorCodes
{
    public enum ErrorCode
    {
        UserNotFound = 100,
        UserAlreadyExists = 101,

        UnAuthorized = 200,
        TokenValidationFailed = 201,

        ErrorInDbWhileSaving = 8888,
        Global = 9999
    }
}
