using AutoBogus;

using Backend.Entities;
using Backend.Models;

using Microsoft.AspNetCore.Mvc;

using Quartz;

namespace Backend.Crons
{
    public class SendEventLogJob([FromServices] IHttpClientFactory httpClientFactory) : IJob
    {
        private readonly HttpClient httpClient = httpClientFactory.CreateClient("InternalApi");
        public Task Execute(IJobExecutionContext context)
        {
            string url = "event-log";
            List<EventLogDto> eventLogs = new AutoFaker<EventLogDto>()
                .Generate(5);

            return httpClient.PostAsJsonAsync(url, eventLogs);
        }
    }
}
