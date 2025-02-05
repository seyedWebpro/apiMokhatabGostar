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
    public class MusicWebsiteController : ControllerBase
    {
        private readonly apiContext _context;

        public MusicWebsiteController(apiContext context)
        {
            _context = context;
        }

        // اضافه کردن سایت

        // [HttpPost("create")]
        // public async Task<IActionResult> CreateMusicSite([FromBody] MusicSiteDto musicSiteDto)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(new
        //         {
        //             StatusCode = 400,
        //             Message = "اطلاعات سایت نامعتبر است."
        //         });
        //     }

        //     var musicSite = new MusicSiteModel
        //     {
        //         SiteName = musicSiteDto.SiteName,
        //         SiteTopic = musicSiteDto.SiteTopic,
        //         AdminId = musicSiteDto.AdminId,
        //         SiteAddress = musicSiteDto.SiteAddress,
        //     };

        //     _context.musicSiteModels.Add(musicSite);
        //     await _context.SaveChangesAsync();

        //     return Ok(new
        //     {
        //         StatusCode = 200,
        //         Message = "سایت با موفقیت اضافه شد.",
        //         MusicSiteId = musicSite.Id
        //     });
        // }


        // اضافه کردن امکان اپلود فایل

     [HttpPost("create")]
public async Task<IActionResult> CreateMusicSite([FromForm] MusicSiteDto request)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(new
        {
            StatusCode = 400,
            Message = "اطلاعات سایت نامعتبر است."
        });
    }

    string? iconPath = null;

    // بررسی و ذخیره آیکون
    if (request.SiteIcon != null && request.SiteIcon.Length > 0)
    {
        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "icons");
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.SiteIcon.FileName)}";
        var filePath = Path.Combine(uploadPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await request.SiteIcon.CopyToAsync(stream);
        }

        iconPath = $"/icons/{fileName}";
    }

    // ایجاد مدل سایت
    var musicSite = new MusicSiteModel
    {
        SiteName = request.SiteName,
        SiteTopic = request.SiteTopic,
        AdminId = request.AdminId,
        SiteAddress = request.SiteAddress,
        SiteIcon = iconPath
    };

    _context.musicSiteModels.Add(musicSite);
    await _context.SaveChangesAsync();

    return Ok(new
    {
        StatusCode = 200,
        Message = "سایت با موفقیت اضافه شد.",
        MusicSiteId = musicSite.Id,
        IconUrl = musicSite.SiteIcon
    });
}

        // دریافت تمام سایت‌ها
    [HttpGet("all")]
public async Task<IActionResult> GetAllmusicSiteModels()
{
    var sites = await _context.musicSiteModels
        .Include(s => s.MusicSiteTarefeModel)
        .ThenInclude(t => t.Tariff)
        .Select(site => new
        {
            site.Id,
            site.SiteName,
            site.SiteTopic,
            site.AdminId,
            site.SiteAddress,
            IconUrl = site.SiteIcon,
            Tariffs = site.MusicSiteTarefeModel.Select(t => new
            {
                t.TariffId,
                t.Tariff.Name,
                t.Price
            }).ToList()
        })
        .ToListAsync();

    return Ok(new
    {
        StatusCode = 200,
        Sites = sites
    });
}


        // دریافت سایت بر اساس آیدی
       [HttpGet("{id}")]
public async Task<IActionResult> GetMusicSiteById(int id)
{
    var site = await _context.musicSiteModels
        .Include(s => s.MusicSiteTarefeModel)
        .ThenInclude(t => t.Tariff)
        .FirstOrDefaultAsync(s => s.Id == id);

    if (site == null)
    {
        return NotFound(new
        {
            StatusCode = 404,
            Message = "سایت مورد نظر یافت نشد."
        });
    }

    return Ok(new
    {
        StatusCode = 200,
        Site = new
        {
            site.Id,
            site.SiteName,
            site.SiteTopic,
            site.AdminId,
            site.SiteAddress,
            IconUrl = site.SiteIcon,
            Tariffs = site.MusicSiteTarefeModel.Select(t => new
            {
                t.TariffId,
                t.Tariff.Name,
                t.Price
            }).ToList()
        }
    });
}



        // ویرایش سایت
        [HttpPost("update/{id}")]
        public async Task<IActionResult> UpdateMusicSite(int id, [FromBody] UpdateMusicSiteDto updateDto)
        {
            var site = await _context.musicSiteModels.FindAsync(id);
            if (site == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "سایت مورد نظر یافت نشد."
                });
            }

            // بررسی اعتبار فقط در صورت ارسال مقدار
            if (!string.IsNullOrEmpty(updateDto.SiteAddress))
            {
                if (!Uri.IsWellFormedUriString(updateDto.SiteAddress, UriKind.Absolute))
                {
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        Message = "آدرس سایت معتبر نیست."
                    });
                }
                site.SiteAddress = updateDto.SiteAddress; // فقط در صورت معتبر بودن، مقدار به‌روزرسانی شود
            }

            if (!string.IsNullOrEmpty(updateDto.SiteName))
                site.SiteName = updateDto.SiteName;

            if (!string.IsNullOrEmpty(updateDto.SiteTopic))
                site.SiteTopic = updateDto.SiteTopic;

            if (!string.IsNullOrEmpty(updateDto.AdminId))
                site.AdminId = updateDto.AdminId;

            _context.musicSiteModels.Update(site);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "سایت با موفقیت به‌روزرسانی شد."
            });
        }



        // حذف سایت
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteMusicSite(int id)
        {
            var site = await _context.musicSiteModels.FindAsync(id);
            if (site == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "سایت مورد نظر یافت نشد."
                });
            }

            // حذف فایل آیکون از سیستم فایل
            if (!string.IsNullOrEmpty(site.SiteIcon))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", site.SiteIcon.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            // حذف سایت از دیتابیس
            _context.musicSiteModels.Remove(site);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "سایت با موفقیت حذف شد."
            });
        }

    }
}