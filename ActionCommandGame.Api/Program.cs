using ActionCommandGame.Configuration;
using ActionCommandGame.Repository;
using ActionCommandGame.Services;
using ActionCommandGame.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure AppSettings
var appSettings = new AppSettings();
builder.Configuration?.Bind(nameof(AppSettings), appSettings);
builder.Services.AddSingleton(appSettings);


// Configure DbContext
builder.Services.AddDbContext<ActionCommandGameDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var securityDefinition = new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    };
    options.AddSecurityDefinition("Bearer", securityDefinition);
    var securityRequirementScheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };
    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            securityRequirementScheme, new string[] { }
        }
    };
    options.AddSecurityRequirement(securityRequirement);
});

builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<INegativeGameEventService, NegativeGameEventService>();
builder.Services.AddScoped<IPositiveGameEventService, PositiveGameEventService>();
builder.Services.AddScoped<IPlayerItemService, PlayerItemService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
//builder.Services.AddScoped<IdentityService>();

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


