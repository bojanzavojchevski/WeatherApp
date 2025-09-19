using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Web.ViewModels
{
    public class WeatherSnapshotCreateViewModel
    {
        [Required(ErrorMessage = "Date/Time is required")]
        [Display(Name = "Date/Time Taken")]
        public DateTime TakenAt { get; set; }

        [Required(ErrorMessage = "Temperature is required")]
        [Range(-50, 60, ErrorMessage = "Temperature must be between -50°C and 60°C")]
        [Display(Name = "Temperature (°C)")]
        public decimal TemperatureC { get; set; }

        [Required(ErrorMessage = "Humidity is required")]
        [Range(0, 100, ErrorMessage = "Humidity must be between 0% and 100%")]
        [Display(Name = "Humidity (%)")]
        public int HumidityPercent { get; set; }

        [Required(ErrorMessage = "Wind speed is required")]
        [Range(0, 150, ErrorMessage = "Wind speed must be between 0 and 150 m/s")]
        [Display(Name = "Wind Speed (m/s)")]
        public decimal WindSpeedMs { get; set; }

        [Required(ErrorMessage = "UV Index is required")]
        [Range(0, 11, ErrorMessage = "UV Index must be between 0 and 11")]
        [Display(Name = "UV Index")]
        public decimal UvIndex { get; set; }

        [Required(ErrorMessage = "Rain probability is required")]
        [Range(0, 100, ErrorMessage = "Rain probability must be 0–100%")]
        [Display(Name = "Rain Probability (%)")]
        public decimal RainProbability { get; set; }

        [Required(ErrorMessage = "Please select a location.")]
        [Display(Name = "Location")]
        public int LocationId { get; set; }
    }
}
