using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Domain.DomainModels;

namespace WeatherApp.Repository.Interfaces
{
    public interface IWeatherSnapshotRepository : IGenericRepository<WeatherSnapshot>
    {
        Task<IEnumerable<WeatherSnapshot>> GetAllWithLocationAsync();
        Task<IEnumerable<WeatherSnapshot>> GetByLocationIdAsync(int locationId);
        Task<WeatherSnapshot?> GetLatestForLocationAsync(int locationId);
    }
}
