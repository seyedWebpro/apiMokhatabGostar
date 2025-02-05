using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using allAPIs.SimoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace allAPIs.SimoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CombinedTariffController : ControllerBase
    {
        private readonly apiContext _context;

        public CombinedTariffController(apiContext context)
        {
            _context = context;
        }

        // ایجاد تعرفه ترکیبی
        [HttpPost("create-combined-tariff")]
        public async Task<IActionResult> CreateCombinedTariff([FromBody] CreateCombinedTariffDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "داده‌های ورودی معتبر نیستند."
                });
            }

            var combinedTariff = new CombinedTariffModel
            {
                Title = dto.Title,
                TariffNames = dto.TariffNames,
                Price = dto.Price
            };

            _context.CombinedTariffModels.Add(combinedTariff);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "تعرفه ترکیبی با موفقیت ایجاد شد.",
                CombinedTariffId = combinedTariff.Id
            });
        }

        // دریافت همه تعرفه‌های ترکیبی
        [HttpGet("get-all-combined-tariffs")]
        public async Task<IActionResult> GetAllCombinedTariffs()
        {
            var combinedTariffs = await _context.CombinedTariffModels.ToListAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "تعرفه‌های ترکیبی با موفقیت دریافت شدند.",
                CombinedTariffs = combinedTariffs
            });
        }

        // دریافت یک تعرفه ترکیبی با شناسه
        [HttpGet("get-combined-tariff/{id}")]
        public async Task<IActionResult> GetCombinedTariffById(int id)
        {
            var combinedTariff = await _context.CombinedTariffModels.FindAsync(id);

            if (combinedTariff == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "تعرفه ترکیبی یافت نشد."
                });
            }

            return Ok(new
            {
                StatusCode = 200,
                Message = "تعرفه ترکیبی با موفقیت دریافت شد.",
                CombinedTariff = combinedTariff
            });
        }

        // ویرایش تعرفه ترکیبی
        [HttpPost("edit-combined-tariff/{id}")]
        public async Task<IActionResult> EditCombinedTariff(int id, [FromBody] UpdateCombinedTariffDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "داده‌های ورودی معتبر نیستند."
                });
            }

            var combinedTariff = await _context.CombinedTariffModels.FindAsync(id);

            if (combinedTariff == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "تعرفه ترکیبی یافت نشد."
                });
            }

            // به‌روزرسانی فیلدها در صورت عدم وجود null
            if (dto.Title != null)
            {
                combinedTariff.Title = dto.Title;
            }

            if (dto.TariffNames != null)
            {
                combinedTariff.TariffNames = dto.TariffNames;
            }

            if (dto.Price.HasValue) // بررسی وجود مقدار برای decimal?
            {
                combinedTariff.Price = dto.Price.Value;
            }

            _context.CombinedTariffModels.Update(combinedTariff);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "تعرفه ترکیبی با موفقیت ویرایش شد."
            });
        }
        // حذف تعرفه ترکیبی
        [HttpPost("delete-combined-tariff/{id}")]
        public async Task<IActionResult> DeleteCombinedTariff(int id)
        {
            var combinedTariff = await _context.CombinedTariffModels.FindAsync(id);

            if (combinedTariff == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "تعرفه ترکیبی یافت نشد."
                });
            }

            _context.CombinedTariffModels.Remove(combinedTariff);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "تعرفه ترکیبی با موفقیت حذف شد."
            });
        }
    }
}