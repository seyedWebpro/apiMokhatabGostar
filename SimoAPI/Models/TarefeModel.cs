using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models
{
    public class TarefeModel
    {
        public int Id { get; set; }
        public string Name { get; set; } // نام تعرفه
        public decimal? Price { get; set; } // قیمت که می‌تواند خالی باشد
        public ICollection<MusicSiteTarefeModel> MusicSiteTarefeModel { get; set; } // ارتباط با سایت‌ها
        public ICollection<TelegramChannelTariffModel> TelegramChannelTariffModels { get; set; }
        public ICollection<PageTariffModel> PageTariffModels    { get; set; }

    }
}