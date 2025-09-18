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
    public class WeatherSnapshotRepository : GenericRepository<WeatherSnapshot>, IWeatherSnapshotRepository
    {
        private readonly ApplicationDbContext _context;

        public WeatherSnapshotRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WeatherSnapshot>> GetByLocationIdAsync(int locationId)
        {
            return await _context.WeatherSnapshots
                .Where(ws => ws.LocationId == locationId)
                .OrderByDescending(ws => ws.TakenAt)
                .ToListAsync();
        }

        public async Task<WeatherSnapshot?> GetLatestForLocationAsync(int locationId)
        {
            return await _context.WeatherSnapshots
                .Where(ws => ws.LocationId == locationId)
                .OrderByDescending(ws => ws.TakenAt)
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<WeatherSnapshot>> GetAllWithLocationAsync()
        {
            return await _context.WeatherSnapshots
                .Include(ws => ws.Location)
                .ToListAsync();
        }
    }
}
