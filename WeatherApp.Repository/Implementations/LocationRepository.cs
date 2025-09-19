using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Domain.DomainModels;
using WeatherApp.Repository.Data;
using WeatherApp.Repository.Interfaces;

namespace WeatherApp.Repository.Implementations
{
    public class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        private readonly ApplicationDbContext _context;

        public LocationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Location?> GetByCoordinatesAsync(decimal latitude, decimal longitude)
        {
            return await _context.Locations
                .FirstOrDefaultAsync(loc => loc.Latitude == latitude && loc.Longitude == longitude);
        }

        public async Task<Location?> GetByNameAsync(string name)
        {
            return await _context.Locations.FirstOrDefaultAsync(l => l.Name == name);
        }
    }
}
