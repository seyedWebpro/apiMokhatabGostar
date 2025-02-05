using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models
{
    public class MusicSiteModel
    {
        public int Id { get; set; }
        public string SiteName { get; set; }
        public string SiteTopic { get; set; }
        public string AdminId { get; set; }
        public string SiteAddress { get; set; }
        public string? SiteIcon { get; set; } // اختیاری
        public DateTime? PublishDate { get; set; } // تاریخ انتشار

        // افزودن ارتباط با MusicSiteTarefeModel
        public ICollection<MusicSiteTarefeModel> MusicSiteTarefeModel { get; set; }

        public ICollection<FavoriteMusicSiteModel> FavoriteMusicSites { get; set; }

        // ارتباط با CampaignMusicSite
        public ICollection<CampaignMusicSite> CampaignMusicSites { get; set; } = new List<CampaignMusicSite>();

    }
}