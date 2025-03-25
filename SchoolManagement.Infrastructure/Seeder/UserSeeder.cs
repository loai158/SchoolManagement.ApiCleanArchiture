using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data.Entities.Identity;

namespace SchoolManagement.Infrastructure.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var usersCount = await userManager.Users.CountAsync();
            if (usersCount <= 0)
            {
                var defaultUser = new ApplicationUser()
                {
                    UserName = "admin",
                    Email = "admin@project.com",
                    FullName = "schoolProject",
                    PhoneNumber = "123456",
                    Address = "Egypt",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                await userManager.CreateAsync(defaultUser, "M123_m");
                await userManager.AddToRoleAsync(defaultUser, "Admin");
            }
        }
    }
}
