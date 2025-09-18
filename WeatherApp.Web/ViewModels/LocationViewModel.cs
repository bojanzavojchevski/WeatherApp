using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Web.ViewModels
{
    public class LocationViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string? Name { get; set; }

        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90")]  
        public decimal Latitude { get; set; }

        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180")]
        public decimal Longitude { get; set; }
        public int? Id { get; set; }
    }
}
