using Backend.Context;
using Backend.Crons;
using Backend.DependecyInjection;

using Microsoft.EntityFrameworkCore;

using Quartz;

namespace Backend
{
    public static class EventLogRegistrar
    {
        public static IServiceCollection RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            AddApiDocumentation(services);
            AddServices(services);
            AddDbContexts(services, configuration);
            AddHttpClient(services, configuration);
            AddCronJob(services, configuration);
            return services;
        }

        private static void AddCronJob(IServiceCollection services, IConfiguration configuration)
        {
            services.AddQuartz(q =>
                {
                    var jobKey = nameof(SendEventLogJob);
                    q.AddJob<SendEventLogJob>(opts => opts.WithIdentity(jobKey));
                    string cron = configuration["QuartzJobs:SendEventLog"]!;

                    q.AddTrigger(opts => opts
                        .ForJob(jobKey)
                        .WithIdentity($"{jobKey}-trigger")
                        .WithCronSchedule(cron));
                }
            );

            services.AddQuartzHostedService(q => { q.WaitForJobsToComplete = true; });
        }

        private static void AddHttpClient(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("InternalApi", client =>
            {
                client.BaseAddress = new Uri(configuration["BaseUrl"]!);
                client.Timeout = TimeSpan.FromSeconds(30);
            });
        }

        private static void AddApiDocumentation(IServiceCollection services)
        {
            services.AddOpenApi();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        private static void AddDbContexts(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EventContext>(options => options.UseInMemoryDatabase("EventLog"));
        }

        private static void AddServices(IServiceCollection services)
        {
            services.RegisterAssemblyByConvention(typeof(Program).Assembly);
        }
    }
}
