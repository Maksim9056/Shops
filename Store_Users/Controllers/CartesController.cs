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

        [HttpGet]
        public async Task<IActionResult> GetCartes(long userId) =>
            Ok(await _carteService.GetCartesByUserIdAsync(userId));

        [HttpPost]
        public async Task<IActionResult> AddCarte(long userId, [FromBody] Ñarte carte)
        {
            await _carteService.AddCarteAsync(carte, userId);
            return CreatedAtAction(nameof(GetCartes), new { userId }, carte);
        }

        [HttpPut("{carteId}")]
        public async Task<IActionResult> UpdateCarte(long carteId, [FromBody] Ñarte carte)
        {
            if (carte.Id != carteId) return BadRequest();
            await _carteService.UpdateCarteAsync(carte);
            return NoContent();
        }

        [HttpDelete("{carteId}")]
        public async Task<IActionResult> DeleteCarte(long carteId)
        {
            await _carteService.DeleteCarteAsync(carteId);
            return NoContent();
        }
    }
}