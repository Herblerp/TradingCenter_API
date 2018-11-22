using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradingcenter.Data;
using Tradingcenter.Data.Repositories;
using Trainingcenter.Domain.Repositories;
using Trainingcenter.Domain.Services;
using Trainingcenter.Domain.Services.CommentServices;
using Trainingcenter.Domain.Services.ExchangeKeyServices;
using Trainingcenter.Domain.Services.NoteServices;
using Trainingcenter.Domain.Services.OrderServices;
using Trainingcenter.Domain.Services.PortfolioServices;
using Trainingcenter.Domain.Services.UserServices;

/*  This class contains the service configurations,
    to keep the startup class more readable.    */

namespace Tradingcenter.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureMvc(this IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public static void ConfigureConnections(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options => options.UseMySql(configuration.GetConnectionString("DefaultConnection")));
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IGenericRepository, GenericRepository>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPortfolioServices, PortfolioServices>();
            services.AddScoped<IPortfolioRepository, PortfolioRepository>();
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IExchangeKeyRepository, ExchangeKeyRepository>();
            services.AddScoped<IExchangeKeyServices, ExchangeKeyServices>();
            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<INoteServices, NoteServices>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentServices, CommentServices>();
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("Appsettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
