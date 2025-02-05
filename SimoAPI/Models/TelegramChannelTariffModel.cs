using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models
{
    public class TelegramChannelTariffModel
    {
        public int TelegramChannelId { get; set; }
        public TelegramChannelModel TelegramChannel { get; set; }

        public int TariffId { get; set; }
        public TarefeModel Tariff { get; set; }
    
        public decimal Price { get; set; } // قیمت تعیین‌شده برای تعرفه در کانال تلگرام
    }

}