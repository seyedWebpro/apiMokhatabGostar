using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using allAPIs.SimoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace allAPIs.SimoAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScreenshotController : ControllerBase
    {
        public apiContext _context { get; }

        public ScreenshotController(apiContext context)
        {
            _context = context;
        }

        // 1. آپلود اسکرین‌شات
        // [HttpPost("upload-screenshot")]
        // public async Task<IActionResult> UploadScreenshot([FromForm] UploadScreenshotDto dto)
        // {
        //     var campaign = await _context.campaigns.FindAsync(dto.CampaignId);
        //     if (campaign == null)
        //     {
        //         return NotFound(new { StatusCode = 404, Message = "کمپین مورد نظر یافت نشد." });
        //     }

        //     PagesModel instagramPage = null;
        //     TelegramChannelModel telegramChannel = null;
        //     MusicSiteModel musicSite = null;

        //     if (dto.InstagramId.HasValue)
        //     {
        //         instagramPage = await _context.pages.FindAsync(dto.InstagramId.Value);
        //         if (instagramPage == null)
        //         {
        //             return NotFound(new { StatusCode = 404, Message = "پیج اینستاگرام یافت نشد." });
        //         }
        //     }

        //     if (dto.ChannelId.HasValue)
        //     {
        //         telegramChannel = await _context.telegramChannels.FindAsync(dto.ChannelId.Value);
        //         if (telegramChannel == null)
        //         {
        //             return NotFound(new { StatusCode = 404, Message = "کانال تلگرام یافت نشد." });
        //         }
        //     }

        //     if (dto.MusicSiteId.HasValue)
        //     {
        //         musicSite = await _context.musicSiteModels.FindAsync(dto.MusicSiteId.Value);
        //         if (musicSite == null)
        //         {
        //             return NotFound(new { StatusCode = 404, Message = "سایت موزیک یافت نشد." });
        //         }
        //     }

        //     if (dto.Screenshot == null || dto.Screenshot.Length == 0)
        //     {
        //         return BadRequest(new { StatusCode = 400, Message = "لطفا اسکرین‌شات را انتخاب کنید." });
        //     }

        //     var fileName = $"{Guid.NewGuid()}.png";
        //     var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Screenshots");

        //     if (!Directory.Exists(directoryPath))
        //     {
        //         Directory.CreateDirectory(directoryPath);
        //     }

        //     var filePath = Path.Combine(directoryPath, fileName);

        //     using (var stream = new FileStream(filePath, FileMode.Create))
        //     {
        //         await dto.Screenshot.CopyToAsync(stream);
        //     }

        //     var screenshot = new ScreenshotModel
        //     {
        //         ScreenshotUrl = "/Screenshots/" + fileName,
        //         UploadDate = DateTime.Now,
        //         CampaignId = campaign.Id,
        //         InstagramPageId = instagramPage?.Id,
        //         ChannelId = telegramChannel?.Id,
        //         MusicSiteId = musicSite?.Id
        //     };

        //     _context.screenshotModels.Add(screenshot);
        //     await _context.SaveChangesAsync();

        //     return Ok(new { StatusCode = 200, Message = "اسکرین‌شات با موفقیت آپلود شد.", ScreenshotUrl = "/Screenshots/" + fileName });
        // }

        // اضافه کردن UserID

        [HttpPost("upload-screenshot")]
        public async Task<IActionResult> UploadScreenshot([FromForm] UploadScreenshotDto dto)
        {

            var currentUser = await _context.users.FirstOrDefaultAsync(u => u.id == dto.UserId);
            if (currentUser == null)
            {
                return Unauthorized(new { StatusCode = 401, Message = "کاربر معتبر نیست." });
            }

            var campaign = await _context.campaigns.FirstOrDefaultAsync(c => c.Id == dto.CampaignId && c.UserId == currentUser.id);
            if (campaign == null)
            {
                return NotFound(new { StatusCode = 404, Message = "کمپین مورد نظر یافت نشد یا متعلق به کاربر نیست." });
            }

            PagesModel instagramPage = null;
            TelegramChannelModel telegramChannel = null;
            MusicSiteModel musicSite = null;

            if (dto.InstagramId.HasValue)
            {
                instagramPage = await _context.Pages.FindAsync(dto.InstagramId.Value);
                if (instagramPage == null)
                {
                    return NotFound(new { StatusCode = 404, Message = "پیج اینستاگرام یافت نشد." });
                }
            }

            if (dto.ChannelId.HasValue)
            {
                telegramChannel = await _context.telegramChannels.FindAsync(dto.ChannelId.Value);
                if (telegramChannel == null)
                {
                    return NotFound(new { StatusCode = 404, Message = "کانال تلگرام یافت نشد." });
                }
            }

            if (dto.MusicSiteId.HasValue)
            {
                musicSite = await _context.musicSiteModels.FindAsync(dto.MusicSiteId.Value);
                if (musicSite == null)
                {
                    return NotFound(new { StatusCode = 404, Message = "سایت موزیک یافت نشد." });
                }
            }

            if (dto.Screenshot == null || dto.Screenshot.Length == 0)
            {
                return BadRequest(new { StatusCode = 400, Message = "لطفا اسکرین‌شات را انتخاب کنید." });
            }

            var fileName = $"{Guid.NewGuid()}.png";
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Screenshots");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var filePath = Path.Combine(directoryPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.Screenshot.CopyToAsync(stream);
            }

            var screenshot = new ScreenshotModel
            {
                ScreenshotUrl = "/Screenshots/" + fileName,
                UploadDate = DateTime.Now,
                CampaignId = campaign.Id,
                InstagramPageId = instagramPage?.Id,
                ChannelId = telegramChannel?.Id,
                MusicSiteId = musicSite?.Id,
                UserId = currentUser.id // ثبت شناسه کاربر
            };

            _context.screenshotModels.Add(screenshot);
            await _context.SaveChangesAsync();

            return Ok(new { StatusCode = 200, Message = "اسکرین‌شات با موفقیت آپلود شد.", ScreenshotUrl = "/Screenshots/" + fileName });
        }

        // 6. دریافت اسکرین‌شات‌های خاص برای کاربر
        [HttpGet("get-user-screenshot")]
        public async Task<IActionResult> GetUserScreenshot(long userId, int campaignId, string type, int? relatedId)
        {
            // ابتدا، بررسی می‌کنیم که آیا کاربر معتبر است یا خیر
            var currentUser = await _context.users.FirstOrDefaultAsync(u => u.id == userId);
            if (currentUser == null)
            {
                return Unauthorized(new { StatusCode = 401, Message = "کاربر معتبر نیست." });
            }

            // بررسی کمپین و اطمینان از تعلق آن به کاربر
            var campaign = await _context.campaigns.FirstOrDefaultAsync(c => c.Id == campaignId && c.UserId == currentUser.id);
            if (campaign == null)
            {
                return NotFound(new { StatusCode = 404, Message = "کمپین مورد نظر یافت نشد یا متعلق به کاربر نیست." });
            }

            // متغیر برای اسکرین‌شات نهایی
            ScreenshotModel screenshot = null;

            // بر اساس نوع درخواست شده، اسکرین‌شات را جستجو می‌کنیم
            switch (type.ToLower())
            {
                case "instagram":
                    if (relatedId.HasValue)
                    {
                        screenshot = await _context.screenshotModels
                            .FirstOrDefaultAsync(s => s.CampaignId == campaignId && s.UserId == userId && s.InstagramPageId == relatedId.Value);
                    }
                    break;

                case "telegram":
                    if (relatedId.HasValue)
                    {
                        screenshot = await _context.screenshotModels
                            .FirstOrDefaultAsync(s => s.CampaignId == campaignId && s.UserId == userId && s.ChannelId == relatedId.Value);
                    }
                    break;

                case "musicsite":
                    if (relatedId.HasValue)
                    {
                        screenshot = await _context.screenshotModels
                            .FirstOrDefaultAsync(s => s.CampaignId == campaignId && s.UserId == userId && s.MusicSiteId == relatedId.Value);
                    }
                    break;

                default:
                    return BadRequest(new { StatusCode = 400, Message = "نوع مورد نظر صحیح نیست." });
            }

            // اگر اسکرین‌شات پیدا نشد
            if (screenshot == null)
            {
                return NotFound(new { StatusCode = 404, Message = "اسکرین‌شات مورد نظر یافت نشد." });
            }

            // بازگشت اسکرین‌شات
            return Ok(new { StatusCode = 200, Screenshot = screenshot });
        }


        // 2. دریافت همه اسکرین‌شات‌ها
        [HttpGet("get-all-screenshots")]
        public async Task<IActionResult> GetAllScreenshots()
        {
            var screenshots = await _context.screenshotModels.ToListAsync();
            if (screenshots == null || screenshots.Count == 0)
            {
                return NotFound(new { StatusCode = 404, Message = "هیچ اسکرین‌شاتی یافت نشد." });
            }

            return Ok(new { StatusCode = 200, Screenshots = screenshots });
        }

        // 3. دریافت اسکرین‌شات‌های اینستاگرام
        [HttpGet("get-instagram-screenshots")]
        public async Task<IActionResult> GetInstagramScreenshots()
        {
            var screenshots = await _context.screenshotModels
                                    .Where(s => s.InstagramPageId != null)
                                    .ToListAsync();
            if (screenshots == null || screenshots.Count == 0)
            {
                return NotFound(new { StatusCode = 404, Message = "هیچ اسکرین‌شات اینستاگرامی یافت نشد." });
            }

            return Ok(new { StatusCode = 200, Screenshots = screenshots });
        }

        // 4. دریافت اسکرین‌شات‌های تلگرام
        [HttpGet("get-telegram-screenshots")]
        public async Task<IActionResult> GetTelegramScreenshots()
        {
            var screenshots = await _context.screenshotModels
                                    .Where(s => s.ChannelId != null)
                                    .ToListAsync();
            if (screenshots == null || screenshots.Count == 0)
            {
                return NotFound(new { StatusCode = 404, Message = "هیچ اسکرین‌شات تلگرامی یافت نشد." });
            }

            return Ok(new { StatusCode = 200, Screenshots = screenshots });
        }

        // 5. دریافت اسکرین‌شات‌های سایت موزیک
        [HttpGet("get-music-site-screenshots")]
        public async Task<IActionResult> GetMusicSiteScreenshots()
        {
            var screenshots = await _context.screenshotModels
                                    .Where(s => s.MusicSiteId != null)
                                    .ToListAsync();
            if (screenshots == null || screenshots.Count == 0)
            {
                return NotFound(new { StatusCode = 404, Message = "هیچ اسکرین‌شات سایت موزیکی یافت نشد." });
            }

            return Ok(new { StatusCode = 200, Screenshots = screenshots });
        }

        // 7. حذف همه اسکرین‌شات‌ها
        [HttpPost("delete-all-screenshots")]
        public async Task<IActionResult> DeleteAllScreenshots()
        {
            var screenshots = await _context.screenshotModels.ToListAsync();
            if (screenshots == null || screenshots.Count == 0)
            {
                return NotFound(new { StatusCode = 404, Message = "هیچ اسکرین‌شاتی برای حذف وجود ندارد." });
            }

            _context.screenshotModels.RemoveRange(screenshots);
            await _context.SaveChangesAsync();

            return Ok(new { StatusCode = 200, Message = "همه اسکرین‌شات‌ها با موفقیت حذف شدند." });
        }

        [HttpGet("download-screenshot/{id}")]
        public async Task<IActionResult> DownloadScreenshot(int id)
        {
            var screenshot = await _context.screenshotModels.FirstOrDefaultAsync(s => s.Id == id);
            if (screenshot == null)
            {
                return NotFound(new { StatusCode = 404, Message = "اسکرین‌شات مورد نظر یافت نشد." });
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", screenshot.ScreenshotUrl.TrimStart('/'));
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound(new { StatusCode = 404, Message = "فایل مورد نظر یافت نشد." });
            }

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            var fileName = Path.GetFileName(filePath);
            var contentType = "application/octet-stream"; // یا نوع محتوا بر اساس نوع فایل

            return File(fileBytes, contentType, fileName);
        }

    }
}
