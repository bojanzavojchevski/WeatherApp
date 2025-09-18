using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Domain.IdentityModels;

namespace WeatherApp.Domain.DomainModels
{
    public class FavoriteLocation : BaseEntity
    {
        [Required(ErrorMessage = "User ID is required.")]
        public string? UserId { get; set; }

        [Required(ErrorMessage = "Location ID is required.")]
        public int LocationId { get; set; }


        public virtual Location? Location { get; set; }
        public virtual WeatherAppUser? User { get; set; }
    }
}
