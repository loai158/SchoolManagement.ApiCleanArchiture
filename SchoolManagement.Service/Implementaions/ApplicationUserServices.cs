using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Data.Entities.Identity;
using SchoolManagement.Infrastructure.UnitOfWorks;
using SchoolManagement.Service.Abstacts;

namespace SchoolManagement.Service.Implementaions
{
    public class ApplicationUserServices : IApplicationUserServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailServices _emailsService;
        private readonly IUrlHelper _urlHelper;

        public ApplicationUserServices(UserManager<ApplicationUser> userManager,
                                        IUnitOfWork unitOfWork,
                                      IHttpContextAccessor httpContextAccessor,
                                      IEmailServices emailsService,
                                      IUrlHelper urlHelper)
        {
            _userManager = userManager;
            this._unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _emailsService = emailsService;

            _urlHelper = urlHelper;
        }

        public async Task<string> AddUserAsync(ApplicationUser user, string password)
        {
            var trans = await _unitOfWork.BeginTransactionAsync();
            try
            {
                //if Email is Exist
                var existUser = await _userManager.FindByEmailAsync(user.Email);
                //email is Exist
                if (existUser != null) return "EmailIsExist";

                //if username is Exist
                var userByUserName = await _userManager.FindByNameAsync(user.UserName);
                //username is Exist
                if (userByUserName != null) return "UserNameIsExist";
                //Create
                var createResult = await _userManager.CreateAsync(user, password);
                //Failed
                if (!createResult.Succeeded)
                    return string.Join(",", createResult.Errors.Select(x => x.Description).ToList());

                await _userManager.AddToRoleAsync(user, "User");

                //Send Confirm Email
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var resquestAccessor = _httpContextAccessor.HttpContext.Request;
                var returnUrl = resquestAccessor.Scheme + "://" + resquestAccessor.Host;//+ _urlHelper.Action("ConfirmEmail", "Authentication", new { userId = user.Id, code = code });
                var message = $"To Confirm Email Click Link: <a href='{returnUrl}'>Link Of Confirmation</a>";
                //$"/Api/V1/Authentication/ConfirmEmail?userId={user.Id}&code={code}";
                //message or body
                await _emailsService.SendEmailAsync(user.Email, message, "ConFirm Email");

                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "Failed";
            }

        }


    }
}