using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using allAPIs.SimoAPI.Models.ReporterView;
using allAPIs.SimoAPI.Models.InstagramView;
using allAPIs.SimoAPI.Models.TelegramView;

namespace allAPIs.SimoAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignReporterController : ControllerBase
    {
        private readonly apiContext _context;
        private readonly ILogger<CampaignReporterController> _logger;

        public CampaignReporterController(apiContext context, ILogger<CampaignReporterController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("edit/music-site/{campaignId}/{musicSiteId}")]
        public async Task<IActionResult> EditCampaignMusicSite(int campaignId, int musicSiteId, [FromBody] UpdateCampaignMusicSiteDto updatedSiteInfo)
        {
            try
            {
                var campaignMusicSite = await _context.campaignMusicSites
                    .Include(cms => cms.Campaign) // دریافت اطلاعات کمپین مرتبط
                    .ThenInclude(c => c.CampaignPages) // دریافت پیج‌های مرتبط با کمپین
                    .Where(cms => cms.CampaignId == campaignId && cms.MusicSiteId == musicSiteId)
                    .FirstOrDefaultAsync();

                if (campaignMusicSite == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "سایت موزیک مربوط به این کمپین پیدا نشد."
                    });
                }

                // به‌روزرسانی فیلدهای سایت موزیک
                if (!string.IsNullOrEmpty(updatedSiteInfo.SiteName) && updatedSiteInfo.SiteName != "null")
                    campaignMusicSite.SiteName = updatedSiteInfo.SiteName;

                if (!string.IsNullOrEmpty(updatedSiteInfo.SiteTopic) && updatedSiteInfo.SiteTopic != "null")
                    campaignMusicSite.SiteTopic = updatedSiteInfo.SiteTopic;

                if (!string.IsNullOrEmpty(updatedSiteInfo.AdminId) && updatedSiteInfo.AdminId != "null")
                    campaignMusicSite.AdminId = updatedSiteInfo.AdminId;

                if (!string.IsNullOrEmpty(updatedSiteInfo.SiteAddress) && updatedSiteInfo.SiteAddress != "null")
                    campaignMusicSite.SiteAddress = updatedSiteInfo.SiteAddress;

                if (!string.IsNullOrEmpty(updatedSiteInfo.SiteIcon) && updatedSiteInfo.SiteIcon != "null")
                    campaignMusicSite.SiteIcon = updatedSiteInfo.SiteIcon;

                if (updatedSiteInfo.PublishDate.HasValue)
                    campaignMusicSite.PublishDate = updatedSiteInfo.PublishDate;


                if (updatedSiteInfo.Price.HasValue)
                    campaignMusicSite.Price = updatedSiteInfo.Price.Value;

                // تغییر تعرفه در صورت ارسال
                if (updatedSiteInfo.TariffId.HasValue)
                {
                    var newTariff = await _context.tarefeModels.FirstOrDefaultAsync(t => t.Id == updatedSiteInfo.TariffId.Value);
                    if (newTariff == null)
                    {
                        return NotFound(new
                        {
                            StatusCode = 404,
                            Message = "تعرفه مورد نظر یافت نشد."
                        });
                    }

                    campaignMusicSite.TariffId = newTariff.Id;
                }

                // ذخیره تغییرات
                _context.campaignMusicSites.Update(campaignMusicSite);
                await _context.SaveChangesAsync();

                // دریافت لیست SiteId های مرتبط با کمپین
                var musicSiteID = campaignMusicSite.Campaign?.CampaignMusicSites.Select(p => p.MusicSiteId).ToList() ?? new List<int>();

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "اطلاعات سایت موزیک با موفقیت به روز رسانی شد.",
                    CampaignId = campaignMusicSite.CampaignId,
                    SiteId = musicSiteID,
                    PublishDate = campaignMusicSite.PublishDate
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = $"خطا در به روز رسانی اطلاعات: {ex.Message}"
                });
            }
        }

