using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Domain.DomainModels;
using WeatherApp.Service.Interfaces;
using WeatherApp.Repository.Interfaces;

namespace WeatherApp.Service.Implementations
{
    public class FavoriteLocationService : IFavoriteLocationService
    {
        private readonly IFavoriteLocationRepository _favoriteLocationRepository;
        public FavoriteLocationService(IFavoriteLocationRepository favoriteLocationRepository)
        {
            _favoriteLocationRepository = favoriteLocationRepository;
        }

        public async Task<IEnumerable<FavoriteLocation>> GetUserFavoritesAsync(string userId)
        {
            return await _favoriteLocationRepository.GetByUserIdAsync(userId);
        }
        public async Task AddFavoriteAsync(string userId, int locationId)
        {
            FavoriteLocation favorite = new FavoriteLocation
            {
                UserId = userId,
                LocationId = locationId
            };

            await _favoriteLocationRepository.AddAsync(favorite);
            await _favoriteLocationRepository.SaveChangesAsync();
        }

        public async Task RemoveFavoriteAsync(int favoriteId)
        {
            var favorite = await _favoriteLocationRepository.GetByIdAsync(favoriteId);
            if (favorite != null)
            {
                _favoriteLocationRepository.Delete(favorite);
                await _favoriteLocationRepository.SaveChangesAsync();
            }
        }
    }
}
