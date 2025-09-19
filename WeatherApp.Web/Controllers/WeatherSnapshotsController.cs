using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Domain.DomainModels;
using WeatherApp.Repository.Data;
using WeatherApp.Service.Implementations;
using WeatherApp.Service.Interfaces;
using WeatherApp.Web.Services;
using WeatherApp.Web.ViewModels;

namespace WeatherApp.Web.Controllers
{
    public class WeatherSnapshotsController : Controller
    {
        private readonly IWeatherSnapshotService _weatherSnapshotService;
        private readonly ILocationService _locationService;
        private readonly IAlertRuleService _alertRuleService;
        
        private readonly OpenWeatherService _openWeatherService;

        public WeatherSnapshotsController(IWeatherSnapshotService weatherSnapshotService, ILocationService locationService, IAlertRuleService alertRuleService, OpenWeatherService openWeatherService)
        {
            _weatherSnapshotService = weatherSnapshotService;
            _locationService = locationService;
            _alertRuleService = alertRuleService;
            _openWeatherService = openWeatherService;
        }

        // GET: WeatherSnapshots
        public async Task<IActionResult> Index()
        {
            return View(await _weatherSnapshotService.GetAllAsync());
        }

        // GET: WeatherSnapshots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weatherSnapshot = await _weatherSnapshotService.GetByIdAsync(id.Value);
            if (weatherSnapshot == null)
            {
                return NotFound();
            }

            var messages = await _alertRuleService.GetTriggeredRuleMessagesAsync(weatherSnapshot);
            ViewData["AlertMessages"] = messages.ToList();

            return View(weatherSnapshot);
        }

        // GET: WeatherSnapshots/Create
        public async Task<IActionResult> Create()
        {
            ViewData["LocationId"] = new SelectList(
                await _locationService.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: WeatherSnapshots/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WeatherSnapshotCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["LocationId"] = new SelectList(
                    await _locationService.GetAllAsync(), "Id", "Name", model.LocationId);
                return View(model);
            }

            var snapshot = new WeatherSnapshot
            {
                TakenAt = model.TakenAt,
                TemperatureC = model.TemperatureC,
                HumidityPercent = model.HumidityPercent,
                WindSpeedMs = model.WindSpeedMs,
                UvIndex = model.UvIndex,
                RainProbability = model.RainProbability,
                LocationId = model.LocationId
            };

            await _weatherSnapshotService.AddAsync(snapshot);

            // NEW: check alert rules
            var messages = await _alertRuleService.GetTriggeredRuleMessagesAsync(snapshot);
            if (messages.Any())
            {
                TempData["AlertMessages"] = string.Join("<br/>", messages);
            }



            return RedirectToAction(nameof(Index));
        }

        // GET: WeatherSnapshots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var weatherSnapshot = await _weatherSnapshotService.GetByIdAsync(id.Value);
            if (weatherSnapshot == null) return NotFound();

            var vm = new WeatherSnapshotEditViewModel
            {
                Id = weatherSnapshot.Id,
                TakenAt = weatherSnapshot.TakenAt,
                TemperatureC = weatherSnapshot.TemperatureC,
                HumidityPercent = weatherSnapshot.HumidityPercent,
                WindSpeedMs = weatherSnapshot.WindSpeedMs,
                UvIndex = weatherSnapshot.UvIndex,
                RainProbability = weatherSnapshot.RainProbability,
                LocationId = weatherSnapshot.LocationId
            };

            ViewData["LocationId"] = new SelectList(
                await _locationService.GetAllAsync(), "Id", "Name", vm.LocationId);

            return View(vm);
        }

        // POST: WeatherSnapshots/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WeatherSnapshotEditViewModel model)
        {
            if (id != model.Id) return BadRequest();

            if (!ModelState.IsValid)
            {
                ViewData["LocationId"] = new SelectList(
                    await _locationService.GetAllAsync(), "Id", "Name", model.LocationId);
                return View(model);
            }

            var snapshot = new WeatherSnapshot
            {
                Id = model.Id,
                TakenAt = model.TakenAt,
                TemperatureC = model.TemperatureC,
                HumidityPercent = model.HumidityPercent,
                WindSpeedMs = model.WindSpeedMs,
                UvIndex = model.UvIndex,
                RainProbability = model.RainProbability,
                LocationId = model.LocationId
            };

            await _weatherSnapshotService.UpdateAsync(snapshot);
            return RedirectToAction(nameof(Index));
        }

        // GET: WeatherSnapshots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weatherSnapshot = await _weatherSnapshotService.GetByIdAsync(id.Value);
            if (weatherSnapshot == null)
            {
                return NotFound();
            }

            return View(weatherSnapshot);
        }

        // POST: WeatherSnapshots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _weatherSnapshotService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: show the search form
        [HttpGet]
        public IActionResult FetchWeather()
        {
            return View();
        }

        // POST: handle form and show results
        [HttpPost]
        public async Task<IActionResult> FetchWeather(string city)
        {
            var result = await _openWeatherService.GetWeatherAsync(city);

            if (result == null)
                return Content("Could not fetch weather data.");

            // Check if location exists
            var location = await _locationService.GetByNameAsync(city);

            // If not, create it
            if (location == null)
            {
                location = new Location { Name = city };
                await _locationService.AddAsync(location);
            }

            // Save snapshot
            var snapshot = new WeatherSnapshot
            {
                TakenAt = DateTime.UtcNow,
                TemperatureC = result.Main.Temp,
                HumidityPercent = result.Main.Humidity,
                WindSpeedMs = result.Wind.Speed,
                UvIndex = 0, // OpenWeather doesn’t return UV directly
                RainProbability = 0, // Simplify for now
                LocationId = location.Id
            };

            await _weatherSnapshotService.AddAsync(snapshot);

            ViewBag.City = city;
            return View("WeatherResult", result); // goes to your nice WeatherResult.cshtml
        }
    }
}
