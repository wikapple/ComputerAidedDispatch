using ComputerAidedDispatchAPI;
using ComputerAidedDispatchAPI.Data;
using ComputerAidedDispatchAPI.Models;
using ComputerAidedDispatchAPI.Repository;
using ComputerAidedDispatchAPI.Repository.IRepository;
using ComputerAidedDispatchAPI.Service;
using ComputerAidedDispatchAPI.Service.IService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// DB currently InMemory:
builder.Services.AddDbContext<ComputerAidedDispatchContext>(opt =>
opt.UseInMemoryDatabase("CadDb"));

// Add Identity:
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ComputerAidedDispatchContext>();

// Add Dependency Injection:
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitRepository, UnitRepository>();
builder.Services.AddScoped<ICallForServiceRepository, CallForServiceRepository>();
builder.Services.AddScoped<IDispatcherRepository, DispatcherRepository>();
builder.Services.AddScoped<ICallCommentRepository, CallCommentRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<IDispatcherService, DispatcherService>();
builder.Services.AddScoped<ICallForServiceService, CallForServiceService>();
builder.Services.AddScoped<ICallCommentService, CallCommentService>();
builder.Services.AddScoped<ICadSharedService, CadSharedService>();

builder.Services.AddAutoMapper(typeof(MappingConfig));

// Get Secret
var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false

    };
});




builder.Services.AddControllers().AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
 builder.Services.AddSwaggerGen(options =>
 {
     options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
     {

         Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
            "Enter 'Bearer' [space] and then your token in the text input below. \r\n\r\n" +
            "Example: \"Bearer 12345abcdef\"",
         Name = "Authorization",
         In = ParameterLocation.Header,
         Scheme = "Bearer"
     });

     options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }

    });
 });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
     app.UseSwagger();
     app.UseSwaggerUI();
}

/* Must use more advanced docker skills in order to implement Https redirection:
 *  Note: I spent two days trying to fix my Docker and Docker-compose network 
 *  before realizing this middleware problem! X-/
 *  app.UseHttpsRedirection();
*/

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
