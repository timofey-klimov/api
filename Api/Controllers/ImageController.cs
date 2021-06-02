using Api.Attributes;
using Api.Dto.Response;
using Api.Filters;
using Logic.Operations.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [AuthFilter]
    [Route("api/[controller]")]
    public class ImageController : ApiAuthUserController
    {
        public ImageController(IMediator mediator) 
            :base(mediator)
        {

        }

        [HttpPost("add-image")]
        [LogWithoutBody]
        public async Task<ApiResponse> AddNewImage(IFormFile file)
        {
            var result = await Mediatr.Send(new AddImgeRequest(User.Id, file.FileName, file.OpenReadStream()));

            return Ok();
        }
    }
}
