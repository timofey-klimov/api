using Newtonsoft.Json;

namespace Api.Dto.Response
{
    public class ApiResponse
    {
        [JsonProperty("code")]
        public int Code { get; private set; }

        [JsonProperty("message")]
        public string Message { get; private set; }

        public ApiResponse(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public static ApiResponse Success() => new ApiResponse(0, null);

        public static ApiResponse Fail(int code, string message) => new ApiResponse(code, message);
    }

    public class ApiResponse<T> : ApiResponse
    {
        [JsonProperty("data")]
        public T Data { get; private set; }
        public ApiResponse(T data, int code, string message)
            :base(code, message)
        {
            Data = data;
        }

        public static new ApiResponse<T> Success(T data) => new ApiResponse<T>(data, 0, null);
    }
}
