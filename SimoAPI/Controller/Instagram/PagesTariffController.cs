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
    public class PagesTariffController : ControllerBase
    {
                private readonly apiContext _context;

        public PagesTariffController(apiContext context)
        {
            _context = context;
        }

        [HttpPost("assign-tariff")]
        public async Task<IActionResult> AssignTariffToInstagramPage([FromBody] AssignTariffInstagramPageDto dto)
        {
            // بررسی وجود کانال
            var page = await _context.Pages.FindAsync(dto.InstagramPageId);
            if (page == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "پیج مورد نظر یافت نشد."
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

            // ایجاد ارتباط بین پیج و تعرفه
            var pageTariff = new PageTariffModel
            {
                PageId = dto.InstagramPageId,
                TariffId = dto.TariffId,
                Price = dto.Price
            };

            _context.PageTariffModels.Add(pageTariff);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "تعرفه با موفقیت به پیج اینستاگرام اختصاص یافت."
            });
        }

        [HttpGet("get-tariffs/{pageId}")]
        public async Task<IActionResult> GetTariffsForInstagramPage(int pageId)
        {
            var tariffs = await _context.PageTariffModels
                .Where(tct => tct.PageId == pageId)
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
                    Message = "تعرفه‌ای برای این پیج یافت نشد."
                });
            }

            return Ok(tariffs);
        }
    }
}