using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models
{
    public class UploadScreenshotDto
    {
        public int CampaignId { get; set; }
        public long UserId { get; set; }
        public IFormFile Screenshot { get; set; }
        public int? InstagramId { get; set; }
        public int? ChannelId { get; set; }
        public int? MusicSiteId { get; set; }
    }

}