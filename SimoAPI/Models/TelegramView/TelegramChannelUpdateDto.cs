using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Controller.Telegram
{
    public class TelegramChannelUpdateDto
    {
        public string? Name { get; set; } // نام کانال (اختیاری)
        public string? Topic { get; set; } // موضوع کانال (اختیاری)
        public string? ChannelId { get; set; } // آیدی کانال (اختیاری)
        public string? ManagerId { get; set; } // آیدی مدیر کانال (اختیاری)
        public int? SubscribersCount { get; set; } // تعداد ساب‌اسکرایب‌ها

    }
}