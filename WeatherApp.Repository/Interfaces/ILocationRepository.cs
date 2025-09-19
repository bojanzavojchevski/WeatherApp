using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Domain.DomainModels;

namespace WeatherApp.Repository.Interfaces
{
    public interface ILocationRepository : IGenericRepository<Location>
    {
        Task<Location?> GetByCoordinatesAsync(decimal latitude, decimal longitude);
        Task<Location?> GetByNameAsync(string name);

    }
}
