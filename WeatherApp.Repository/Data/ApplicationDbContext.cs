using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeatherApp.Domain.DomainModels;
using WeatherApp.Domain.IdentityModels;

namespace WeatherApp.Repository.Data
{
    public class ApplicationDbContext : IdentityDbContext<WeatherAppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


    }
}
