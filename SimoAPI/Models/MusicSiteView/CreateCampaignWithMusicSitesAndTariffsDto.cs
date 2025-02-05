using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models.MusicSiteView
{
    public class CreateCampaignWithMusicSitesAndTariffsDto
{
    public string Name { get; set; }
    public long UserId { get; set; }
    public DateTime StartDate { get; set; }
    public List<MusicSiteTariffDto> MusicSiteTariffs { get; set; }
}
}