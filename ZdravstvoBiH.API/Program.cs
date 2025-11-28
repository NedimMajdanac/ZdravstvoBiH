using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Zdravstvo.Core.Entities;
using Zdravstvo.Core.Interfaces;
using Zdravstvo.Infrastructure.Data;
using Zdravstvo.Infrastructure.Service;
using ZdravstvoBiH.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });


builder.Services.AddScoped<IPacijentService, PacijentService>();
builder.Services.AddScoped<IDoktorService, DoktorService>();
builder.Services.AddScoped<IUstanovaService, UstanovaService>();
builder.Services.AddScoped<ITerminService, TerminService>();
builder.Services.AddScoped<IMedicinskiKartonService, MedicinskiKartonService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUputnicaService, UputnicaService>();
builder.Services.AddScoped<IPregledService,PregledService>();
builder.Services.AddScoped<IDijagnozaService, DijagnozaService>();
builder.Services.AddScoped<ValidationService>();

builder.Services.AddDbContext<ZdravstvoContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Default"),
        b => b.MigrationsAssembly("Zdravstvo.Infrastructure")
        )
    );

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
