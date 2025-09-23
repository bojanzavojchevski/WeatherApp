using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WeatherApp.Domain.DomainModels;
using WeatherApp.Repository.Data;
using WeatherApp.Service.Interfaces;
using WeatherApp.Web.ViewModels;

namespace WeatherApp.Web.Controllers
{
    [Authorize]
    public class AlertRulesController : Controller
    {
        private readonly IAlertRuleService _alertRuleService;
        private readonly ILocationService _locationService;

        public AlertRulesController(IAlertRuleService alertRuleService, ILocationService locationService)
        {
            _alertRuleService = alertRuleService;
            _locationService = locationService;
        }

        // GET: AlertRules
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            var alertRules = await _alertRuleService.GetByUserIdAsync(userId);

            var models = alertRules.Select(r => new AlertRuleViewModel
            {
                Id = r.Id,
                LocationId = r.LocationId,
                LocationName = r.Location?.Name ?? "-", // Assuming Location is included in the service method
                MinTempC = r.MinTempC,
                MaxTempC = r.MaxTempC,
                MaxWindMs = r.MaxWindMs,
                MinUvIndex = r.MinUvIndex,
                MinRainProb = r.MinRainProb,
                IsActive = r.IsActive
            }).ToList();

            return View(models);
        }

        // GET: AlertRules/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var alertRule = await _alertRuleService.GetByIdAsync(id);
            if (alertRule == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(alertRule.UserId != userId)
            {
                return Forbid();
            }

            var model = new AlertRuleViewModel
            {
                Id = alertRule.Id,
                LocationId = alertRule.LocationId,
                MinTempC = alertRule.MinTempC,
                MaxTempC = alertRule.MaxTempC,
                MaxWindMs = alertRule.MaxWindMs,
                MinUvIndex = alertRule.MinUvIndex,
                MinRainProb = alertRule.MinRainProb,
                IsActive = alertRule.IsActive
            };

            return View(model);
        }

        // GET: AlertRules/Create
        public async Task<IActionResult> Create()
        {
            ViewData["LocationId"] = new SelectList(await _locationService.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: AlertRules/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlertRuleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["LocationId"] = new SelectList(await _locationService.GetAllAsync(), "Id", "Name", model.LocationId);
                return View(model);
            }

            var alertRule = new AlertRule
            {
                LocationId = model.LocationId,
                MinTempC = model.MinTempC,
                MaxTempC = model.MaxTempC,
                MaxWindMs = model.MaxWindMs,
                MinUvIndex = model.MinUvIndex,
                MinRainProb = model.MinRainProb,
                IsActive = model.IsActive,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            
            await _alertRuleService.AddAsync(alertRule);
            return RedirectToAction(nameof(Index));
        }

        // GET: AlertRules/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var alertRule = await _alertRuleService.GetByIdAsync(id);
            if(alertRule == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(alertRule.UserId != userId)
            {
                return Forbid();
            }

            var item = new AlertRuleViewModel
            {
                Id = alertRule.Id,
                LocationId = alertRule.LocationId,
                MinTempC = alertRule.MinTempC,
                MaxTempC = alertRule.MaxTempC,
                MaxWindMs = alertRule.MaxWindMs,
                MinUvIndex = alertRule.MinUvIndex,
                MinRainProb = alertRule.MinRainProb,
                IsActive = alertRule.IsActive
            };

            ViewData["LocationId"] = new SelectList(await _locationService.GetAllAsync(), "Id", "Name", alertRule.LocationId);
            return View(item);

        }

        // POST: AlertRules/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AlertRuleViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                ViewData["LocationId"] = new SelectList(await _locationService.GetAllAsync(), "Id", "Name", model.LocationId);
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var alertRule = await _alertRuleService.GetByIdAsync(id);

            if (alertRule == null)
            {
                return NotFound();
            }

            if (alertRule.UserId != userId)
            {
                return Forbid();
            }

            // Update allowed fields only
            alertRule.LocationId = model.LocationId;
            alertRule.MinTempC = model.MinTempC;
            alertRule.MaxTempC = model.MaxTempC;
            alertRule.MaxWindMs = model.MaxWindMs;
            alertRule.MinUvIndex = model.MinUvIndex;
            alertRule.MinRainProb = model.MinRainProb;
            alertRule.IsActive = model.IsActive;

            await _alertRuleService.UpdateAsync(alertRule);
            return RedirectToAction(nameof(Index));
        }


        // GET: AlertRules/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var alertRule = await _alertRuleService.GetByIdAsync(id);
            if (alertRule == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (alertRule.UserId != userId)
            {
                return Forbid();
            }

            var model = new AlertRuleViewModel
            {
                Id = alertRule.Id,
                LocationId = alertRule.LocationId,
                MinTempC = alertRule.MinTempC,
                MaxTempC = alertRule.MaxTempC,
                MaxWindMs = alertRule.MaxWindMs,
                MinUvIndex = alertRule.MinUvIndex,
                MinRainProb = alertRule.MinRainProb,
                IsActive = alertRule.IsActive
            };

            return View(model);
        }

        // POST: AlertRules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alertRule = await _alertRuleService.GetByIdAsync(id);
            if (alertRule == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (alertRule.UserId != userId)
            {
                return Forbid();
            }

            await _alertRuleService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
