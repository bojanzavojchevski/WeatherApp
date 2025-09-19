using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Web.ViewModels
{
    public class AlertRuleViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please select a location.")]
        [Display(Name = "Location")]
        public int LocationId { get; set; }

        public string? LocationName { get; set; }

        [Required(ErrorMessage = "Minimum temperature is required")]
        [Range(-50, 60, ErrorMessage = "Min Temp must be between -50 and 60 °C")]
        [Display(Name = "Min Temperature (°C)")]
        public decimal MinTempC { get; set; }

        [Required(ErrorMessage = "Maximum temperature is required")]
        [Range(-50, 60, ErrorMessage = "Max Temp must be between -50 and 60 °C")]
        [Display(Name = "Max Temperature (°C)")]
        public decimal MaxTempC { get; set; }

        [Required(ErrorMessage = "Maximum wind speed is required")]
        [Range(0, 150, ErrorMessage = "Wind speed must be between 0 and 150 m/s")]
        [Display(Name = "Max Wind (m/s)")]
        public decimal MaxWindMs { get; set; }

        [Required(ErrorMessage = "Minimum UV index is required")]
        [Range(0, 11, ErrorMessage = "UV Index must be between 0 and 11")]
        [Display(Name = "Min UV Index")]
        public decimal MinUvIndex { get; set; }

        [Required(ErrorMessage = "Minimum rain probability is required")]
        [Range(0, 100, ErrorMessage = "Rain probability must be 0–100%")]
        [Display(Name = "Min Rain Probability (%)")]
        public decimal MinRainProb { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;
    }
}
