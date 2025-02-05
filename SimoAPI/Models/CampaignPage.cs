using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models
{
    public class CampaignPage
    {
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public CampaignModel Campaign { get; set; }

        public int PageId { get; set; }
        public PagesModel Page { get; set; }

        public int TariffId { get; set; } // اضافه کردن TariffId
        public TarefeModel Tariff { get; set; } // اضافه کردن رابطه با TarefeModel

        public string? TelegramID { get; set; } = "";
        public string? WhatsappNumber { get; set; } = "";
        public string? sex { get; set; }
        public string? ShowName { get; set; }
        public string? PersianName { get; set; }
        public int? Followesrs { get; set; }
        public int? Following { get; set; }
        public string? ImgUrl { get; set; }
        public string? Description { get; set; }
        public int? PostViews { get; set; }
        public string? PostLink { get; set; } = "";
        public int? PostLikes { get; set; }
        public int? postComments { get; set; }
        public int? PostImpertion { get; set; }
        public int? StoryViews { get; set; }
        public int? StoryImpertion { get; set; }
        public double? Engagement { get; set; }
        public DateTime? PublishDate { get; set; } // تاریخ انتشار
        public decimal? Price { get; set; } // فیلد جدید برای قیمت

    }
}