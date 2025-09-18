using System;
using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Web.ViewModels
{
    public class WeatherSnapshotEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime TakenAt { get; set; }

        [Range(-100, 100)]
        public decimal TemperatureC { get; set; }

        [Range(0, 100)]
        public int HumidityPercent { get; set; }

        public decimal WindSpeedMs { get; set; }
        public decimal UvIndex { get; set; }
        public decimal RainProbability { get; set; }

        [Required]
        public int LocationId { get; set; }
    }
}
