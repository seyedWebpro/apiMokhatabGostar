using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models.TelegramView
{
    public class UpdateCampaignTelegramChannelDto
    {
        public string? Name { get; set; }
        public string? Topic { get; set; }
        public string? TelID { get; set; }
        public string? ManagerId { get; set; }
        public string? PhotoPath { get; set; }
        public int? SubscribersCount { get; set; }
        public DateTime? PublishDate { get; set; } // تاریخ انتشار
        public decimal? Price { get; set; }
        public int? TariffId { get; set; }

    }
}