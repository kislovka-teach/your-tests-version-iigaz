using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Variant1.Dtos;
using Variant2;
using Variant2.Extensions.RouteGroupBuilderExtensions;
using Variant2.Services;
using Variant2.Services.Repositories;
using Variant2.Services.Repositories.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("Variant2Database"))
        .UseSnakeCaseNamingConvention());

builder.Services.AddScoped<IRevisionsComparer, RevisionsComparer>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IRevisionRepository, RevisionRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

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

app.MapGroup("").MapArticles();

app.Run();