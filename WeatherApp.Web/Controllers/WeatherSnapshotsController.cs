using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using WeatherApp.Domain.DomainModels;
using WeatherApp.Repository.Data;
using WeatherApp.Repository.Interfaces;
using WeatherApp.Service.Interfaces;
using WeatherApp.Web.ViewModels;

namespace WeatherApp.Web.Controllers
{
    public class WeatherSnapshotsController : Controller
    {
        private readonly IWeatherSnapshotService _weatherSnapshotService;
        private readonly ILocationService _locationService;

        public WeatherSnapshotsController(IWeatherSnapshotService weatherSnapshotService, ILocationService locationService)
        {
            _weatherSnapshotService = weatherSnapshotService;
            _locationService = locationService;
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

            return View(weatherSnapshot);
        }

        // GET: WeatherSnapshots/Create
        public async Task<IActionResult> Create()
        {
            ViewData["LocationId"] = new SelectList(await _locationService.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: WeatherSnapshots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WeatherSnapshotCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["LocationId"] = new SelectList(await _locationService.GetAllAsync(), "Id", "Name", model.LocationId);
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
            return RedirectToAction(nameof(Index));
        }

        // GET: WeatherSnapshots/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var snapshot = await _weatherSnapshotService.GetByIdAsync(id);
            if(snapshot == null)
            {
                return NotFound();
            }
            return View(snapshot);
        }

        // POST: WeatherSnapshots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TakenAt,TemperatureC,HumidityPercent,WindSpeedMs,UvIndex,RainProbability,LocationId")] WeatherSnapshot weatherSnapshot)
        {
            if(id != weatherSnapshot.Id)
            {
                return BadRequest();
            }
            if(!ModelState.IsValid)
            {
                return View(weatherSnapshot);
            }
            await _weatherSnapshotService.UpdateAsync(weatherSnapshot);
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

        //private bool WeatherSnapshotExists(int id)
        //{
        //    return _context.WeatherSnapshots.Any(e => e.Id == id);
        //}
    }
}

