using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Domain.DomainModels;

namespace WeatherApp.Service.Interfaces
{
    public interface IAlertRuleService
    {
        Task<IEnumerable<AlertRule>> GetAllAsync();
        Task<AlertRule?> GetByIdAsync(int id);
        Task<IEnumerable<AlertRule>> GetByUserIdAsync(string userId);
        Task AddAsync(AlertRule alertRule);
        Task UpdateAsync(AlertRule alertRule);
        Task DeleteAsync(int id);

    }
}
