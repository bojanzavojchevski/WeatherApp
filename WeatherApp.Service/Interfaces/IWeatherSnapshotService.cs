using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Domain.DomainModels;
using WeatherApp.Repository.Interfaces;

namespace WeatherApp.Service.Interfaces
{
    public interface IWeatherSnapshotService
    {
        Task<IEnumerable<WeatherSnapshot>> GetAllAsync();
        Task<WeatherSnapshot?> GetByIdAsync(int id);
        Task<IEnumerable<WeatherSnapshot>> GetByLocationIdAsync(int locationId);
        Task<WeatherSnapshot?> GetLatestForLocationAsync(int locationId);
        Task AddAsync(WeatherSnapshot weatherSnapshot);
        Task UpdateAsync(WeatherSnapshot weatherSnapshot);
        Task DeleteAsync(int id);
    }
}