[HttpGet("get/music-site/{campaignId}/{musicSiteId}")]
public async Task<IActionResult> GetCampaignMusicSite(int campaignId, int musicSiteId)
{
    try
    {
        var campaign = await _context.campaigns
            .Include(c => c.CampaignMusicSites)
            .ThenInclude(cms => cms.Tariff)
            .Where(c => c.Id == campaignId)
            .FirstOrDefaultAsync();

        if (campaign == null || campaign.CampaignMusicSites == null || !campaign.CampaignMusicSites.Any())
        {
            return NotFound(new
            {
                StatusCode = 404,
                Message = "هیچ سایت موزیکی برای این کمپین پیدا نشد."
            });
        }

        var musicSite = campaign.CampaignMusicSites
            .FirstOrDefault(cms => cms.MusicSiteId == musicSiteId);

        if (musicSite == null)
        {
            return NotFound(new
            {
                StatusCode = 404,
                Message = "سایت موزیک مورد نظر پیدا نشد."
            });
        }

        var campaignMusicSite = new
        {
            musicSite.SiteName,
            musicSite.SiteTopic,
            musicSite.AdminId,
            musicSite.SiteAddress,
            musicSite.SiteIcon,
            musicSite.Price,
            musicSite.PublishDate, // اضافه کردن تاریخ انتشار
            Tariff = new
            {
                musicSite.TariffId,
                musicSite.Tariff?.Name
            },
            MusicSiteId = musicSite.MusicSiteId, // اضافه کردن MusicSiteId
            UserId = campaign.UserId // اضافه کردن UserId
        };

        return Ok(new
        {
            StatusCode = 200,
            Message = "سایت موزیک با موفقیت دریافت شد.",
            CampaignId = campaign.Id,
            UserId = campaign.UserId,
            Data = campaignMusicSite
        });
    }
    catch (Exception ex)
    {
        return BadRequest(new
        {
            StatusCode = 400,
            Message = $"خطا در دریافت اطلاعات: {ex.Message}"
        });
    }
}


        [HttpGet("get/music-sites/{campaignId}")]
        public async Task<IActionResult> GetCampaignMusicSites(int campaignId)
        {
            try
            {
                var campaign = await _context.campaigns
                    .Include(c => c.CampaignMusicSites)
                    .ThenInclude(cms => cms.Tariff)
                    .Where(c => c.Id == campaignId)
                    .FirstOrDefaultAsync();

                if (campaign == null || campaign.CampaignMusicSites == null || !campaign.CampaignMusicSites.Any())
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "هیچ سایت موزیکی برای این کمپین پیدا نشد."
                    });
                }

                var campaignMusicSites = campaign.CampaignMusicSites.Select(cms => new
                {
                    cms.SiteName,
                    cms.SiteTopic,
                    cms.AdminId,
                    cms.SiteAddress,
                    cms.SiteIcon,
                    cms.Price,
                    cms.PublishDate, // اضافه کردن تاریخ انتشار
                    Tariff = new
                    {
                        cms.TariffId,
                        cms.Tariff?.Name
                    },
                    MusicSiteId = cms.MusicSiteId, // اضافه کردن MusicSiteId
                    UserId = campaign.UserId // اضافه کردن UserId
                }).ToList();

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "لیست سایت‌های موزیک با موفقیت دریافت شد.",
                    CampaignId = campaign.Id,
                    UserId = campaign.UserId,
                    Data = campaignMusicSites
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = $"خطا در دریافت اطلاعات: {ex.Message}"
                });
            }
        }


        [HttpPost("edit/telegram/{campaignId}/{channelId}")]
        public async Task<IActionResult> EditCampaignTelegramChannel(int campaignId, int channelId, [FromBody] UpdateCampaignTelegramChannelDto updatedChannelInfo)
        {
            try
            {
                var campaignChannel = await _context.campaignChannels
                    .Where(cc => cc.CampaignId == campaignId && cc.ChannelId == channelId)
                    .FirstOrDefaultAsync();

                if (campaignChannel == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "کانال تلگرام مربوط به این کمپین پیدا نشد."
                    });
                }

                // به‌روزرسانی فیلدهای کانال تلگرام در صورت ارسال مقدار جدید
                if (!string.IsNullOrWhiteSpace(updatedChannelInfo.Name))
                    campaignChannel.Name = updatedChannelInfo.Name;

                if (!string.IsNullOrWhiteSpace(updatedChannelInfo.Topic))
                    campaignChannel.Topic = updatedChannelInfo.Topic;

                if (!string.IsNullOrWhiteSpace(updatedChannelInfo.ManagerId))
                    campaignChannel.ManagerId = updatedChannelInfo.ManagerId;

                if (!string.IsNullOrWhiteSpace(updatedChannelInfo.PhotoPath))
                    campaignChannel.PhotoPath = updatedChannelInfo.PhotoPath;

                if (!string.IsNullOrWhiteSpace(updatedChannelInfo.TelID))
                    campaignChannel.TelID = updatedChannelInfo.TelID;

                if (updatedChannelInfo.SubscribersCount.HasValue)
                    campaignChannel.SubscribersCount = updatedChannelInfo.SubscribersCount.Value;

                if (updatedChannelInfo.Price.HasValue)
                    campaignChannel.Price = updatedChannelInfo.Price.Value;

                // تغییر تعرفه در صورت ارسال مقدار جدید
                if (updatedChannelInfo.TariffId.HasValue)
                {
                    var newTariff = await _context.tarefeModels.FirstOrDefaultAsync(t => t.Id == updatedChannelInfo.TariffId.Value);
                    if (newTariff == null)
                    {
                        return NotFound(new
                        {
                            StatusCode = 404,
                            Message = "تعرفه مورد نظر یافت نشد."
                        });
                    }

                    campaignChannel.TariffId = newTariff.Id;
                }

                // به‌روزرسانی تاریخ انتشار
                if (updatedChannelInfo.PublishDate.HasValue)
                    campaignChannel.PublishDate = updatedChannelInfo.PublishDate.Value;

                // ذخیره تغییرات
                _context.campaignChannels.Update(campaignChannel);
                await _context.SaveChangesAsync();

                // بازگرداندن اطلاعات جدید کانال پس از ویرایش
                var updatedChannelDto = new
                {
                    campaignChannel.Name,
                    campaignChannel.Topic,
                    campaignChannel.ManagerId,
                    campaignChannel.PhotoPath,
                    campaignChannel.TelID,
                    campaignChannel.SubscribersCount,
                    campaignChannel.Price,
                    campaignChannel.PublishDate, // اضافه شدن تاریخ انتشار
                    Tariff = new
                    {
                        campaignChannel.TariffId,
                        campaignChannel.Tariff?.Name
                    }
                };

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "اطلاعات کانال تلگرام با موفقیت به روز رسانی شد.",
                    Data = updatedChannelDto
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = $"خطا در به روز رسانی اطلاعات: {ex.Message}"
                });
            }
        }



        [HttpGet("get/telegram-channels/{campaignId}/{channelId}")]
        public async Task<IActionResult> GetCampaignTelegramChannel(int campaignId, int channelId)
        {
            try
            {
                var campaignChannel = await _context.campaignChannels
                    .Where(cc => cc.CampaignId == campaignId && cc.ChannelId == channelId) // فیلتر بر اساس کمپین و کانال
                    .Include(cc => cc.Tariff) // بارگذاری اطلاعات تعرفه مرتبط
                    .Select(cc => new
                    {
                        cc.Name,
                        cc.Topic,
                        cc.ManagerId,
                        cc.PhotoPath,
                        cc.Price,
                        cc.TelID,
                        cc.SubscribersCount,
                        cc.PublishDate, // اضافه شدن تاریخ انتشار
                        Tariff = new
                        {
                            cc.TariffId,
                            cc.Tariff.Name
                        },
                        ChannelId = cc.ChannelId, // اضافه کردن ChannelId
                        UserId = cc.Campaign.UserId // اضافه کردن UserId
                    })
                    .FirstOrDefaultAsync(); // استفاده از FirstOrDefault برای دریافت یک رکورد یا null

                if (campaignChannel == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "کانال تلگرامی برای این کمپین پیدا نشد."
                    });
                }

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "اطلاعات کانال تلگرام با موفقیت دریافت شد.",
                    Data = campaignChannel
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = $"خطا در دریافت اطلاعات: {ex.Message}"
                });
            }
        }


        [HttpGet("get/telegram-channels/{campaignId}")]
        public async Task<IActionResult> GetCampaignTelegramChannels(int campaignId)
        {
            try
            {
                var campaignChannels = await _context.campaignChannels
                    .Where(cc => cc.CampaignId == campaignId)
                    .Include(cc => cc.Tariff) // بارگذاری اطلاعات تعرفه مرتبط
                    .Select(cc => new
                    {
                        cc.Name,
                        cc.Topic,
                        cc.ManagerId,
                        cc.PhotoPath,
                        cc.Price,
                        cc.TelID,
                        cc.SubscribersCount,
                        cc.PublishDate, // اضافه شدن تاریخ انتشار
                        Tariff = new
                        {
                            cc.TariffId,
                            cc.Tariff.Name
                        },
                        ChannelId = cc.ChannelId, // اضافه کردن ChannelId
                        UserId = cc.Campaign.UserId // اضافه کردن UserId
                    })
                    .ToListAsync();

                if (!campaignChannels.Any())
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "هیچ کانال تلگرامی برای این کمپین پیدا نشد."
                    });
                }

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "لیست کانال‌های تلگرام با موفقیت دریافت شد.",
                    Data = campaignChannels
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = $"خطا در دریافت اطلاعات: {ex.Message}"
                });
            }
        }


        [HttpPost("edit/instagram/{campaignId}/{pageId}")]
        public async Task<IActionResult> EditCampaignInstagramPage(int campaignId, int pageId, [FromBody] UpdateCampaignInstagramPageDto updatedPageInfo)
        {
            try
            {
                var campaignPage = await _context.CampaignPages
                    .Include(cp => cp.Tariff) // بارگذاری اطلاعات تعرفه مرتبط
                    .FirstOrDefaultAsync(cp => cp.CampaignId == campaignId && cp.PageId == pageId);

                if (campaignPage == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "پیج اینستاگرام مربوط به این کمپین پیدا نشد."
                    });
                }

                // به روز رسانی اطلاعات عمومی صفحه
                if (!string.IsNullOrEmpty(updatedPageInfo.ShowName)) campaignPage.ShowName = updatedPageInfo.ShowName;
                if (!string.IsNullOrEmpty(updatedPageInfo.PersianName)) campaignPage.PersianName = updatedPageInfo.PersianName;
                if (!string.IsNullOrEmpty(updatedPageInfo.ImgUrl)) campaignPage.ImgUrl = updatedPageInfo.ImgUrl;
                if (!string.IsNullOrEmpty(updatedPageInfo.Description)) campaignPage.Description = updatedPageInfo.Description;
                if (updatedPageInfo.PostComments.HasValue) campaignPage.postComments = updatedPageInfo.PostComments.Value;
                if (updatedPageInfo.PostViews.HasValue) campaignPage.PostViews = updatedPageInfo.PostViews.Value;
                if (updatedPageInfo.PostLikes.HasValue) campaignPage.PostLikes = updatedPageInfo.PostLikes.Value;
                if (!string.IsNullOrEmpty(updatedPageInfo.PostLink)) campaignPage.PostLink = updatedPageInfo.PostLink;
                if (updatedPageInfo.Price.HasValue) campaignPage.Price = updatedPageInfo.Price.Value;
                if (updatedPageInfo.PublishDate.HasValue) campaignPage.PublishDate = updatedPageInfo.PublishDate.Value; // اضافه شدن تاریخ انتشار

                // تغییر تعرفه در صورت ارسال مقدار جدید
                if (updatedPageInfo.TariffId.HasValue)
                {
                    var newTariff = await _context.tarefeModels.FirstOrDefaultAsync(t => t.Id == updatedPageInfo.TariffId.Value);
                    if (newTariff == null)
                    {
                        return NotFound(new
                        {
                            StatusCode = 404,
                            Message = "تعرفه مورد نظر یافت نشد."
                        });
                    }

                    campaignPage.TariffId = newTariff.Id;
                }

                // ذخیره تغییرات
                _context.CampaignPages.Update(campaignPage);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "اطلاعات پیج اینستاگرام با موفقیت به‌روزرسانی شد."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = $"خطا در به‌روزرسانی اطلاعات: {ex.Message}"
                });
            }
        }

        [HttpGet("get/instagram-pages/{campaignId}/{pageId}")]
        public async Task<IActionResult> GetCampaignInstagramPage(int campaignId, int pageId)
        {
            try
            {
                var campaignPage = await _context.CampaignPages
                    .Where(cp => cp.CampaignId == campaignId && cp.PageId == pageId)
                    .Include(cp => cp.Tariff) // بارگذاری اطلاعات تعرفه مرتبط
                    .Select(cp => new
                    {
                        cp.ShowName,
                        cp.PersianName,
                        cp.ImgUrl,
                        cp.Description,
                        cp.Price,
                        cp.postComments,
                        cp.PostViews,
                        cp.PostLikes,
                        cp.PostLink,
                        cp.PublishDate, // اضافه شدن تاریخ انتشار
                        Tariff = new
                        {
                            cp.TariffId,
                            cp.Tariff.Name
                        },
                        PageId = cp.PageId, // اضافه کردن PageId
                        UserId = cp.Campaign.UserId // اضافه کردن UserId
                    })
                    .FirstOrDefaultAsync(); // با استفاده از FirstOrDefault یک نتیجه واحد یا null برمی‌گردانیم

                if (campaignPage == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "پیج اینستاگرامی برای این کمپین پیدا نشد."
                    });
                }

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "اطلاعات پیج اینستاگرام با موفقیت دریافت شد.",
                    Data = campaignPage
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = $"خطا در دریافت اطلاعات: {ex.Message}"
                });
            }
        }

        [HttpGet("get/instagram-pages/{campaignId}")]
        public async Task<IActionResult> GetCampaignInstagramPages(int campaignId)
        {
            try
            {
                var campaignPages = await _context.CampaignPages
                    .Where(cp => cp.CampaignId == campaignId)
                    .Include(cp => cp.Tariff) // بارگذاری اطلاعات تعرفه مرتبط
                    .Select(cp => new
                    {
                        cp.ShowName,
                        cp.PersianName,
                        cp.ImgUrl,
                        cp.Description,
                        cp.Price,
                        cp.postComments,
                        cp.PostViews,
                        cp.PostLikes,
                        cp.PostLink,
                        cp.PublishDate, // اضافه شدن تاریخ انتشار
                        Tariff = new
                        {
                            cp.TariffId,
                            cp.Tariff.Name
                        },
                        PageId = cp.PageId, // اضافه کردن PageId
                        UserId = cp.Campaign.UserId // اضافه کردن UserId
                    })
                    .ToListAsync();

                if (!campaignPages.Any())
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "هیچ پیج اینستاگرامی برای این کمپین پیدا نشد."
                    });
                }

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "لیست پیج‌های اینستاگرام با موفقیت دریافت شد.",
                    Data = campaignPages
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = $"خطا در دریافت اطلاعات: {ex.Message}"
                });
            }
        }

    }
}
