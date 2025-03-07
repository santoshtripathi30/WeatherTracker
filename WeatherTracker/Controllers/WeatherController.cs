using Microsoft.AspNetCore.Mvc;

using WeatherTracker.Services;


namespace WeatherTracker.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetWeather(string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                ViewBag.Message = "Please enter a city name.";
                return View("Index");
            }

            try
            {
                var weatherInfo = await _weatherService.GetWeatherAsync(city);
                if (weatherInfo.Contains("error"))
                {
                    ViewBag.Message = weatherInfo;  // Set the error message if any
                }
                else
                {
                    ViewBag.Weather = weatherInfo;  // Set the weather information if successful
                }
            }
            catch (Exception ex)
            {
                // If an error occurs, display it on the UI
                ViewBag.Message = $"An error occurred: {ex.Message}. Please try again later.";
            }

            return View("~/Views/Weather/Index.cshtml");
        }


    }
}
