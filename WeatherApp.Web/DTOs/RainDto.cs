using System.Text.Json.Serialization;

namespace WeatherApp.Web.DTOs
{
    public class RainDto
    {
        [JsonPropertyName("1h")]
        public decimal? OneHour { get; set; }
    }
}
