using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Domain.Domain_Models
{
    public class WeatherSnapshot
    {
        public int Id { get; set; }
        public DateTime TakenAt { get; set; }

        // Core Metrics
        public decimal TemperatureC {  get; set; }
        public int HumidityPercent { get; set; }
        public decimal WindSpeedMs { get; set; }
        public decimal UvIndex { get; set; }
        public decimal RainProbability { get; set; }

        // FK
        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}
