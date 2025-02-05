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
    public class TelegramChannelTariffController : ControllerBase
    {
        private readonly apiContext _context;

        public TelegramChannelTariffController(apiContext context)
        {
            _context = context;
        }

        [HttpPost("assign-tariff")]
        public async Task<IActionResult> AssignTariffToTelegramChannel([FromBody] AssignTariffTelegramChannelDto dto)
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

            // بررسی وجود تعرفه
            var tariff = await _context.tarefeModels.FindAsync(dto.TariffId);
            if (tariff == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "تعرفه مورد نظر یافت نشد."
                });
            }

            // ایجاد ارتباط بین کانال و تعرفه
            var channelTariff = new TelegramChannelTariffModel
            {
                TelegramChannelId = dto.TelegramChannelId,
                TariffId = dto.TariffId,
                Price = dto.Price
            };

            _context.telegramChannelTariffModels.Add(channelTariff);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "تعرفه با موفقیت به کانال تلگرام اختصاص یافت."
            });
        }

        [HttpGet("get-tariffs/{channelId}")]
        public async Task<IActionResult> GetTariffsForTelegramChannel(int channelId)
        {
            var tariffs = await _context.telegramChannelTariffModels
                .Where(tct => tct.TelegramChannelId == channelId)
                .Select(tct => new
                {
                    tct.TariffId,
                    TariffName = tct.Tariff.Name,
                    tct.Price
                })
                .ToListAsync();

            if (!tariffs.Any())
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "تعرفه‌ای برای این کانال یافت نشد."
                });
            }

            return Ok(tariffs);
        }
    }

}