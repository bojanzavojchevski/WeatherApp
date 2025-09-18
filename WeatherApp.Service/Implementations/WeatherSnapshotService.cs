using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Domain.DomainModels;
using WeatherApp.Repository.Interfaces;
using WeatherApp.Service.Interfaces;

namespace WeatherApp.Service.Implementations
{
    public class WeatherSnapshotService : IWeatherSnapshotService
    {
        private readonly IWeatherSnapshotRepository _weatherSnapshotRepository;

        public WeatherSnapshotService(IWeatherSnapshotRepository weatherSnapshotRepository)
        {
            _weatherSnapshotRepository = weatherSnapshotRepository;
        }

        public async Task<IEnumerable<WeatherSnapshot>> GetAllAsync()
        {
            return await _weatherSnapshotRepository.GetAllWithLocationAsync();
        }

        public async Task<WeatherSnapshot?> GetByIdAsync(int id)
        {
            return await _weatherSnapshotRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<WeatherSnapshot>> GetByLocationIdAsync(int locationId)
        {
            return await _weatherSnapshotRepository.GetByLocationIdAsync(locationId);
        }

        public async Task<WeatherSnapshot?> GetLatestForLocationAsync(int locationId)
        {
            return await _weatherSnapshotRepository.GetLatestForLocationAsync(locationId);
        }

        public async Task AddAsync(WeatherSnapshot weatherSnapshot)
        {
            await _weatherSnapshotRepository.AddAsync(weatherSnapshot);
            await _weatherSnapshotRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(WeatherSnapshot weatherSnapshot)
        {
            _weatherSnapshotRepository.Update(weatherSnapshot);
            await _weatherSnapshotRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _weatherSnapshotRepository.GetByIdAsync(id);
            if (entity != null)
            {
                _weatherSnapshotRepository.Delete(entity);
                await _weatherSnapshotRepository.SaveChangesAsync();
            }

        }


    }
}
