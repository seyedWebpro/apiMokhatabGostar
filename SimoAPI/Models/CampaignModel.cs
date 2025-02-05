using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models
{
    public class CampaignModel
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; } = "";
        public DateTime? StartDate { get; set; }
        public long? UserId { get; set; }
        public string? PricePageId { get; set; } = "";

        public string? Platform { get; set; } = ""; // Insatgram or Telegram or MusicSite

        // Navigation Properties
        // لیست پیج‌های مرتبط با این کمپین
        [JsonIgnore]
        public virtual ICollection<CampaignPage> CampaignPages { get; set; } = new List<CampaignPage>();

        [JsonIgnore]
        public virtual ICollection<CampaignChannel> CampaignChannels { get; set; } = new List<CampaignChannel>(); // کانال‌های تلگرام

        [JsonIgnore]
        public virtual ICollection<CampaignMusicSite> CampaignMusicSites { get; set; } = new List<CampaignMusicSite>(); // سایت‌های موزیک

    }
}