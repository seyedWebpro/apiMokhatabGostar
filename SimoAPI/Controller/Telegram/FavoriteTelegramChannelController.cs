using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using allAPIs.SimoAPI.Models;
using allAPIs.SimoAPI.Models.TelegramView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace allAPIs.SimoAPI.Controller.Telegram
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteTelegramChannelController : ControllerBase
    {
        private readonly apiContext _context;

        public FavoriteTelegramChannelController(apiContext context)
        {
            _context = context;
        }

        // افزودن کانال به علاقه‌مندی‌ها
        [HttpPost("add")]
        public async Task<IActionResult> AddToFavorites([FromBody] AddFavoriteDto dto)
        {
            // بررسی وجود کانال
            var channel = await _context.telegramChannels.FindAsync(dto.TelegramChannelId);
            if (channel == null)
            {
                return NotFound(new { Message = "کانال تلگرام یافت نشد." });
            }

            // بررسی وجود علاقه‌مندی
            var existingFavorite = await _context.FavoriteTelegramChannels
                .FirstOrDefaultAsync(f => f.UserId == dto.UserId && f.TelegramChannelId == dto.TelegramChannelId);

            if (existingFavorite != null)
            {
                return BadRequest(new { Message = "این کانال قبلاً به علاقه‌مندی‌ها اضافه شده است." });
            }

            // افزودن علاقه‌مندی
            var favorite = new FavoriteTelegramChannelModel
            {
                UserId = dto.UserId,
                TelegramChannelId = dto.TelegramChannelId
            };

            _context.FavoriteTelegramChannels.Add(favorite);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "کانال با موفقیت به علاقه‌مندی‌ها اضافه شد." });
        }

        // حذف کانال از علاقه‌مندی‌ها
        [HttpPost("remove")]
        public async Task<IActionResult> RemoveFromFavorites([FromBody] RemoveFavoriteDto dto)
        {
            var favorite = await _context.FavoriteTelegramChannels
                .FirstOrDefaultAsync(f => f.UserId == dto.UserId && f.TelegramChannelId == dto.TelegramChannelId);

            if (favorite == null)
            {
                return NotFound(new { Message = "این کانال در لیست علاقه‌مندی‌ها وجود ندارد." });
            }

            _context.FavoriteTelegramChannels.Remove(favorite);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "کانال با موفقیت از علاقه‌مندی‌ها حذف شد." });
        }

        // نمایش همه علاقه‌مندی‌ها
        [HttpGet("list/{userId}")]
        public async Task<IActionResult> GetFavorites(int userId)
        {
            var favorites = await _context.FavoriteTelegramChannels
                .Where(f => f.UserId == userId)
                .Select(f => new
                {
                    f.TelegramChannelId,
                    ChannelName = f.TelegramChannel.Name,
                    ChannelTopic = f.TelegramChannel.Topic
                })
                .ToListAsync();

            if (!favorites.Any())
            {
                return NotFound(new { Message = "هیچ کانالی در لیست علاقه‌مندی‌ها یافت نشد." });
            }

            return Ok(favorites);
        }
    }
}