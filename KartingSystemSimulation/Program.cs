using KartingSystemSimulation.Repositories;
using KartingSystemSimulation.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;// For SymmetricSecurityKey and TokenValidationParameters
using System.Text; // For Encoding

namespace KartingSystemSimulation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add Authorization
            builder.Services.AddAuthorization();
            // Add services to the container
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                        ValidAudience = builder.Configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
                    };
                });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            });

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
            builder.Services.AddScoped<IGameRepository, GameRepository>();
            builder.Services.AddScoped<IRaceBookingRepository, RaceBookingRepository>();
            builder.Services.AddScoped<ILiveRaceRepository, LiveRaceRepository>();
            builder.Services.AddScoped<IMembershipRepository,  MembershipRepository>();
            builder.Services.AddScoped<ILeaderboardRepository, LeaderboardRepository>();

            // Register services
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<IRegisterService, RegisterService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ISupervisorService, SupervisorService>();
            builder.Services.AddScoped<IKartService, KartService>();
            builder.Services.AddScoped<IRacerService, RacerService>();
            builder.Services.AddScoped<IGameService, GameService>();
            builder.Services.AddScoped<IRaceBookingService, RaceBookingService>();
            builder.Services.AddScoped<ILiveRaceService, LiveRaceService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<ILeaderboardService, LeaderboardService>();
            builder.Services.AddScoped<IRaceHistoryService, RaceHistoryService>();
            // AutoMapper configuration
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddScoped<IMembershipService, MembershipService>();
           // AutoMapper configuration
           builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

            // AutoMapper configuration
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddHttpClient();

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
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer <token>')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
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
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.UseDeveloperExceptionPage();

            app.Run();
        }
    }
}
