using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Service.Interfaces;
using WeatherApp.Web.ViewModels;

namespace WeatherApp.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertRulesApiController : ControllerBase
    {
        private readonly IAlertRuleService _alertRuleService;

        public AlertRulesApiController(IAlertRuleService alertRuleService)
        {
            _alertRuleService = alertRuleService;
        }

        // GET: api/AlertRules
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var alertRules = await _alertRuleService.GetAllAsync();
            return Ok(alertRules);
        }

        // GET: api/AlertRules/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rule = await _alertRuleService.GetByIdAsync(id);
            if (rule == null)
            {
                return NotFound();
            }
            return Ok(rule);
        }


        // POST: api/AlertRules
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AlertRuleViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var rule = new WeatherApp.Domain.DomainModels.AlertRule
            {
                LocationId = model.LocationId!,
                MinTempC = model.MinTempC,
                MaxTempC = model.MaxTempC,
                MaxWindMs = model.MaxWindMs,
                MinUvIndex = model.MinUvIndex,
                MinRainProb = model.MinRainProb,
                IsActive = model.IsActive
            };

            await _alertRuleService.AddAsync(rule);

            return CreatedAtAction(nameof(GetById), new { id = rule.Id }, rule);
        }

        // PUT: api/AlertRules/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AlertRuleViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var rule = await _alertRuleService.GetByIdAsync(id);
            if (rule == null) return NotFound();

            rule.MinTempC = model.MinTempC;
            rule.MaxTempC = model.MaxTempC;
            rule.MaxWindMs = model.MaxWindMs;
            rule.MinUvIndex = model.MinUvIndex;
            rule.MinRainProb = model.MinRainProb;
            rule.IsActive = model.IsActive;

            await _alertRuleService.UpdateAsync(rule);

            return NoContent();
        }

        // DELETE: api/AlertRules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rule = await _alertRuleService.GetByIdAsync(id);
            if (rule == null) return NotFound();

            await _alertRuleService.DeleteAsync(id);

            return NoContent();
        }


    }
}
