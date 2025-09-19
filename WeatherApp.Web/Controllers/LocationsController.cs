using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeatherApp.Domain.DomainModels;
using WeatherApp.Repository.Data;
using WeatherApp.Repository.Interfaces;
using WeatherApp.Service.Interfaces;
using WeatherApp.Web.ViewModels;

namespace WeatherApp.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LocationsController : Controller
    {
        private readonly ILocationService _locationService;

        public LocationsController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        // GET: Locations
        public async Task<IActionResult> Index()
        {
            return View(await _locationService.GetAllAsync());
        }

        // GET: Locations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _locationService.GetByIdAsync(id.Value);

            return View(location);
        }

        // GET: Locations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LocationViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var location = new Location
            {
                Name = model.Name,
                Latitude = model.Latitude,
                Longitude = model.Longitude
            };

            await _locationService.AddAsync(location);
            return RedirectToAction(nameof(Index));
        }

        // GET: Locations/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var location = await _locationService.GetByIdAsync(id);
            if(location == null)
            {
                return NotFound();
            }
            
            var model = new LocationViewModel
            {
                Id = location.Id,
                Name = location.Name,
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };
            return View(model);
        }

        // POST: Locations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LocationViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var location = new Location
            {
                Id = model.Id.Value,
                Name = model.Name,
                Latitude = model.Latitude,
                Longitude = model.Longitude
            };
            
            await _locationService.UpdateAsync(location);
            return RedirectToAction(nameof(Index));
        }

        // GET: Locations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _locationService.GetByIdAsync(id.Value);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _locationService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
