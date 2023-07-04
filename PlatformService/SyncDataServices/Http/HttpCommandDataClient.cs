using Microsoft.Extensions.Configuration;
using PlatformService.Dtos;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task SendPlatformToCommand(PlatformReadDto platform)
        {
            var httpContent = new StringContent
                (
                    JsonSerializer.Serialize(platform),
                    Encoding.UTF8,
                    "application/json"
                );

            var response = await _httpClient.PostAsync($"{_configuration["CommandService"]}", httpContent);

            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("Excellent Meeran!!");
                Console.WriteLine("Fly from PlatFormSr to CommandSr");
            }
            else
            {
                Console.WriteLine("Failed Connection Establishmed with CommandSr!!");
            }
        }
    }
}
