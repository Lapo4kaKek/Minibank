using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using Minibank.Core;
using Minibank.Data.HttpCLients;
using Minibank.Data.HttpCLients.Models;

namespace Minibank.Data
{
    public class Database : IDatabase
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationSettings _settings;

        public Database(HttpClient httpClient, IOptions<ApplicationSettings> options)
        {
            _httpClient = httpClient;
            _settings = options.Value;
        }

        public int GetRubleCourse(string currencyCode)
        {
            var response = _httpClient.GetFromJsonAsync<CourseResponse>("daily_json.js")
                .GetAwaiter().GetResult();

            return (int)response.Valute[currencyCode].Value;
        }
    }

}