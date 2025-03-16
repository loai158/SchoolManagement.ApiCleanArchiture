using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SchoolManagement.Core;
using SchoolManagement.Core.Middleware;
using SchoolManagement.Infrastructure;
using SchoolManagement.Infrastructure.Data;
using SchoolManagement.Service;
using System.Globalization;

namespace SchoolManagement.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //Localization
            builder.Services.AddControllersWithViews();
            builder.Services.AddLocalization(opt =>
            {
                opt.ResourcesPath = "Resources";
            });

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                List<CultureInfo> supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("de-DE"),
                    new CultureInfo("fr-FR"),
                    new CultureInfo("ar-EG")
                };

                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            // Connection to SQL Server
            builder.Services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
            });
            #region Dependency Injection
            builder.Services
                .AddInfrastructureDependences()
                .AddServiceDependences()
                .AddCoreDependences();
            #endregion

            //Allow CORS
            var CORS = "_cors";

            builder.Services.AddCors(Options =>
            {
                Options.AddPolicy(name: CORS,
                    policy =>
                    {
                        policy.AllowAnyHeader();
                        policy.AllowAnyMethod();
                        policy.AllowAnyOrigin();
                    });
            });
            var app = builder.Build();
            var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseHttpsRedirection();

            app.UseCors(CORS);
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}