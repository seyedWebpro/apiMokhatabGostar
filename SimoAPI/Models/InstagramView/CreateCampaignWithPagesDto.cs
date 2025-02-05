using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models.InstagramView
{
   public class CreateCampaignWithPagesDto
{
    public string Name { get; set; }
    public long UserId { get; set; }
    public DateTime StartDate { get; set; }
    public List<int> SelectedPageIds { get; set; }
}
}