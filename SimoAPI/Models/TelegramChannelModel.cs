using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models
{
    public class TelegramChannelModel
    {
        public int Id { get; set; } // شناسه کانال
        public string Name { get; set; } // نام کانال
        public string Topic { get; set; } // موضوع کانال
        public string TelID { get; set; } // آیدی کانال
        public string? ManagerId { get; set; } // آیدی مدیر کانال        
        public int SubscribersCount { get; set; } // تعداد ساب‌اسکرایب‌ها
        public DateTime? PublishDate { get; set; } // تاریخ انتشار

        // مسیر عکس کانال
        public string PhotoPath { get; set; }

        public ICollection<TelegramChannelTariffModel> TelegramChannelTariffModels { get; set; }
        public ICollection<TelegramChannelCombinedTariffModel> TelegramChannelCombinedTariffModels { get; set; }

        public ICollection<FavoriteTelegramChannelModel> FavoriteTelegramChannels { get; set; }

         // لیست کمپین‌های مرتبط با این کانال
        public ICollection<CampaignChannel> CampaignChannels { get; set; }

    }
}
