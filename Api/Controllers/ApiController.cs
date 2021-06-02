using Api.Dto.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public abstract class ApiController : ControllerBase
    {
        protected IMediator Mediatr;
        protected ApiController(IMediator mediator)
        {
            Mediatr = mediator;
        }

        protected virtual ApiResponse Ok()
        {
            return ApiResponse.Success();
        }

        protected virtual ApiResponse<T> Ok<T>(T data)
        {
            return ApiResponse<T>.Success(data);
        }
    }
}
