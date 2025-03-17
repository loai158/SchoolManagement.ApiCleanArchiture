using MediatR;
using SchoolManagement.Core.Features.User.Queries.Responses;
using SchoolManagement.Core.Wrapper;

namespace SchoolManagement.Core.Features.User.Queries.Models
{
    public class GetUsersPaginatedQuery : IRequest<PaginatedResult<GetUsersPaginatedResponse>>
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }


    }
}
