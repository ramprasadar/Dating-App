using DatingAppAPI.Data;
using DatingAppAPI.Interfaces;
using DatingAppAPI.ServiceExtensions;


using DatingAppAPI.Extensions;
using DatingAppAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = builder.Configuration;
builder.Services.AddApplicationServices(config);

builder.Services.AddControllers();

//Adding Authentication
builder.Services.AddIdentityServices(config);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMIddleware>();

if (builder.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
