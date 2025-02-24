
using CompanyApp.Infrastructure.Data;
using CompanyApp.Infrastructure.Repositories.Repository;
using CompanyApp.Infrastructure.Repositories.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using CompanyApp.Application.Services.Interfaces;
using CompanyApp.Application.Services.Service;
using CompanyApp.Infrastructure.Pagination;

namespace CompanyApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web API", Version = "v1" });
            });
            builder.Services.AddLogging();
            builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
           
            //var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.RequireHttpsMetadata = false;
            //        options.SaveToken = true;
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuerSigningKey = true,
            //            IssuerSigningKey = new SymmetricSecurityKey(key),
            //            ValidateIssuer = false,
            //            ValidateAudience = false
            //        };
            //    });

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ICompanyService, CompanyService>();
            builder.Services.AddScoped<IContactService, ContactService>();
            builder.Services.AddScoped<ICountryService, CountryService>();
            //builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
            builder.Services.AddScoped<IContactRepository, ContactRepository>();
            builder.Services.AddScoped<ICountryRepository, CountryRepository>();
            builder.Services.Configure<PaginationSettings>(builder.Configuration.GetSection("PaginationSettings"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API v1"));
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRouting();

            app.MapControllers();

            app.Run();
        }
    }
}
