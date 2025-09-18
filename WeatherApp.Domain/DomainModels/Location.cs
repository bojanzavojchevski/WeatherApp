using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Domain.DomainModels
{
    public class Location : BaseEntity
    {
        [Required(ErrorMessage = "Location name is required.")]
        [StringLength(100, ErrorMessage = "Location name cannot exceed 100 characters.")]
        public string? Name { get; set; }

        // coordinates
        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90.")]
        public decimal Latitude { get; set; }
        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180.")]
        public decimal Longitude { get; set; }

        public ICollection<WeatherSnapshot> Snapshots { get; set; } = new List<WeatherSnapshot>();
        public ICollection<FavoriteLocation> Favorites { get; set; } = new List<FavoriteLocation>();
        public ICollection<AlertRule> AlertRules { get; set; } = new List<AlertRule>();
    }
}
