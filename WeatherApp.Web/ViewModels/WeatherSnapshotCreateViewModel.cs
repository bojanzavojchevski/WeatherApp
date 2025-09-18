namespace WeatherApp.Web.ViewModels
{
    public class WeatherSnapshotCreateViewModel
    {
        public DateTime TakenAt { get; set; }
        public decimal TemperatureC { get; set; }
        public int HumidityPercent { get; set; }
        public decimal WindSpeedMs { get; set; }
        public decimal UvIndex { get; set; }
        public decimal RainProbability { get; set; }

        // Only keep the FK, not the navigation property
        public int LocationId { get; set; }
    }
}
