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
    public class AlertRuleService : IAlertRuleService
    {
        private readonly IAlertRuleRepository _alertRuleRepository;

        public AlertRuleService(IAlertRuleRepository alertRuleRepository)
        {
            _alertRuleRepository = alertRuleRepository;
        }

        public async Task<IEnumerable<AlertRule>> GetAllAsync()
        {
            return await _alertRuleRepository.GetAllAsync();
        }

        public async Task<AlertRule?> GetByIdAsync(int id)
        {
            return await _alertRuleRepository.GetByIdAsync(id);
        }
        
        public async Task<IEnumerable<AlertRule>> GetByUserIdAsync(string userId)
        {
            return await _alertRuleRepository.GetByUserIdAsync(userId);
        }

        public async Task AddAsync(AlertRule alertRule)
        {
            await _alertRuleRepository.AddAsync(alertRule);
            await _alertRuleRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(AlertRule alertRule)
        {
            _alertRuleRepository.Update(alertRule);
            await _alertRuleRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _alertRuleRepository.GetByIdAsync(id);
            if(item != null)
            {
                _alertRuleRepository.Delete(item);
                await _alertRuleRepository.SaveChangesAsync();
            }
        }
            
            
    }
}
