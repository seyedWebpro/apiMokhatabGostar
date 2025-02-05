using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models.InstagramView
{
    public class UpdateCampaignInstagramPageDto
    {
         public string? ShowName { get; set; }
        public string? PersianName { get; set; }
        public string? ImgUrl { get; set; }
        public string? Description { get; set; }

        // فیلدهای جدید برای به‌روزرسانی در کنترلر
        public int? PostComments { get; set; }
        public int? PostViews { get; set; }
        public int? PostLikes { get; set; }
        public string? PostLink { get; set; }
        public DateTime? PublishDate { get; set; } // تاریخ انتشار
        public decimal? Price { get; set; }

        public int? TariffId { get; set; }

        // فیلدهای اضافی که به روزرسانی نمی‌شوند حذف شدند
        // public string? PageId { get; set; }
        // public string? TelegramID { get; set; }
        // public string? WhatsappNumber { get; set; }
        // public string? Sex { get; set; }
        // public int? Followers { get; set; }
        // public int? Following { get; set; }
        // public int? PostImpression { get; set; }
        // public int? StoryViews { get; set; }
        // public int? StoryImpression { get; set; }
        // public double? Engagement { get; set; }
        // public int? PageTypeCategoryId { get; set; }
        // public int? PageTypeId { get; set; }

    }

}