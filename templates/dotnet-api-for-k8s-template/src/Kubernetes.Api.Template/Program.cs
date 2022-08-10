#if(enableHealthCheck)
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
#endif
#if (enableApiVersioning)
using Microsoft.AspNetCore.Mvc;
#endif
#if (usePrometheus)
using Prometheus;
#endif

namespace Kubernetes.Api.Template
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
#if (enableApiVersioning)

            builder.Services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = ApiVersion.Default;
            });
#endif
#if (enableHealthCheck && !usePrometheus)

            builder.Services.AddHealthChecks();
#elif (enableHealthCheck && usePrometheus)

            builder.Services.AddHealthChecks().ForwardToPrometheus();
#endif
#if (enableOpenApi)

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
#endif

            var app = builder.Build();
            app.MapControllers();
#if (enableOpenApi)

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
#endif
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
        }
    }
}