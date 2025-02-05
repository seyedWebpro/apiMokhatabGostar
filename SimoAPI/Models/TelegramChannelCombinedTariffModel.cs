using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models
{
    public class TelegramChannelCombinedTariffModel
    {
        public int TelegramChannelId { get; set; }
        public TelegramChannelModel TelegramChannel { get; set; }

        public int CombinedTariffId { get; set; }
        public CombinedTariffModel CombinedTariff { get; set; }

        public decimal Price { get; set; } // قیمت تعیین‌شده برای تعرفه ترکیبی در کانال تلگرام
    }
}