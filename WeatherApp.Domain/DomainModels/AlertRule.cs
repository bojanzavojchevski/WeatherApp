using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Domain.DomainModels
{
    public class AlertRule : BaseEntity
    {
        public string? UserId { get; set; }
        public int LocationId { get; set; }
        public Location? Location { get; set; }

        // Thresholds
        public decimal MinTempC {  get; set; }
        public decimal MaxTempC { get; set; }
        public decimal MaxWindMs { get; set; }
        public decimal MinUvIndex { get; set; }
        public decimal MinRainProb { get; set; }

        public bool IsActive { get; set; }
    }
}
