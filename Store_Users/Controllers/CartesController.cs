using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShopClassLibrary;
using ShopClassLibrary.ModelShop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Store_Users.Controllers
{
    [ApiController]
    [Route("api/users/{userId}/cartes")]
    public class CartesController : ControllerBase
    {
        private readonly ICarteService _carteService;

        public CartesController(ICarteService carteService)
        {
            _carteService = carteService;
        }

        // Получение всех карт пользователя
        [HttpGet]
        public async Task<IActionResult> GetCartes(long userId)
        {
            var cartes = await _carteService.GetCartesByUserIdAsync(userId);
            if (cartes == null || !cartes.Any())
            {
                return NotFound($"Карт у пользователя с Id {userId} не найдено.");
            }
            return Ok(cartes);
        }

        // Получение одной карты пользователя
        [HttpGet("{carteId}")]
        public async Task<IActionResult> GetCarteById(long userId, long carteId)
        {
            var carte = await _carteService.GetCarteByUserAsync(userId, carteId);
            if (carte == null)
            {
                return NotFound($"Карта с Id {carteId} у пользователя {userId} не найдена.");
            }
            return Ok(carte);
        }

        // Добавление карты пользователю
        [HttpPost]
        public async Task<IActionResult> AddCarte(long userId, [FromBody] Сarte carte)
        {
            await _carteService.AddCarteAsync(carte, userId);
            return CreatedAtAction(nameof(GetCarteById), new { userId, carteId = carte.Id }, carte);
        }

        // Обновление карты пользователя
        [HttpPut("{carteId}")]
        public async Task<IActionResult> UpdateCarte(long carteId, [FromBody] Сarte carte)
        {
            if (carte.Id != carteId)
            {
                return BadRequest("Id карты не совпадает.");
            }
            await _carteService.UpdateCarteAsync(carte);
            return NoContent();
        }

        // Удаление карты пользователя
        [HttpDelete("{carteId}")]
        public async Task<IActionResult> DeleteCarte(long carteId)
        {
            await _carteService.DeleteCarteAsync(carteId);
            return NoContent();
        }
    }


}