using MediatR;
using SchoolManagement.Core.Basics;
using SchoolManagement.Core.Features.Email.Commands.Models;
using SchoolManagement.Service.Abstacts;

namespace SchoolManagement.Core.Features.Email.Commands.Handler
{
    public class EmailsCommandHandler : ResponseHandler,
        IRequestHandler<SendEmailCommand, Response<string>>
    {
        private readonly IEmailServices _emailServices;

        public EmailsCommandHandler(IEmailServices emailServices)
        {
            this._emailServices = emailServices;
        }
        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var response = await _emailServices.SendEmailAsync(request.Email, request.Message, "");
            if (response == "Success")
                return Success<string>("");
            return BadRequest<string>("SendEmailFailed");
        }
    }
}
