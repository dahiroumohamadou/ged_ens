using backend.Data;
using backend.Repository.Implementations;
using backend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using backend.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using backend.Middleware;

 var builder = WebApplication.CreateBuilder(args);

// SECURITY WEB API WITH JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
        };
    });


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Serializing object to load data after mapping (Eager)
builder.Services.AddMvc(option => option.EnableEndpointRouting = false)
    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
    .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
//MariaDb ou Mysql configuration
var connectionString = builder.Configuration.GetConnectionString("MysqlConn");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
// All Interfaces for api

builder.Services.AddScoped<ICycle, CycleRepo>();
builder.Services.AddScoped<IFiliere, FiliereRepo>();
builder.Services.AddScoped<IDocument, DocumentRepo>();
builder.Services.AddScoped<IUser, UserRepo>();


//for all orgin web api 
//builder.Services.AddCors();
//for single cors 1st etape
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", a =>
    {
        a.WithOrigins("http://localhost:5290");
    });
});

var app = builder.Build();
// securtiy with api key (calling class implement api key)
//app.UseMiddleware<ApiKeyMiddleware>();
// end security api key

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//for single cors 2nd etape
app.UseCors(options =>
{
    options.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});
//calling authentifcation for a web sececurity jwt
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//dernier config for calling web api
app.UseCors("MyPolicy");

app.Run();
