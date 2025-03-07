namespace WeatherTracker.Services
{
    public interface IWeatherService
    {
        public Task<string> GetWeatherAsync(string city);
    }
}
