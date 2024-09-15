using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TournamentOrganization.BusinessLogic.Dtos;
using TournamentOrganization.BusinessLogic.Interfaces;
using TournamentOrganization.BusinessLogic.Services;
using TournamentOrganization.DataAccess;
using TournamentOrganization.DataAccess.Repositories.Implementations;
using TournamentOrganization.DataAccess.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddTransient<IValidator<TournamentDto>, TournamentDtoValidator>();

//Repositories
builder.Services.AddScoped<ITournamentRepository, TournamentRepository>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IMatchRepository, MatchRepository>();

//Services
builder.Services.AddScoped<ITournamentService, TournamentService>();
builder.Services.AddScoped<ITournamentSimulationService, TournamentSimulationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
