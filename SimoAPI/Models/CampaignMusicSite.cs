using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models
{
    public class CampaignMusicSite
    {
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public CampaignModel Campaign { get; set; }

        public int MusicSiteId { get; set; }
        public MusicSiteModel MusicSite { get; set; }

        public int TariffId { get; set; } // اضافه کردن TariffId
        public TarefeModel Tariff { get; set; } // اضافه کردن رابطه با TarefeModel

        // اطلاعات مخصوص این کمپین (کپی از MusicSite)
        public string SiteName { get; set; }
        public string SiteTopic { get; set; }
        public string? AdminId { get; set; }
        public string SiteAddress { get; set; }
        public string SiteIcon { get; set; }
        public decimal? Price { get; set; } 
        public DateTime? PublishDate { get; set; } // تاریخ انتشار


    }

}