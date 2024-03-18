using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Variant1;
using Variant1.Dtos;
using Variant1.Extensions.RouteGroupBuilderExtensions;
using Variant1.Models;
using Variant1.Models.Validators;
using Variant1.Profiles;
using Variant1.Services.Repositories;
using Variant1.Services.Repositories.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("Variant1Database"))
        .UseSnakeCaseNamingConvention());
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/login");
builder.Services.AddAutoMapper(expression => expression.AddProfile(new AutoMapperProfile()));
builder.Services.AddAuthorization();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPlantsRepository, PlantsRepository>();
builder.Services.AddScoped<IDisplayRepository, DisplayRepository>();
builder.Services.AddScoped<IVisitorRepository, VisitorRepository>();
builder.Services.AddScoped<IValidator<Display>, DisplayValidator>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/login",
        async (HttpContext context, [FromServices] IUserRepository userRepository, [FromBody] LoginDto loginDto) =>
        {
            var user = await userRepository.LogInAsync(loginDto.Login, loginDto.Password);
            if (user == null)
                return Results.NotFound();
            var claims = new List<Claim>
                { new(ClaimsIdentity.DefaultNameClaimType, user.Login) };
            claims.AddRange(user.Roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Role)));
            var principal =
                new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
            await context.SignInAsync(principal);
            return Results.NoContent();
        })
    .WithName("Login")
    .WithOpenApi();

app.MapGroup("/plants")
    .MapPlants()
    .RequireAuthorization(policy => policy.RequireAuthenticatedUser());

app.MapGroup("/displays").MapDisplays();

app.Run();