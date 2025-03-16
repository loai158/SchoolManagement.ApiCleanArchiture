using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Core.Basics;
using System.Net;

namespace SchoolManagement.Api.Base
{
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        private IMediator _mediatorInstance;
        protected IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();
        public ObjectResult NewResult<T>(Response<T> response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new OkObjectResult(response);
                case HttpStatusCode.Created:
                    return new CreatedResult(string.Empty, response);
                case HttpStatusCode.Unauthorized:
                    return new UnauthorizedObjectResult(response);
                case HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(response);
                case HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(response);
                case HttpStatusCode.Accepted:
                    return new AcceptedResult(string.Empty, response);
                case HttpStatusCode.UnprocessableEntity:
                    return new UnprocessableEntityObjectResult(response);
                default:
                    return new BadRequestObjectResult(response);
            }
        }
    }
}
