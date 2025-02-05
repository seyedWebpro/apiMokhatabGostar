using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models.MusicSiteView
{
    public class UpdateMusicSiteDto
    {
        public string? SiteName { get; set; } // نام سایت (اختیاری)
        public string? SiteTopic { get; set; } // موضوع سایت (اختیاری)
        public string? AdminId { get; set; } // آیدی مدیر سایت (اختیاری)
        public string? SiteAddress { get; set; } // آدرس سایت (اختیاری)
    }

}