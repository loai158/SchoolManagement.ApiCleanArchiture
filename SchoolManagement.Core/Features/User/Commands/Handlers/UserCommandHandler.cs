using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Core.Basics;
using SchoolManagement.Core.Features.User.Commands.Models;
using SchoolManagement.Data.Entities.Identity;
using SchoolManagement.Service.Abstacts;

namespace SchoolManagement.Core.Features.User.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler,
        IRequestHandler<AddUserCommand, Response<string>>,
        IRequestHandler<DeleteUserCommand, Response<string>>,
        IRequestHandler<EditUserCommand, Response<string>>,
        IRequestHandler<ChangeUserPAsswordCommand, Response<string>>
    {
        private readonly IEmailServices _emailServices;
        private readonly IApplicationUserServices _applicationUserServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserCommandHandler(IEmailServices emailServices, IApplicationUserServices applicationUserServices, IHttpContextAccessor httpContextAccessor, IMapper mapper, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._emailServices = emailServices;
            this._applicationUserServices = applicationUserServices;
            this._httpContextAccessor = httpContextAccessor;
            this._mapper = mapper;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //check if already exist
            var identityUser = _mapper.Map<ApplicationUser>(request);
            //Create
            var createResult = await _applicationUserServices.AddUserAsync(identityUser, request.Password);
            switch (createResult)
            {
                case "EmailIsExist": return BadRequest<string>("EmailIsExist");
                case "UserNameIsExist": return BadRequest<string>("UserNameIsExist ");
                case "ErrorInCreateUser": return BadRequest<string>("FaildToAddUser ");
                case "Failed": return BadRequest<string>("TryToRegisterAgain");
                case "Success": return Success<string>("");
                default: return BadRequest<string>(createResult);

            }
        }

        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            //find if emil exist first
            var userDb = await _userManager.FindByIdAsync(request.Id);
            if (userDb != null)
            {
                //map first the create
                var mapper = _mapper.Map(request, userDb);
                var result = await _userManager.UpdateAsync(mapper);
                if (result.Succeeded)
                {
                    return Success("User Eidted Successfully");
                }
                else
                {
                    return BadRequest<string>("Faild to edit the user");
                }

            }
            else
            {
                return NotFound<string>();
            }

        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userDb = await _userManager.FindByIdAsync(request.Id);
            if (userDb != null)
            {
                var result = await _userManager.DeleteAsync(userDb);
                if (result.Succeeded)
                {
                    return Success("Deleted Succefully");
                }
                else
                {
                    return BadRequest<string>("Some thing Wrong Happen");
                }
            }
            else
            {
                return NotFound<string>();
            }
        }

        public async Task<Response<string>> Handle(ChangeUserPAsswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
                return Success("Password Changed Successfully");
            }
            else
            {
                return NotFound<string>();
            }
        }




    }
}
