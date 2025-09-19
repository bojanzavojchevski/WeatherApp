namespace WeatherApp.Web.DTOs
{
    public class OpenWeatherResponseDto
    {
        public CoordDto Coord { get; set; }
        public MainDto Main { get; set; }
        public WindDto Wind { get; set; }
        public List<WeatherDescriptionDto> Weather { get; set; }
        public string Name { get; set; } // city name
        public RainDto? Rain { get; set; }
    }
}
