using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Domain.DomainModels;

namespace WeatherApp.Repository.Interfaces
{
    public interface IFavoriteLocationRepository : IGenericRepository<FavoriteLocation>
    {
        Task<IEnumerable<FavoriteLocation>> GetByUserIdAsync(string userId);
    }
}
