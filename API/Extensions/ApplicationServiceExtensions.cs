using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtension
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config ){
            
             services.AddScoped<ITokenService, TokenService>();

            // Setting up a connection string for our database
            services.AddDbContext<DataContext>(optionsAction=>
            {
                optionsAction.UseSqlite(config.GetConnectionString("DefaultConnection"));
            }
            
            );

            return services;

        }

    }
}