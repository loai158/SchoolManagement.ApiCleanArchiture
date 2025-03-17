using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Core.Basics;
using SchoolManagement.Core.Features.User.Queries.Models;
using SchoolManagement.Core.Features.User.Queries.Responses;
using SchoolManagement.Core.Wrapper;
using SchoolManagement.Data.Entities.Identity;

namespace SchoolManagement.Core.Features.User.Queries.Handlers
{
    public class UserQueryHandler : ResponseHandler,
        IRequestHandler<GetUsersPaginatedQuery, PaginatedResult<GetUsersPaginatedResponse>>,
        IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>

    {
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        public UserQueryHandler(IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<PaginatedResult<GetUsersPaginatedResponse>> Handle(GetUsersPaginatedQuery request, CancellationToken cancellationToken)
        {
            var users = userManager.Users.AsQueryable();
            var PaginatedList = await mapper.ProjectTo<GetUsersPaginatedResponse>(users)
                                            .ToPaginatedListAsync(request.pageNumber, request.pageSize);
            return PaginatedList;
        }

        public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == request.Id);
            if (user == null)
            {
                return NotFound<GetUserByIdResponse>("User id is not found");
            }
            else
            {
                //map
                var result = mapper.Map<GetUserByIdResponse>(user);
                return Success(result);

            }
        }
    }
}
