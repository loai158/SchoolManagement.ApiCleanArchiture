﻿using MediatR;
using SchoolManagement.Core.Basics;
using SchoolManagement.Data.Helper;

namespace SchoolManagement.Core.Features.Authentication.Commands.Models
{
    public class SignInCommand : IRequest<Response<JwtAuthResult>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
