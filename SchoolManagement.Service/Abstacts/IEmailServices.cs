namespace SchoolManagement.Service.Abstacts
{
    public interface IEmailServices
    {
        public Task<string> SendEmailAsync(string email, string message, string? reason);
    }
}
