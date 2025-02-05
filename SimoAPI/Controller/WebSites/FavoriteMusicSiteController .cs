using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using allAPIs.SimoAPI.Models;
using allAPIs.SimoAPI.Models.MusicSiteView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace allAPIs.SimoAPI.Controller.WebSites
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteMusicSiteController : ControllerBase
    {
        private readonly apiContext _context;

        public FavoriteMusicSiteController(apiContext context)
        {
            _context = context;
        }

        // افزودن سایت به علاقه‌مندی‌ها
        [HttpPost("add")]
        public async Task<IActionResult> AddToFavorites([FromBody] AddFavoriteMusicSiteDTO dto)
        {
            // بررسی وجود سایت
            var musicSite = await _context.musicSiteModels.FindAsync(dto.MusicSiteId);
            if (musicSite == null)
            {
                return NotFound(new { Message = "سایت موزیک یافت نشد." });
            }

            // بررسی وجود علاقه‌مندی
            var existingFavorite = await _context.FavoriteMusicSiteModels
                .FirstOrDefaultAsync(f => f.UserId == dto.UserId && f.MusicSiteId == dto.MusicSiteId);

            if (existingFavorite != null)
            {
                return BadRequest(new { Message = "این سایت قبلاً به علاقه‌مندی‌ها اضافه شده است." });
            }

            // افزودن علاقه‌مندی
            var favorite = new FavoriteMusicSiteModel
            {
                UserId = dto.UserId,
                MusicSiteId = dto.MusicSiteId
            };

            _context.FavoriteMusicSiteModels.Add(favorite);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "سایت با موفقیت به علاقه‌مندی‌ها اضافه شد." });
        }

        // حذف سایت از علاقه‌مندی‌ها
        [HttpPost("remove")]
        public async Task<IActionResult> RemoveFromFavorites([FromBody] RemoveFavoriteMusicSiteDTO dto)
        {
            var favorite = await _context.FavoriteMusicSiteModels   
                .FirstOrDefaultAsync(f => f.UserId == dto.UserId && f.MusicSiteId == dto.MusicSiteId);

            if (favorite == null)
            {
                return NotFound(new { Message = "این سایت در لیست علاقه‌مندی‌ها وجود ندارد." });
            }

            _context.FavoriteMusicSiteModels.Remove(favorite);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "سایت با موفقیت از علاقه‌مندی‌ها حذف شد." });
        }

        // نمایش همه علاقه‌مندی‌ها
        [HttpGet("list/{userId}")]
        public async Task<IActionResult> GetFavorites(int userId)
        {
            var favorites = await _context.FavoriteMusicSiteModels
                .Where(f => f.UserId == userId)
                .Select(f => new
                {
                    f.MusicSiteId,
                    SiteName = f.MusicSite.SiteName,
                    SiteTopic = f.MusicSite.SiteTopic
                })
                .ToListAsync();

            if (!favorites.Any())
            {
                return NotFound(new { Message = "هیچ سایت موزیکی در لیست علاقه‌مندی‌ها یافت نشد." });
            }

            return Ok(favorites);
        }
    }
}