using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models.MusicSiteView
{
    public class AssignTariffMusicSiteDto
    {
        public int MusicSiteId { get; set; } // آیدی سایت موزیک
        public int TariffId { get; set; } // آیدی تعرفه
        public decimal Price { get; set; } // قیمت تعیین‌شده برای تعرفه
    }
}