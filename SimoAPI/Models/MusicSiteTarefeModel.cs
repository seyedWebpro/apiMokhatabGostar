using System;
using System.Collections.Generic;

namespace allAPIs.SimoAPI.Models
{
    public class MusicSiteTarefeModel
    {
        public int MusicSiteId { get; set; }
        public MusicSiteModel MusicSite { get; set; }

        public int TariffId { get; set; }
        public TarefeModel Tariff { get; set; }

        public decimal Price { get; set; } // قیمت تعیین‌شده برای این تعرفه در سایت خاص
    }
}
