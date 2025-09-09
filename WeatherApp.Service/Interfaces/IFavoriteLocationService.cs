using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Domain.DomainModels;

namespace WeatherApp.Service.Interfaces
{
    public interface IFavoriteLocationService
    {
        Task<IEnumerable<FavoriteLocation>> GetUserFavoritesAsync(string userId);
        Task AddFavoriteAsync(string userId, int locationId);
        Task RemoveFavoriteAsync(int locationId);
    }
}
