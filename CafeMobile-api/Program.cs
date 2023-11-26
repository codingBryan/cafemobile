global using Microsoft.EntityFrameworkCore;
using Amazon.S3;
using CafeMobile_api.Context;
using CafeMobile_api.Models.Entities;
using CafeMobile_api.Repository.CafeteriaRepo;
using CafeMobile_api.Repository.ParentRepo;
using CafeMobile_api.Repository.StudentRepo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);


var connection_string = builder.Configuration.GetConnectionString("test_db");
builder.Services.AddDbContext<DataContext>(opts =>
{
    opts.UseMySql(connection_string, ServerVersion.AutoDetect(connection_string));
});

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);



var config = builder.Configuration;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opts =>
{
    opts.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:secretKey"]!)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
    };
});

builder.Services.AddSwaggerGen(opts =>
{
    opts.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization headers using the bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,

    });
    opts.OperationFilter<SecurityRequirementsOperationFilter>();
});


builder.Services.AddSingleton<IAmazonS3, AmazonS3Client>();
builder.Services.AddScoped<StudentRepository, StudentService>();
builder.Services.AddScoped<CafeteriaRepository,CafeteriaService>();
builder.Services.AddScoped<ParentRepository, ParentService>();
builder.Services.AddScoped<Authentication>();
builder.Services.AddHttpContextAccessor();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
