using System.Text.Json.Serialization;

namespace WeatherApp.Web.DTOs
{
    public class MainDto
    {
        public decimal Temp { get; set; }

        [JsonPropertyName("feels_like")]   // this maps JSON "feels_like"
        public decimal FeelsLike { get; set; }

        public int Pressure { get; set; }
        public int Humidity { get; set; }
    }
}
