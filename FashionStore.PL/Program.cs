
using System.Runtime.CompilerServices;
using System.Text;
using FashionStore.BLL;
using FashionStore.BLL.Services.AuthServices;
using FashionStore.BLL.Services.TokenServices;
using FashionStore.BLL.Validators;
using FashionStore.DAL;
using FashionStore.DAL.Context;
using FashionStore.DAL.Entities;
using FashionStore.DAL.Interfaces;
using FashionStore.DAL.Repositories;
using FashionStore.PL.Extentions;
using FashionStore.PL.Middlewares;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

namespace FashionStore.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Validations

            builder.Services.AddValidatorsFromAssemblyContaining<RegisterDtoValidations>();

            #endregion

            #region SendingEmail

            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
            builder.Services.AddTransient<EmailService>();
            #endregion

            builder.Services.AddControllers().ConfigureApiBehaviorOptions(option =>
            {
                option.SuppressModelStateInvalidFilter = true;
            });


            builder.Services.AddSingleton<IConnectionMultiplexer>(service =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });

            builder.Services.AddScoped<TokenServices>();
            builder.Services.AddBusinessService();
            builder.Services.AddDataAccessService(builder.Configuration);

            builder.Services.AddExceptionHandler<ExceptionHandlingMiddleware>();
            builder.Services.AddProblemDetails();

            builder.Services.AddSwaggerService();

            #region Identity

            builder.Services.AddIdentityCore<AppUser>(options =>
            {
                // Validation to be read from configurations
                options.Password.RequiredUniqueChars = 2;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;

                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<Auth_Context>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            #endregion

            #region Authentication
            //https://localhost:7041/api/Account/external-login?provider=Google
            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; // For JWT validation
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme; // For Google logins
            //    options.DefaultSignInScheme = "ExternalCookie";
            //})
            //.AddJwtBearer(options =>
            //{
            //    var secretKey = builder.Configuration.GetValue<string>("JwtSecretKey")!;

            //    var secretKeyInBytes = Encoding.UTF8.GetBytes(secretKey);
            //    var key = new SymmetricSecurityKey(secretKeyInBytes);

            //    options.TokenValidationParameters = new()
            //    {
            //        ValidateIssuer = false,
            //        ValidateAudience = false,
            //        IssuerSigningKey = key,
            //    };
            //})
            //.AddCookie("ExternalCookie", options => // For temporary external auth
            //{
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(5); // Short-lived
            //    options.SlidingExpiration = true;
            //})
            //.AddGoogle(options =>
            //{
            //    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
            //    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
            //    options.SaveTokens = true;
            //});

            ////////////////////////////////////////////////////////////////////

            #region working configs with cookie
            // this is the working code with cookie 

            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = IdentityConstants.ApplicationScheme; // For Identity cookies
            //    options.DefaultSignInScheme = IdentityConstants.ExternalScheme; // For external auth
            //    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme; // For Google challenges
            //})
            //  .AddCookie(IdentityConstants.ApplicationScheme, options =>
            //  {
            //      options.LoginPath = "/Account/Login";
            //      options.AccessDeniedPath = "/Account/AccessDenied";
            //  })
            //  .AddCookie(IdentityConstants.ExternalScheme) // Required for external auth
            //  .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
            //  {
            //      options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
            //      options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
            //      options.CallbackPath = "/signin-google";
            //      options.SaveTokens = true;
            //      options.Events = new OAuthEvents
            //      {
            //          OnRemoteFailure = context =>
            //          {
            //              context.Response.Redirect("/error?message=" + context.Failure.Message);
            //              context.HandleResponse();
            //              return Task.CompletedTask;
            //          }
            //      };
            //  })
            //  .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            //  {
            //      var secretKey = builder.Configuration.GetValue<string>("JwtSecretKey")!;
            //      var secretKeyInBytes = Encoding.UTF8.GetBytes(secretKey);
            //      var key = new SymmetricSecurityKey(secretKeyInBytes);

            //      options.TokenValidationParameters = new()
            //      {
            //          ValidateIssuer = false,
            //          ValidateAudience = false,
            //          IssuerSigningKey = key,
            //      };
            //  }); 
            #endregion

            #region Working Configurations With JWT token instead of cookie
            //Working Configurations With JWT token instead of cookie

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = "Identity.External"; // For external auth flow
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie("Identity.Application", options =>
            {
                options.Cookie.Name = "app.identity";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.SlidingExpiration = true;
            })
            .AddCookie("Identity.External", options =>
            {
                options.Cookie.Name = "ext.identity";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            })
            .AddCookie("TemporaryCookie", options =>
            {
                options.Cookie.Name = "temp.oauth";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            })
            .AddGoogle(options =>
            {
                options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
                options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
                options.SignInScheme = "Identity.External";
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["JwtSecretKey"]!))
                };
            }); 
            #endregion

            #endregion


            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
                RequestPath = "/images"
            });
            app.UseExceptionHandler();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
