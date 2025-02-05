using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using allAPIs.SimoAPI.Models;
using allAPIs.SimoAPI.Models.InstagramView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace allAPIs.SimoAPI.Controller.Instagram
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritePageController : ControllerBase
    {

        private readonly apiContext _context;

        public FavoritePageController(apiContext context)
        {
            _context = context;
        }

        // افزودن کانال به علاقه‌مندی‌ها
        [HttpPost("add")]
        public async Task<IActionResult> AddToFavorites([FromBody] AddPageFavoriteDto dto)
        {
            // بررسی وجود کانال
            var channel = await _context.Pages.FindAsync(dto.PageId);
            if (channel == null)
            {
                return NotFound(new { Message = "پیج اینستا یافت نشد." });
            }

            // بررسی وجود علاقه‌مندی
            var existingFavorite = await _context.FavoritePagesModels
                .FirstOrDefaultAsync(f => f.UserId == dto.UserId && f.PageId == dto.PageId);

            if (existingFavorite != null)
            {
                return BadRequest(new { Message = "این پیج قبلاً به علاقه‌مندی‌ها اضافه شده است." });
            }

            // افزودن علاقه‌مندی
            var favorite = new FavoritePagesModel
            {
                UserId = dto.UserId,
                PageId = dto.PageId
            };

            _context.FavoritePagesModels.Add(favorite);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "پیج با موفقیت به علاقه‌مندی‌ها اضافه شد." });
        }

        // حذف کانال از علاقه‌مندی‌ها
        [HttpPost("remove")]
        public async Task<IActionResult> RemoveFromFavorites([FromBody] RemoveFavoritePageDto dto)
        {
            var favorite = await _context.FavoritePagesModels
                .FirstOrDefaultAsync(f => f.UserId == dto.UserId && f.PageId == dto.PageId);

            if (favorite == null)
            {
                return NotFound(new { Message = "این پیج در لیست علاقه‌مندی‌ها وجود ندارد." });
            }

            _context.FavoritePagesModels.Remove(favorite);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "پیج با موفقیت از علاقه‌مندی‌ها حذف شد." });
        }


        // نمایش همه علاقه‌مندی‌ها
        [HttpGet("list/{userId}")]
        public async Task<IActionResult> GetFavorites(int userId)
        {
            var favorites = await _context.FavoritePagesModels
                .Where(f => f.UserId == userId)
                .Select(f => new
                {
                    f.PageId,
                    PageName = f.Pages.ShowName,
                    PageDescription = f.Pages.Description
                })  
                .ToListAsync();

            if (!favorites.Any())
            {
                return NotFound(new { Message = "هیچ پیجی در لیست علاقه‌مندی‌ها یافت نشد." });
            }

            return Ok(favorites);
        }
    }
}