using ApiKeySample.Extensions;

var builder = WebApplication.CreateBuilder(args);

IServiceCollection services = builder.Services;

services.AddControllers();
services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(SwaggerConfigurator.Configure)
    .AddApiKey();

var app = builder.Build();

app
    .UseSwagger()
    .UseSwaggerUI()
    .UseAuthentication()
    .UseAuthorization();

app.MapControllers();

app.Run();