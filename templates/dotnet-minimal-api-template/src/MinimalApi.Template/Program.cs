using MinimalApi.Template.Endpoints;
#if (enableHealthCheck)
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
#endif
#if (usePrometheus)
using Prometheus;
#endif

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

#if (enableOpenApi)
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
#endif

#if (enableHealthCheck && !usePrometheus)
builder.Services.AddHealthChecks();
#elif (enableHealthCheck && usePrometheus)
services.AddHealthChecks().ForwardToPrometheus();
#endif

services.AddSingleton<ICustomersRepository, CustomersRepository>();

var app = builder.Build();

#if (enableOpenApi)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#endif

app.MapGroup("/customers")
    .MapCustomerEndpoints()
    .WithTags("Customers");

#if (enableHealthCheck)
app.MapHealthChecks("/health/ready");
app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = _ => false
});
#endif

#if (usePrometheus)
app.MapMetrics();
app.UseHttpMetrics(options =>
{
    options.ReduceStatusCodeCardinality();
    options.AddCustomLabel("host", context => $"{context.Request.Host.Host}:{context.Request.Host.Port}");
});
#endif

await app.RunAsync();

public partial class Program { }