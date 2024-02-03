using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TerraformRegistry;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IServiceConfiguration>(_ => new ServiceConfiguration());

var lambda_function = Environment.GetEnvironmentVariable("AWS_LAMBDA_FUNCTION_NAME");

if (!string.IsNullOrEmpty(lambda_function))
{
    builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);
}

var app = builder.Build();

if (string.IsNullOrEmpty(lambda_function))
{
    app.UseHttpsRedirection();
}

app.UseRouting();
app.MapControllers();

app.Run();