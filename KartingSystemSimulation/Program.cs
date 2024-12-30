using KartingSystemSimulation.Repositories;
using KartingSystemSimulation.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerGen; // Required for SwaggerGen


namespace KartingSystemSimulation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Register repositories
            builder.Services.AddScoped<IAdminRepository, AdminRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IGameRepository, GameRepository>();
            builder.Services.AddScoped<IKartRepository, KartRepository>();
            builder.Services.AddScoped<ILeaderboardRepository, LeaderboardRepository>();
            builder.Services.AddScoped<ILiveRaceRepository, LiveRaceRepository>();
            builder.Services.AddScoped<IRaceBookingRepository, RaceBookingRepository>();
            builder.Services.AddScoped<IRaceHistoryLeaderboardRepository, RaceHistoryLeaderboardRepository>();
            builder.Services.AddScoped<IRaceHistoryRepository, RaceHistoryRepository>();
            builder.Services.AddScoped<IRaceRacerRepository, RaceRacerRepository>();
            builder.Services.AddScoped<IRacerRepository, RacerRepository>();
            builder.Services.AddScoped<ISupervisorRacerRepository, SupervisorRacerRepository>();
            builder.Services.AddScoped<ISupervisorRepository, SupervisorRepository>();

            // Register services
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<IRegisterService, RegisterService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ISupervisorService, SupervisorService>();
            builder.Services.AddScoped<IKartService, KartService>();
            // AutoMapper configuration
            builder.Services.AddAutoMapper(typeof(Program));

            // Add controllers
            builder.Services.AddControllers();

            // Add Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Karting System Simulation API",
                    Version = "v1",
                    Description = "API Documentation for the Karting System Simulation Project",
                    Contact = new OpenApiContact
                    {
                        Name = "Support Team",
                        Email = "support@kartingsimulation.com"
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Karting System Simulation API v1");

                });
            }
         
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
