using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models.ReporterView
{
    public class UpdateCampaignMusicSiteDto
{
    public string? SiteName { get; set; }
    public string? SiteTopic { get; set; }
    public string? AdminId { get; set; }
    public string? SiteAddress { get; set; }
    public string? SiteIcon { get; set; }
    public decimal? Price { get; set; } // فیلد جدید برای قیمت

    public int? TariffId { get; set; }
    public DateTime? PublishDate { get; set; }

}



}