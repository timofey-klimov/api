namespace Domain.Exceptions.ErrorCodes
{
    public enum ErrorCode
    {
        UserNotFound = 100,
        UserAlreadyExists = 101,

        UnAuthorized = 200,
        TokenValidationFailed = 201,
        NotificationTemplateWasNotFind = 202,
        IncorrectCode = 203,

        OperationFailed = 600,

        ErrorInDbWhileSaving = 8888,
        Global = 9999
    }
}
