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
    public class AlertRuleRepository : GenericRepository<AlertRule>, IAlertRuleRepository
    {
        private readonly ApplicationDbContext _context;
        public AlertRuleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AlertRule>> GetByUserIdAsync(string userId)
        {
            return await _context.AlertRules
                .Where(x => x.UserId == userId)
                .Include(a => a.Location)
                .ToListAsync();
        }

        public async Task<IEnumerable<AlertRule>> GetByLocationIdAsync(int locationId)
        {
            return await _context.AlertRules
                .Include(r => r.Location)
                .Where(x => x.LocationId == locationId)
                .ToListAsync();
        }
    }
}
