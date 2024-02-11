using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using porosartapi.model;
using porosartapi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using porosartapi.model.BusinessModel;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PorosArt")).UseSnakeCaseNamingConvention());
builder.Services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation  
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "JWT Token Authentication API",
                    Description = "ASP.NET 6 Web API"
                });
                // To Enable authorization using Swagger (JWT)  
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            var appSettingsSection = builder.Configuration.GetSection("AppSettings");

            BaseService._appSetting = appSettingsSection.Get<AppSettings>();

            // builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //             .AddMicrosoftIdentityWebApi(options =>
            //     {
            //         builder.Configuration.Bind("AzureAdB2C", options);

            //         options.TokenValidationParameters.NameClaimType = "name";
            //     },
            //     options => { builder.Configuration.Bind("AzureAdB2C", options); });

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(BaseService._appSetting.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            // .AddMicrosoftIdentityWebApi(options =>
            // {
            //     builder.Configuration.Bind("AzureAdB2C", options);

            //     options.TokenValidationParameters.NameClaimType = "name";
            // },
            // options => { builder.Configuration.Bind("AzureAdB2C", options); });
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
// {
//     opt.RequireHttpsMetadata = false;//bu de?i?icek abi ssl gelince true set et!!!!

//     opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//     {
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(BaseService._appSetting.Secret)),
//         ValidateIssuerSigningKey = true,
//         ValidateLifetime = true,
//     };
// });

builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<GameService>();
builder.Services.AddTransient<TeamService>();
builder.Services.AddTransient<EventService>();
builder.Services.AddTransient<WorkshopService>();
builder.Services.AddCors(options =>
            {
                options.AddPolicy("ClientPermission", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:3000", "https://imager.deliversai.net")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
var app = builder.Build();
app.UseCors("ClientPermission");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseAuthentication();
    app.UseAuthorization();

}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

// app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
