using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Domain.Domain_Models
{
    public class FavoriteLocation
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}
