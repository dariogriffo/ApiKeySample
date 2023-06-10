using ApiKeySample.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IServiceCollection services = builder.Services;

services.AddControllers();
services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(SwaggerConfigurator.Configure)
    .AddApiKey();

WebApplication app = builder.Build();

app
    .UseSwagger()
    .UseSwaggerUI()
    .UseAuthentication()
    .UseAuthenticationShortCircuit()
    .UseAuthorization();

app.MapControllers();

app.Run();