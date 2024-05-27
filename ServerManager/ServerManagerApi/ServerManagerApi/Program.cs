using Microsoft.EntityFrameworkCore;
using ServerManagerCore.Interfaces;
using ServerManagerCore.Services;
using ServerManagerDAL.Data;
using ServerManagerDAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(corsPolicyBuilder =>
    {
        corsPolicyBuilder.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<RequestService>();
builder.Services.AddScoped<IRequestRepository, RequestRepository>();

builder.Services.AddScoped<SessionService>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();

builder.Services.AddScoped<ServerService>();
builder.Services.AddScoped<IServerRepository, ServerRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

// Just for testing.
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
