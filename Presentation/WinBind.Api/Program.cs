using WinBind.Application;
using WinBind.Infrastructure;
using WinBind.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

builder.Services.ApplicationServiceRegistration();

builder.Services.InfrastructureServiceRegistration();

builder.Services.PersistenceServiceRegistration(builder.Configuration);


var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
