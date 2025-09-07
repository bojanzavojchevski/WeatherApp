using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Domain.DomainModels
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // coordinates
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public ICollection<WeatherSnapshot> Snapshots { get; set; } = new List<WeatherSnapshot>();
        public ICollection<FavoriteLocation> Favorites { get; set; } = new List<FavoriteLocation>();
        public ICollection<AlertRule> AlertRules { get; set; } = new List<AlertRule>();
    }
}
