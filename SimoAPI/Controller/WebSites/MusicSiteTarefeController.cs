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
    public class MusicSiteTarefeController : ControllerBase
    {
        private readonly apiContext _context;

        public MusicSiteTarefeController(apiContext context)
        {
            _context = context;
        }

        [HttpPost("assign-tariff")]
        public async Task<IActionResult> AssignTariffToMusicSite([FromBody] AssignTariffMusicSiteDto dto)
        {
            // بررسی وجود سایت
            var site = await _context.musicSiteModels.FindAsync(dto.MusicSiteId);
            if (site == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "سایت موزیک مورد نظر یافت نشد."
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

            // ایجاد ارتباط بین سایت و تعرفه
            var musicSiteTariff = new MusicSiteTarefeModel
            {
                MusicSiteId = dto.MusicSiteId,
                TariffId = dto.TariffId,
                Price = dto.Price
            };

            _context.musicSiteTarefeModels.Add(musicSiteTariff);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "تعرفه با موفقیت به سایت موزیک اختصاص یافت."
            });
        }


        [HttpGet("get-tariffs/{siteId}")]
        public async Task<IActionResult> GetTariffsForMusicSite(int siteId)
        {
            var tariffs = await _context.musicSiteTarefeModels
                .Where(mst => mst.MusicSiteId == siteId)
                .Select(mst => new
                {
                    mst.TariffId,
                    TariffName = mst.Tariff.Name,
                    mst.Price
                })
                .ToListAsync();

            if (!tariffs.Any())
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "تعرفه‌ای برای این سایت یافت نشد."
                });
            }

            return Ok(tariffs);
        }

    }
}