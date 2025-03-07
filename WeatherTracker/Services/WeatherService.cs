using System.Text;

using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Options;

namespace WeatherTracker.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly WeatherServiceSettings _weatherServiceSettings;

        public WeatherService(HttpClient httpClient, IOptions<WeatherServiceSettings> weatherServiceSettings)
        {
            _httpClient = httpClient;
            _weatherServiceSettings = weatherServiceSettings.Value;
        }

        public async Task<string> GetWeatherAsync(string city)
        {
            try
            {
                var url = $"{_weatherServiceSettings.BaseUrl}?q={city}&appid={_weatherServiceSettings.ApiKey}&units=metric"; // units=metric for Celsius temperature
                var response = await _httpClient.GetStringAsync(url);

                var weatherData = JObject.Parse(response);

                // Check if the response contains weather data
                if (weatherData["cod"].ToString() != "200") // Check if the response status code is "200" (success)
                {
                    return "City not found or invalid response from weather service.";
                }

                // Extracting relevant weather data
                var temperature = weatherData["main"]["temp"].ToString();
                var description = weatherData["weather"][0]["description"].ToString();

                return $"The current temperature in {city} is {temperature}°C with {description}.";
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return $"An error occurred: {ex.Message}. Please try again later.";
            }
        }

    }
}
