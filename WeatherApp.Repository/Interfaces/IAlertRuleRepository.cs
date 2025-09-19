using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Domain.DomainModels;

namespace WeatherApp.Repository.Interfaces
{
    public interface IAlertRuleRepository : IGenericRepository<AlertRule>
    {
        Task<IEnumerable<AlertRule>> GetByUserIdAsync(string userId);
        Task<IEnumerable<AlertRule>> GetByLocationIdAsync(int locationId);
    }
}
