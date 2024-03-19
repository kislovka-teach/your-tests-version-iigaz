using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Variant2;
using Variant2.Dtos;
using Variant2.Extensions.RouteGroupBuilderExtensions;
using Variant2.Profiles;
using Variant2.Services;
using Variant2.Services.Repositories;
using Variant2.Services.Repositories.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(expression => expression.AddProfile(new AutoMapperProfile()));
builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("Variant2Database"))
        .UseSnakeCaseNamingConvention());
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters

    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Audience"],
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JwtKey"]!)),
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization();

builder.Services.AddScoped<IRevisionsComparer, RevisionsComparer>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IRevisionRepository, RevisionRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

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
        async (IConfiguration configuration, [FromServices] IUserRepository userRepository,
            [FromBody] LoginDto loginDto) =>
        {
            var user = await userRepository.LogInAsync(loginDto.Login, loginDto.Password);
            if (user == null)
                return Results.NotFound();
            var claims = new List<Claim>
                { new(ClaimsIdentity.DefaultNameClaimType, user.Login), new("userId", user.Id.ToString()) };
            claims.AddRange(user.Roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Role)));
            var jwt = new JwtSecurityToken(configuration["Issuer"], configuration["Audience"], claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtKey"]!)),
                    SecurityAlgorithms.HmacSha256));
            return Results.Ok(new { AccessToken = new JwtSecurityTokenHandler().WriteToken(jwt) });
        })
    .WithName("Login")
    .WithOpenApi();

app.MapGroup("").MapArticles();

app.Run();