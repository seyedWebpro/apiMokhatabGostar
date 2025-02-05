using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models
{
    public class ScreenshotModel
    {
        public int Id { get; set; }
        public string ScreenshotUrl { get; set; }  // آدرس اسکرین‌شات
        public DateTime UploadDate { get; set; }

        // ارتباط با کمپین
        public int CampaignId { get; set; }
        public CampaignModel Campaign { get; set; }

        // ارتباط با کانال تلگرام یا سایت موزیک
        public int? ChannelId { get; set; }
        public TelegramChannelModel Channel { get; set; }

        public int? MusicSiteId { get; set; }
        public MusicSiteModel MusicSite { get; set; }

        public int? InstagramPageId { get; set; }
        public PagesModel InstagramPage { get; set; }

        // ارتباط با کاربر
        public long UserId { get; set; }  // آیدی کاربر
        public user User { get; set; }  // مدل کاربر
    }



}