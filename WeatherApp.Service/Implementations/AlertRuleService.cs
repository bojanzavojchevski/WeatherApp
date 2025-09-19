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

        public async Task<IEnumerable<string>> GetTriggeredRuleMessagesAsync(WeatherSnapshot snapshot)
        {
            var rules = await _alertRuleRepository.GetByLocationIdAsync(snapshot.LocationId);

            var messages = new List<string>();

            foreach (var r in rules.Where(r => r.IsActive))
            {
                if (snapshot.TemperatureC < r.MinTempC)
                    messages.Add($"Temperature {snapshot.TemperatureC}°C is below MinTemp {r.MinTempC}°C (Location: {r.Location?.Name})");

                if (snapshot.TemperatureC > r.MaxTempC)
                    messages.Add($"Temperature {snapshot.TemperatureC}°C is above MaxTemp {r.MaxTempC}°C");

                if (snapshot.WindSpeedMs > r.MaxWindMs)
                    messages.Add($"Wind {snapshot.WindSpeedMs} m/s exceeds MaxWind {r.MaxWindMs} m/s");

                if (snapshot.UvIndex < r.MinUvIndex)
                    messages.Add($"UV Index {snapshot.UvIndex} is below MinUV {r.MinUvIndex}");

                if (snapshot.RainProbability < r.MinRainProb)
                    messages.Add($"Rain probability {snapshot.RainProbability}% is below MinRainProb {r.MinRainProb}%");
            }

            return messages;
        }

    }
}
