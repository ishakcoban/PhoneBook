using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PhoneBook.Core.request;
using PhoneBook.DataAccess.DBContext;
using PhoneBook.DataAccess.Repositories.Concrete;
using PhoneBook.Services.Abstract;
using PhoneBook.Services.Concrete;
using PhoneBook.Services.Helpers;
using PhoneBook.Services.Mappers;
using System.Text;

namespace PhoneBook.Web
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
            });

            //db
            services.AddDbContext<ApplicationDbContext>(options => { options.UseSqlServer(Configuration.GetConnectionString("Default")); });

            //repos
            services.AddScoped<LoginRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<PhoneNumberRepository>();
            services.AddScoped<NoteRepository>();

            //validators
            services.AddTransient<LoginValidator>();
            services.AddTransient<AddUserValidator>();
            services.AddTransient<AddNoteValidator>();
            services.AddScoped<IValidator<AddUserRequest>, AddUserValidator>();
            services.AddScoped<IValidator<LoginRequest>, LoginValidator>();
            services.AddScoped<IValidator<AddNoteRequest>, AddNoteValidator>();

            //mappers
            services.AddAutoMapper(typeof(UserMapper));
            services.AddAutoMapper(typeof(NoteMapper));
            services.AddAutoMapper(typeof(PhoneNumberMapper));

            //helpers
            services.AddScoped<UserIDGetter>();

            //services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILoginService, LoginService>();


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Configuration["Jwt:Issuer"],
            ValidAudience = Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
        };
    });
            services.AddHttpContextAccessor();
            services.AddAuthorization();
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // Configure JSON serialization settings here if needed
                    options.JsonSerializerOptions.WriteIndented = true; // This is equivalent to Formatting.Indented in Newtonsoft.Json
                });

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseStaticFiles();

            app.UseCors("Cors");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.Run();
        }

    }

}








