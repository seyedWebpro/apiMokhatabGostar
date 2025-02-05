using allAPIs.SimoAPI.Models;
using allAPIs.SimoAPI.Models.InstagramView;
using allAPIs.SimoAPI.Models.MusicSiteView;
using allAPIs.SimoAPI.Models.TelegramView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace allAPIs.SimoAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignController : ControllerBase
    {
        public CampaignController(apiContext context, IHttpContextAccessor httpContextAccessor)
        {
            Con = context;
        }
        public apiContext Con { get; }

        [HttpGet("get-all-campaigns")]
        public async Task<IActionResult> GetAllCampaigns()
        {
            try
            {
                // دریافت تمامی کمپین‌ها از همه پلتفرم‌ها
                var allCampaigns = await Con.campaigns
                    .Include(c => c.CampaignPages)
                        .ThenInclude(cp => cp.Page) // شامل اطلاعات پیج‌ها
                    .Include(c => c.CampaignPages)
                        .ThenInclude(cp => cp.Tariff) // شامل اطلاعات تعرفه‌ها
                    .Include(c => c.CampaignChannels)
                        .ThenInclude(cc => cc.Channel) // شامل اطلاعات کانال‌های تلگرام
                    .Include(c => c.CampaignChannels)
                        .ThenInclude(cc => cc.Tariff) // شامل اطلاعات تعرفه‌های تلگرام
                    .Include(c => c.CampaignMusicSites)
                        .ThenInclude(cms => cms.MusicSite) // شامل اطلاعات سایت‌های موزیک
                    .Include(c => c.CampaignMusicSites)
                        .ThenInclude(cms => cms.Tariff) // شامل اطلاعات تعرفه‌های موزیک
                    .ToListAsync();

                if (allCampaigns == null || !allCampaigns.Any())
                {
                    return NotFound("هیچ کمپینی یافت نشد.");
                }

                return Ok(new
                {
                    StatusCode = 200,
                    Campaigns = allCampaigns.Select(campaign => new
                    {
                        campaign.Id,
                        campaign.Name,
                        campaign.StartDate,
                        campaign.UserId,
                        campaign.Platform,
                        CampaignPages = campaign.CampaignPages.Select(cp => new
                        {
                            PageId = cp.Page.Id,
                            PageName = cp.Page.ShowName,
                            PageDescription = cp.Page.Description,
                            PageImage = cp.Page.ImgUrl,
                            TariffId = cp.TariffId,
                            TariffName = cp.Tariff?.Name,
                        }).ToList(),
                        CampaignChannels = campaign.CampaignChannels.Select(cc => new
                        {
                            ChannelId = cc.Channel.Id,
                            ChannelName = cc.Channel.Name,
                            TariffId = cc.TariffId,
                            TariffName = cc.Tariff?.Name
                        }).ToList(),
                        CampaignMusicSites = campaign.CampaignMusicSites.Select(cms => new
                        {
                            MusicSiteId = cms.MusicSite.Id,
                            MusicSiteName = cms.MusicSite.SiteName,
                            MusicSiteTopic = cms.MusicSite.SiteTopic,
                            MusicSiteAddress = cms.MusicSite.SiteAddress,
                            MusicSiteIcon = cms.MusicSite.SiteIcon,
                            TariffId = cms.TariffId,
                            TariffName = cms.Tariff?.Name
                        }).ToList()
                    })
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("get-campaign/{id}")]
        public async Task<IActionResult> GetCampaignById(int id)
        {
            try
            {
                // دریافت کمپین بر اساس شناسه
                var campaign = await Con.campaigns
                    .Include(c => c.CampaignPages)
                        .ThenInclude(cp => cp.Page) // شامل اطلاعات پیج‌ها
                    .Include(c => c.CampaignPages)
                        .ThenInclude(cp => cp.Tariff) // شامل اطلاعات تعرفه‌ها
                    .Include(c => c.CampaignChannels)
                        .ThenInclude(cc => cc.Channel) // شامل اطلاعات کانال‌های تلگرام
                    .Include(c => c.CampaignChannels)
                        .ThenInclude(cc => cc.Tariff) // شامل اطلاعات تعرفه‌های تلگرام
                    .Include(c => c.CampaignMusicSites)
                        .ThenInclude(cms => cms.MusicSite) // شامل اطلاعات سایت‌های موزیک
                    .Include(c => c.CampaignMusicSites)
                        .ThenInclude(cms => cms.Tariff) // شامل اطلاعات تعرفه‌های موزیک
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (campaign == null)
                {
                    return NotFound("کمپینی با این شناسه یافت نشد.");
                }

                return Ok(new
                {
                    StatusCode = 200,
                    Campaign = new
                    {
                        campaign.Id,
                        campaign.Name,
                        campaign.StartDate,
                        campaign.UserId,
                        campaign.Platform,
                        CampaignPages = campaign.CampaignPages.Select(cp => new
                        {
                            PageId = cp.Page.Id,
                            PageName = cp.Page.ShowName,
                            PageDescription = cp.Page.Description,
                            PageImage = cp.Page.ImgUrl,
                            TariffId = cp.TariffId,
                            TariffName = cp.Tariff?.Name,
                        }).ToList(),
                        CampaignChannels = campaign.CampaignChannels.Select(cc => new
                        {
                            ChannelId = cc.Channel.Id,
                            ChannelName = cc.Channel.Name,
                            TariffId = cc.TariffId,
                            TariffName = cc.Tariff?.Name
                        }).ToList(),
                        CampaignMusicSites = campaign.CampaignMusicSites.Select(cms => new
                        {
                            MusicSiteId = cms.MusicSite.Id,
                            MusicSiteName = cms.MusicSite.SiteName,
                            MusicSiteTopic = cms.MusicSite.SiteTopic,
                            MusicSiteAddress = cms.MusicSite.SiteAddress,
                            MusicSiteIcon = cms.MusicSite.SiteIcon,
                            TariffId = cms.TariffId,
                            TariffName = cms.Tariff?.Name
                        }).ToList()
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpPost("create-campaign-with-pages-and-tariffs")]
        public async Task<IActionResult> CreateCampaignWithPagesAndTariffs([FromBody] CreateCampaignWithPagesAndTariffsDto dto)
        {
            try
            {
                // 1. بررسی وجود کاربر
                var user = await Con.users.FindAsync(dto.UserId);
                if (user == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "کاربر مورد نظر یافت نشد."
                    });
                }

                // 2. ایجاد کمپین جدید
                var campaign = new CampaignModel
                {
                    Name = dto.Name,
                    UserId = dto.UserId,
                    StartDate = dto.StartDate,
                    Platform = "Instagram"  // پلتفرم اینستاگرام
                };

                Con.campaigns.Add(campaign);
                await Con.SaveChangesAsync();

                // 3. بررسی و افزودن پیج‌ها و تعرفه‌ها به کمپین
                if (dto.PageTariffs == null || !dto.PageTariffs.Any())
                {
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        Message = "لیست پیج‌ها و تعرفه‌ها نمی‌تواند خالی باشد."
                    });
                }

                var pageIds = dto.PageTariffs.Select(pt => pt.PageId).Distinct().ToList();
                var tariffIds = dto.PageTariffs.Select(pt => pt.TariffId).Distinct().ToList();

                // لود کردن پیج‌ها و تعرفه‌ها به صورت بهینه
                var pages = await Con.Pages.Where(p => pageIds.Contains(p.Id)).ToListAsync();
                var tariffs = await Con.PageTariffModels
                    .Where(pt => tariffIds.Contains(pt.TariffId) && pageIds.Contains(pt.PageId))
                    .ToListAsync();

                foreach (var pageTariff in dto.PageTariffs)
                {
                    var page = pages.FirstOrDefault(p => p.Id == pageTariff.PageId);
                    if (page == null)
                    {
                        return NotFound(new
                        {
                            StatusCode = 404,
                            Message = $"پیجی با شناسه {pageTariff.PageId} یافت نشد."
                        });
                    }

                    var tariff = tariffs.FirstOrDefault(pt => pt.PageId == pageTariff.PageId && pt.TariffId == pageTariff.TariffId);
                    if (tariff == null)
                    {
                        return NotFound(new
                        {
                            StatusCode = 404,
                            Message = $"تعرفه‌ای با شناسه {pageTariff.TariffId} برای پیج با شناسه {pageTariff.PageId} یافت نشد."
                        });
                    }

                    var campaignPage = new CampaignPage
                    {
                        CampaignId = campaign.Id,
                        PageId = pageTariff.PageId,
                        TariffId = pageTariff.TariffId
                    };
                    Con.CampaignPages.Add(campaignPage);
                }

                // 4. ذخیره تغییرات
                await Con.SaveChangesAsync();

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "کمپین با موفقیت و پیج‌ها و تعرفه‌های انتخابی ایجاد شد."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = $"خطای داخلی سرور: {ex.Message}"
                });
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetPageCampaigns()
        {
            try
            {
                var pageCampaigns = await Con.campaigns
                    .Where(c => c.Platform == "Instagram")
                    .Include(c => c.CampaignPages)
                    .ThenInclude(cp => cp.Page)
                    .Include(c => c.CampaignPages)
                    .ThenInclude(cp => cp.Tariff) // اضافه کردن اطلاعات تعرفه
                    .ToListAsync();

                if (pageCampaigns == null || !pageCampaigns.Any())
                {
                    return NotFound("کمپین‌های مرتبط با پیج‌ها یافت نشد.");
                }

                return Ok(new
                {
                    StatusCode = 200,
                    Campaigns = pageCampaigns.Select(campaign => new
                    {
                        campaign.Id,
                        campaign.Name,
                        campaign.StartDate,
                        campaign.UserId,
                        campaign.Platform,
                        CampaignPages = campaign.CampaignPages.Select(cp => new
                        {
                            PageId = cp.Page.Id,
                            PageName = cp.Page.ShowName,
                            PageDescription = cp.Page.Description,
                            PageImage = cp.Page.ImgUrl,
                            TariffId = cp.TariffId, // اضافه کردن TariffId
                            TariffName = cp.Tariff.Name, // اضافه کردن نام تعرفه
                        }).ToList()
                    })
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error : {ex.Message}");
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetPageCampaignsByUserId(long userId)
        {
            try
            {
                // بررسی وجود کاربر
                var userExist = await Con.users.AnyAsync(u => u.id == userId);
                if (!userExist)
                {
                    return NotFound("کاربری با این شناسه یافت نشد.");
                }

                // دریافت کمپین‌های کاربر با اطلاعات پیج‌ها و تعرفه‌ها
                var pageCampaigns = await Con.campaigns
                    .Where(c => c.UserId == userId && c.Platform == "Instagram")
                    .Include(c => c.CampaignPages)
                        .ThenInclude(cp => cp.Page) // شامل اطلاعات پیج
                    .Include(c => c.CampaignPages)
                        .ThenInclude(cp => cp.Tariff) // شامل اطلاعات تعرفه
                    .ToListAsync();

                // بررسی وجود کمپین‌ها
                if (pageCampaigns == null || !pageCampaigns.Any())
                {
                    return NotFound("کمپین مرتبط با پیج‌ها برای این کاربر یافت نشد.");
                }

                // ساخت خروجی
                return Ok(new
                {
                    StatusCode = 200,
                    Campaigns = pageCampaigns.Select(campaign => new
                    {
                        campaign.Id,
                        campaign.Name,
                        campaign.StartDate,
                        campaign.UserId,
                        campaign.Platform,
                        CampaignPages = campaign.CampaignPages.Select(cp => new
                        {
                            PageId = cp.Page.Id,
                            PageName = cp.Page.ShowName,
                            PageDescription = cp.Page.Description,
                            PageImage = cp.Page.ImgUrl,
                            TariffId = cp.TariffId, // شناسه تعرفه
                            TariffName = cp.Tariff?.Name, // نام تعرفه
                        }).ToList()
                    })
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error : {ex.Message}");
            }
        }

        [HttpPost("create-campaign-with-music-sites-and-tariffs")]
        public async Task<IActionResult> CreateCampaignWithMusicSitesAndTariffs([FromBody] CreateCampaignWithMusicSitesAndTariffsDto dto)
        {
            // 1. بررسی وجود کاربر
            var user = await Con.users.FindAsync(dto.UserId);
            if (user == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "کاربر مورد نظر یافت نشد."
                });
            }

            // 2. ایجاد کمپین جدید
            var campaign = new CampaignModel
            {
                Name = dto.Name,
                UserId = dto.UserId,
                StartDate = dto.StartDate,
                Platform = "Music"  // پلتفرم موزیک
            };

            Con.campaigns.Add(campaign);
            await Con.SaveChangesAsync();

            // 3. افزودن سایت‌های موزیک و تعرفه‌ها به کمپین
            foreach (var musicSiteTariff in dto.MusicSiteTariffs)
            {
                var musicSite = await Con.musicSiteModels.FindAsync(musicSiteTariff.MusicSiteId);
                if (musicSite == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = $"سایتی با شناسه {musicSiteTariff.MusicSiteId} یافت نشد."
                    });
                }

                var tariff = await Con.musicSiteTarefeModels
                    .FirstOrDefaultAsync(mst => mst.MusicSiteId == musicSiteTariff.MusicSiteId && mst.TariffId == musicSiteTariff.TariffId);
                if (tariff == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = $"تعرفه‌ای با شناسه {musicSiteTariff.TariffId} برای سایت با شناسه {musicSiteTariff.MusicSiteId} یافت نشد."
                    });
                }

                // ایجاد شیء `CampaignMusicSite` با مقداردهی فیلدهای مورد نیاز
                var campaignMusicSite = new CampaignMusicSite
                {
                    CampaignId = campaign.Id,
                    MusicSiteId = musicSiteTariff.MusicSiteId,
                    TariffId = musicSiteTariff.TariffId,
                    SiteName = musicSite.SiteName,
                    SiteTopic = musicSite.SiteTopic,
                    AdminId = musicSite.AdminId,
                    SiteAddress = musicSite.SiteAddress, // مقداردهی SiteAddress
                    SiteIcon = musicSite.SiteIcon,
                    Price = tariff.Price // مقدار تعرفه برای سایت
                };

                Con.campaignMusicSites.Add(campaignMusicSite);
            }

            // 4. ذخیره تغییرات
            await Con.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "کمپین با موفقیت و سایت‌های موزیک و تعرفه‌های انتخابی ایجاد شد."
            });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetMusicCampaigns()
        {
            try
            {
                var musicCampaigns = await Con.campaigns
                    .Where(c => c.Platform == "Music")
                    .Include(c => c.CampaignMusicSites)
                        .ThenInclude(cms => cms.MusicSite)
                    .Include(c => c.CampaignMusicSites)
                        .ThenInclude(cms => cms.Tariff)
                    .ToListAsync();

                if (musicCampaigns == null || !musicCampaigns.Any())
                {
                    return NotFound("کمپین‌های موزیک یافت نشد.");
                }

                return Ok(new
                {
                    StatusCode = 200,
                    Campaigns = musicCampaigns.Select(campaign => new
                    {
                        campaign.Id,
                        campaign.Name,
                        campaign.StartDate,
                        campaign.UserId,
                        campaign.Platform,
                        CampaignMusicSites = campaign.CampaignMusicSites.Select(cms => new
                        {
                            MusicSiteId = cms.MusicSite.Id,
                            MusicSiteName = cms.MusicSite.SiteName,
                            MusicSiteTopic = cms.MusicSite.SiteTopic,
                            MusicSiteAddress = cms.MusicSite.SiteAddress,
                            MusicSiteIcon = cms.MusicSite.SiteIcon,
                            TariffId = cms.TariffId,
                            TariffName = cms.Tariff?.Name,
                        }).ToList()
                    })
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error : {ex.Message}");
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetMusicCampaignsByUserId(long userId)
        {
            try
            {
                var userExist = await Con.users.AnyAsync(u => u.id == userId);
                if (!userExist)
                {
                    return NotFound("کاربری با این شناسه یافت نشد.");
                }

                var musicCampaigns = await Con.campaigns
                    .Where(c => c.UserId == userId && c.Platform == "Music")
                    .Include(c => c.CampaignMusicSites)
                        .ThenInclude(cms => cms.MusicSite)
                    .Include(c => c.CampaignMusicSites)
                        .ThenInclude(cms => cms.Tariff)
                    .ToListAsync();

                if (musicCampaigns == null || !musicCampaigns.Any())
                {
                    return NotFound("کمپین موزیک برای این کاربر یافت نشد.");
                }

                return Ok(new
                {
                    StatusCode = 200,
                    Campaigns = musicCampaigns.Select(campaign => new
                    {
                        campaign.Id,
                        campaign.Name,
                        campaign.StartDate,
                        campaign.UserId,
                        campaign.Platform,
                        CampaignMusicSites = campaign.CampaignMusicSites.Select(cms => new
                        {
                            MusicSiteId = cms.MusicSite.Id,
                            MusicSiteName = cms.MusicSite.SiteName,
                            MusicSiteTopic = cms.MusicSite.SiteTopic,
                            MusicSiteAddress = cms.MusicSite.SiteAddress,
                            MusicSiteIcon = cms.MusicSite.SiteIcon,
                            TariffId = cms.TariffId,
                            TariffName = cms.Tariff?.Name,
                        }).ToList()
                    })
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error : {ex.Message}");
            }
        }

        [HttpPost("create-campaign-with-telegram-channels-and-tariffs")]
        public async Task<IActionResult> CreateCampaignWithTelegramChannelsAndTariffs([FromBody] CreateCampaignWithChannelsAndTriffDto dto)
        {
            try
            {
                // 1. بررسی وجود کاربر
                var user = await Con.users.FindAsync(dto.UserId);
                if (user == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "کاربر مورد نظر یافت نشد."
                    });
                }

                // 2. ایجاد کمپین جدید
                var campaign = new CampaignModel
                {
                    Name = dto.Name,
                    UserId = dto.UserId,
                    StartDate = dto.StartDate,
                    Platform = "Telegram"
                };

                Con.campaigns.Add(campaign);
                await Con.SaveChangesAsync();

                // 3. بررسی و افزودن کانال‌ها و تعرفه‌ها به کمپین
                if (dto.ChannelTariffs == null || !dto.ChannelTariffs.Any())
                {
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        Message = "لیست کانال‌ها و تعرفه‌ها نمی‌تواند خالی باشد."
                    });
                }

                var channelIds = dto.ChannelTariffs.Select(ct => ct.ChannelId).Distinct().ToList();
                var tariffIds = dto.ChannelTariffs.Select(ct => ct.TariffId).Distinct().ToList();

                // لود کردن کانال‌ها و تعرفه‌ها
                var channels = await Con.telegramChannels
                    .Where(c => channelIds.Contains(c.Id))
                    .ToListAsync();

                var tariffs = await Con.telegramChannelTariffModels
                    .Where(t => tariffIds.Contains(t.TariffId) && channelIds.Contains(t.TelegramChannelId))
                    .ToListAsync();

                foreach (var channelTariff in dto.ChannelTariffs)
                {
                    var channel = channels.FirstOrDefault(c => c.Id == channelTariff.ChannelId);
                    if (channel == null)
                    {
                        return NotFound(new
                        {
                            StatusCode = 404,
                            Message = $"کانالی با شناسه {channelTariff.ChannelId} یافت نشد."
                        });
                    }

                    var tariff = tariffs.FirstOrDefault(t => t.TelegramChannelId == channelTariff.ChannelId && t.TariffId == channelTariff.TariffId);
                    if (tariff == null)
                    {
                        return NotFound(new
                        {
                            StatusCode = 404,
                            Message = $"تعرفه‌ای با شناسه {channelTariff.TariffId} برای کانال با شناسه {channelTariff.ChannelId} یافت نشد."
                        });
                    }

                    // ایجاد CampaignChannel با مقداردهی تمام فیلدها
                    var campaignChannel = new CampaignChannel
                    {
                        CampaignId = campaign.Id,
                        ChannelId = channelTariff.ChannelId,
                        TariffId = channelTariff.TariffId,
                        Name = !string.IsNullOrEmpty(channel.Name) ? channel.Name : "نام نامشخص",
                        Topic = !string.IsNullOrEmpty(channel.Topic) ? channel.Topic : "نامشخص",
                        ManagerId = !string.IsNullOrEmpty(channel.ManagerId) ? channel.ManagerId : "Unknown",
                        SubscribersCount = channel.SubscribersCount,
                        PhotoPath = !string.IsNullOrEmpty(channel.PhotoPath) ? channel.PhotoPath : "default.jpg",
                        TelID = !string.IsNullOrEmpty(channel.TelID) ? channel.TelID : "Unknown",
                        Price = tariff.Price
                    };
                    Con.campaignChannels.Add(campaignChannel);
                }

                // 4. ذخیره تغییرات
                await Con.SaveChangesAsync();

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "کمپین با موفقیت و کانال‌های تلگرام و تعرفه‌های انتخابی ایجاد شد."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = $"خطای داخلی سرور: {ex.Message}",
                    InnerException = ex.InnerException?.Message
                });
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetTelegramCampaigns()
        {
            try
            {
                var telegramCampaigns = await Con.campaigns
                    .Where(c => c.Platform == "Telegram")
                    .Include(c => c.CampaignChannels)
                        .ThenInclude(cc => cc.Channel)
                    .Include(c => c.CampaignChannels)
                        .ThenInclude(cc => cc.Tariff)
                    .ToListAsync();

                if (telegramCampaigns == null || !telegramCampaigns.Any())
                {
                    return NotFound("کمپین‌های تلگرام یافت نشد.");
                }

                return Ok(new
                {
                    StatusCode = 200,
                    Campaigns = telegramCampaigns.Select(campaign => new
                    {
                        campaign.Id,
                        campaign.Name,
                        campaign.StartDate,
                        campaign.UserId,
                        campaign.Platform,
                        CampaignChannels = campaign.CampaignChannels.Select(cc => new
                        {
                            ChannelId = cc.Channel.Id,
                            ChannelName = cc.Channel.Name,
                            TariffId = cc.TariffId,
                            TariffName = cc.Tariff?.Name,
                        }).ToList()
                    })
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error : {ex.Message}");
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetTelegramCampaignsByUserId(long userId)
        {
            try
            {
                // بررسی وجود کاربر
                var userExist = await Con.users.AnyAsync(u => u.id == userId);
                if (!userExist)
                {
                    return NotFound("کاربری با این شناسه یافت نشد.");
                }

                // دریافت کمپین‌های کاربر با اطلاعات کانال‌ها و تعرفه‌ها
                var telegramCampaigns = await Con.campaigns
                    .Where(c => c.UserId == userId && c.Platform == "Telegram")
                    .Include(c => c.CampaignChannels)
                        .ThenInclude(cc => cc.Channel) // شامل اطلاعات کانال
                    .Include(c => c.CampaignChannels)
                        .ThenInclude(cc => cc.Tariff) // شامل اطلاعات تعرفه
                    .ToListAsync();

                // بررسی وجود کمپین‌ها
                if (telegramCampaigns == null || !telegramCampaigns.Any())
                {
                    return NotFound("کمپین‌های تلگرام برای این کاربر یافت نشد.");
                }

                // ساخت خروجی
                return Ok(new
                {
                    StatusCode = 200,
                    Campaigns = telegramCampaigns.Select(campaign => new
                    {
                        campaign.Id,
                        campaign.Name,
                        campaign.StartDate,
                        campaign.UserId,
                        campaign.Platform,
                        CampaignChannels = campaign.CampaignChannels.Select(cc => new
                        {
                            ChannelId = cc.Channel.Id,
                            ChannelName = cc.Channel.Name,
                            TariffId = cc.TariffId, // شناسه تعرفه
                            TariffName = cc.Tariff?.Name, // نام تعرفه
                        }).ToList()
                    })
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error : {ex.Message}");
            }
        }

    }
}


