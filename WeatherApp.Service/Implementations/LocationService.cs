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
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            return await _locationRepository.GetAllAsync();
        }
        public async Task<Location?> GetByIdAsync(int id)
        {
            return await _locationRepository.GetByIdAsync(id);
        }
        public async Task AddAsync(Location location)
        {
            await _locationRepository.AddAsync(location);
            await _locationRepository.SaveChangesAsync();
        }
        public async Task UpdateAsync(Location location)
        {
            _locationRepository.Update(location);
            await _locationRepository.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var location = await _locationRepository.GetByIdAsync(id);
            if (location != null)
            {
                _locationRepository.Delete(location);
                await _locationRepository.SaveChangesAsync();
            }
        }
        public async Task<Location?> GetByCoordinatesAsync(decimal latitude, decimal longitude)
        {
            return await _locationRepository.GetByCoordinatesAsync(latitude, longitude);
        }

        public async Task<int?> GetLocationIdByNameAsync(string name)
        {
            var location = await _locationRepository.GetByNameAsync(name);
            return location?.Id;
        }
        public async Task<int> CreateLocationAsync(string name)
        {
            var location = new Location { Name = name };
            await _locationRepository.AddAsync(location);
            return location.Id;
        }
        public async Task<Location?> GetByNameAsync(string name)
        {
            return await _locationRepository.GetByNameAsync(name);
        }
    }
}
