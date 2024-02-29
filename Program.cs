using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using porosartapi.model;
using porosartapi.model.BusinessModel;
using porosartapi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<AppDbContext>(options =>
    options
        .UseNpgsql(builder.Configuration.GetConnectionString("PorosArt"))
        .UseSnakeCaseNamingConvention()
);
builder.Services.AddSwaggerGen(swagger =>
{
    //This is to generate the Default UI of Swagger Documentation
    swagger.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Version = "v1",
            Title = "JWT Token Authentication API",
            Description = "ASP.NET 6 Web API"
        }
    );
    // To Enable authorization using Swagger (JWT)
    swagger.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description =
                "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
        }
    );
    swagger.AddSecurityRequirement(
        new OpenApiSecurityRequirement
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
                new string[] { }
            }
        }
    );
});
var appSettingsSection = builder.Configuration.GetSection("AppSettings");

BaseService._appSetting = appSettingsSection.Get<AppSettings>();


// builder
//     .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddMicrosoftIdentityWebApi(
//         options =>
//         {
//             builder.Configuration.Bind("AzureAdB2C", options);

//             options.TokenValidationParameters.NameClaimType = "name";
//         },
//         options =>
//         {
//             builder.Configuration.Bind("AzureAdB2C", options);
//         }
//     );

 builder
    .Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = true;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(BaseService._appSetting.Secret)
            ),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<GameService>();
builder.Services.AddTransient<TeamService>();
builder.Services.AddTransient<EventService>();
builder.Services.AddTransient<WorkshopService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "ClientPermission",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:3000", "http://porosart.com")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        }
    );
});
var app = builder.Build();
app.UseCors("ClientPermission");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseAuthentication();
    app.UseAuthorization();
}

// "AzureAdB2C": {
//   "Instance": "https://porosartapib2c.b2clogin.com",
//   "Domain": "porosartapib2c.onmicrosoft.com",
//   "ClientId": "2ad1bc44-adcf-479d-928c-4dd115583b5a"
// },
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
