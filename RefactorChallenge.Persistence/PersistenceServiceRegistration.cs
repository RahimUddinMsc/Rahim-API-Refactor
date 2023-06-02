using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RefactorChallenge.Application.Contracts;
using RefactorChallenge.Persistence.Repositories;
using RefactoringChallenge.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace RefactorChallenge.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {            
            services.AddDbContext<NorthwindDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(GenericRepository<>));
            
            return services;
        }


    }
}
