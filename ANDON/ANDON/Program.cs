using System.Text;
using ANDON.Middleware;
using ANDON_Application.Interface.Auth;
using ANDON_Application.Interface.Defect;
using ANDON_Application.Interface.Routes;
using ANDON_Application.Mapper;
using ANDON_Application.Mapper.Auth;
using ANDON_Application.Mapper.Routes;
using ANDON_Application.Service.Auth;
using ANDON_Application.Service.Defect;
using ANDON_Application.Service.Routes;
using ANDON_Domain.Interface;
using ANDON_Infrastructure.Repository;
using ANDON_Infrastructure.Repository.Auth;
using ANDON_Infrastructure.Repository.Routes;
using ANDON_Infrastructure.Token;
using ANDON_Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/// mặc đinh dữ liệu trẩ về là theo dạng CamelCase
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Địa chỉ của frontend (React)
              .AllowAnyMethod()  // Cho phép tất cả các phương thức HTTP như GET, POST, PUT, DELETE
              .AllowAnyHeader()  // Cho phép tất cả các header
              .AllowCredentials();  // Cho phép gửi cookies và header xác thực
    });
});
// Cấu hình jwts settings
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);
// 2.Cấu hình Authentication với JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(secretKey)
    };
});
/// Đăng kí UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// đăng kis login
builder.Services.AddScoped<IJwtRepository, JwtRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, AuthRepository>();
builder.Services.AddScoped<IDefectRepository, DefectRepository>();
builder.Services.AddScoped<IRouteRepository,RouteRepository>();
builder.Services.AddScoped<IDefectService,DefectService>();
builder.Services.AddScoped<IRouteService,RouteService>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AuthProfile>();
    cfg.AddProfile<DefectProfile>();
    cfg.AddProfile<RouteProfile>();
});

var app = builder.Build();

// add dependenci

app.UseCors("AllowFrontend");
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ANDON API v1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<JwtMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
