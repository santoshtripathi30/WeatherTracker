using System;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace WeatherTracker.Functions
{
    public class Function
    {
        private static readonly HttpClient _httpClient = new HttpClient();


        [FunctionName("Function1")]
        public async Task Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            string city = "Pune"; // Replace with dynamic city selection
            string apiKey = "YOUR_OPENWEATHER_API_KEY";
            string weatherApiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

            HttpResponseMessage response = await _httpClient.GetAsync(weatherApiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dynamic weatherData = JsonConvert.DeserializeObject(content);

                double temprature = weatherData.main.temp;

                if (temprature < 29)
                {

                    log.LogInformation($"Alert! It's temprature is {temprature} in {city}.");

                }



            }


        }
    }
}
