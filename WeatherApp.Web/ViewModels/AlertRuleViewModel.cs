using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Web.ViewModels
{
    public class AlertRuleViewModel
    {
        public int? Id { get; set; } 

        [Required]
        public int? LocationId { get; set; }
        public string? LocationName { get; set; }

        [Range(-50, 60, ErrorMessage = "Min Temp must be between -50 and 60 °C")]
        public decimal MinTempC { get; set; }

        [Range(-50, 60, ErrorMessage = "Max Temp must be between -50 and 60 °C")]
        public decimal MaxTempC { get; set; }

        [Range(0, 150, ErrorMessage = "Wind speed must be between 0 and 150 m/s")]
        public decimal MaxWindMs { get; set; }

        [Range(0, 11, ErrorMessage = "UV Index must be between 0 and 11")]
        public decimal MinUvIndex { get; set; }

        [Range(0, 100, ErrorMessage = "Rain probability must be 0–100%")]
        public decimal MinRainProb { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;
    }
}
