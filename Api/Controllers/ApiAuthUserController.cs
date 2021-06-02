using Api.Model;
using MediatR;
using Utils.Extensions;

namespace Api.Controllers
{
    public class ApiAuthUserController : ApiController
    {
        public ApiAuthUserController(IMediator mediator)
            :base(mediator)
        {

        }

        protected AuthUser User => HttpContext.User.To<AuthUser>();
    }
}
