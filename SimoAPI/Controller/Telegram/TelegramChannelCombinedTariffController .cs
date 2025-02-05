using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using allAPIs.SimoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace allAPIs.SimoAPI.Controllers.Telegram
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelegramChannelCombinedTariffController : ControllerBase
    {
        private readonly apiContext _context;

        public TelegramChannelCombinedTariffController(apiContext context)
        {
            _context = context;
        }

        // اختصاص تعرفه ترکیبی به کانال تلگرام
        [HttpPost("assign-combined-tariff")]
        public async Task<IActionResult> AssignCombinedTariffToChannel([FromBody] AssignCombinedTariffToTelegramChannelDto dto)
        {
            // بررسی وجود کانال
            var channel = await _context.telegramChannels.FindAsync(dto.TelegramChannelId);
            if (channel == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "کانال تلگرام مورد نظر یافت نشد."
                });
            }

            // بررسی وجود تعرفه ترکیبی
            var combinedTariff = await _context.CombinedTariffModels.FindAsync(dto.CombinedTariffId);
            if (combinedTariff == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "تعرفه ترکیبی مورد نظر یافت نشد."
                });
            }

            // بررسی تکراری نبودن ارتباط
            var existingAssignment = await _context.TelegramChannelCombinedTariffModels
                .FirstOrDefaultAsync(tcct => tcct.TelegramChannelId == dto.TelegramChannelId && tcct.CombinedTariffId == dto.CombinedTariffId);

            if (existingAssignment != null)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "این تعرفه ترکیبی قبلاً به کانال اختصاص داده شده است."
                });
            }

            // ایجاد ارتباط بین کانال و تعرفه ترکیبی
            var channelCombinedTariff = new TelegramChannelCombinedTariffModel
            {
                TelegramChannelId = dto.TelegramChannelId,
                CombinedTariffId = dto.CombinedTariffId,
                Price = dto.Price
            };

            _context.TelegramChannelCombinedTariffModels.Add(channelCombinedTariff);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "تعرفه ترکیبی با موفقیت به کانال تلگرام اختصاص یافت."
            });
        }

        // دریافت تعرفه‌های ترکیبی اختصاص‌یافته به یک کانال تلگرام
        [HttpGet("get-combined-tariffs/{channelId}")]
        public async Task<IActionResult> GetCombinedTariffsForTelegramChannel(int channelId)
        {
            var combinedTariffs = await _context.TelegramChannelCombinedTariffModels
                .Where(tcct => tcct.TelegramChannelId == channelId)
                .Select(tcct => new
                {
                    tcct.CombinedTariffId,
                    CombinedTariffTitle = tcct.CombinedTariff.Title,
                    tcct.Price
                })
                .ToListAsync();

            if (!combinedTariffs.Any())
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "تعرفه ترکیبی برای این کانال یافت نشد."
                });
            }

            return Ok(new
            {
                StatusCode = 200,
                Message = "تعرفه‌های ترکیبی با موفقیت دریافت شدند.",
                CombinedTariffs = combinedTariffs
            });
        }
    }
}