using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models.TelegramView
{
    public class CreateCampaignWithChannelsAndTriffDto
    {
        public string Name { get; set; }
    public long UserId { get; set; }
    public DateTime StartDate { get; set; }
    public List<ChannelTariffDto> ChannelTariffs { get; set; } // لیست کانال‌ها با تعرفه‌های مربوطه
    }
}