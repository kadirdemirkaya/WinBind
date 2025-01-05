using WinBind.Application;
using WinBind.Infrastructure;
using WinBind.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddRouting(opt =>
{
    opt.LowercaseUrls = true;
});

builder.Services.AddCors();

builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

builder.Services.ApplicationServiceRegistration();

builder.Services.InfrastructureServiceRegistration();

builder.Services.PersistenceServiceRegistration(builder.Configuration);


var app = builder.Build();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
});

app.UseSwagger();

app.UseSwaggerUI();

app.InfrastructureApplicationBuilder();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
