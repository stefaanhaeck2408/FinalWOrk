using DL.Context;
using DL.Models;
using DL.Repositories;
using DL.UnitOfWork;
using Facade.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Text;



namespace Services
{
    public static class OnStartUpExtention
    {
        public static void StartUpExtension(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IIngevoerdAntwoordService, IngevoerdAntwoordService>();
            services.AddScoped<IQuizService, QuizService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IVragenService, VraagService>();
            services.AddScoped<IRondeService, RondeService>();
            services.AddScoped<IStripeAccountService, StripeAccountService>();
            services.AddScoped<IQuizRondeUnitOfWork, QuizRondeUnitOfWork>();
            services.AddScoped<ITeamQuizRondeUnitOfWork, TeamQuizRondeUnitOfWork>();
            services.AddScoped<IRondeVraagUnitOfWork, RondeVraagUnitOfWork>();
            services.AddScoped<ISQLRepository<StripeAccount>, SQLRepository<StripeAccount>>();

            services.AddScoped(typeof(ISQLRepository<>), typeof(SQLRepository<>));


        }

    }
}
