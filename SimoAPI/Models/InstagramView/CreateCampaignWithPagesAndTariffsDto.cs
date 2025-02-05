using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models.InstagramView
{
    public class CreateCampaignWithPagesAndTariffsDto
    {
        public string Name { get; set; }
        public long UserId { get; set; }
        public DateTime StartDate { get; set; }
        
        public List<PageTariffSelectionDto> PageTariffs { get; set; }
    }
}