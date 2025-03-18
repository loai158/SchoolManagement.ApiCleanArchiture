using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Core.Basics;
using SchoolManagement.Core.Features.User.Commands.Models;
using SchoolManagement.Data.Entities.Identity;

namespace SchoolManagement.Core.Features.User.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler,
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserCommandHandler(IMapper mapper, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._mapper = mapper;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //check if already exist
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                //map first
                var registerUser = _mapper.Map<ApplicationUser>(request);
                var result = await _userManager.CreateAsync(registerUser, request.Password);
                if (result.Succeeded)
                {
                    return Created("User Rigistered Successfully");
                }
                else
                {
                    return BadRequest<string>("Faild to add new user");
                }
            }
            else
            {
                return BadRequest<string>("Email Already Exist");
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
    }
}
