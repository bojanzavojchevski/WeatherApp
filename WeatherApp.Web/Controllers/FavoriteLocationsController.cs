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

namespace WeatherApp.Web.Controllers
{
    [Authorize]
    public class FavoriteLocationsController : Controller
    {
        private readonly IFavoriteLocationService _favoriteLocationService;

        public FavoriteLocationsController(IFavoriteLocationService favoriteLocationService)
        {
            _favoriteLocationService = favoriteLocationService;
        }

        // GET: FavoriteLocations
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var favorites = await _favoriteLocationService.GetUserFavoritesAsync(userId);
            return View(favorites);
        }


        // GET: FavoriteLocations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FavoriteLocations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int locationId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(!string.IsNullOrEmpty(userId))
            {
                await _favoriteLocationService.AddFavoriteAsync(userId, locationId);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // GET: FavoriteLocations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var favorites = await _favoriteLocationService.GetUserFavoritesAsync(userId);
            var favorite = favorites.FirstOrDefault(f => f.Id == id);

            if (favorite == null)
            {
                return NotFound();
            }

            return View(favorite);
        }

        // POST: FavoriteLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _favoriteLocationService.RemoveFavoriteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
