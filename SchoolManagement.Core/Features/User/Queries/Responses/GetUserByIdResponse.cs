﻿namespace SchoolManagement.Core.Features.User.Queries.Responses
{
    public class GetUserByIdResponse
    {
        public string FullName { get; set; }
        public string? Address { get; set; }

        public string? Email { get; set; }
    }
}
