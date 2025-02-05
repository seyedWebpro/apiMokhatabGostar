using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models
{
    public class CampaignChannel
    {
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public CampaignModel Campaign { get; set; }

        public int ChannelId { get; set; }
        public TelegramChannelModel Channel { get; set; }

        public string TelID { get; set; } // آیدی کانال
        


        public int TariffId { get; set; } // اضافه کردن TariffId
        public TarefeModel Tariff { get; set; } // اضافه کردن رابطه با TarefeModel
        public string Name { get; set; } // نام کانال
        public string Topic { get; set; } // موضوع کانال
        public string? ManagerId { get; set; } // آیدی مدیر کانال        
        public int SubscribersCount { get; set; } // تعداد ساب‌اسکرایب‌ها
        public DateTime? PublishDate { get; set; } // تاریخ انتشار

        // مسیر عکس کانال
        public string PhotoPath { get; set; }
        public decimal? Price { get; set; } // فیلد جدید برای قیمت

    }
}