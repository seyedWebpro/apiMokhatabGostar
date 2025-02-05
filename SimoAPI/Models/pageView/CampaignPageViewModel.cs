using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models.pageView
{
    public class CampaignPageViewModel
    {
        public CampaignPage CampaignPage { get; set; }
        public List<PageVersion> PageVersions { get; set; }
    }
}