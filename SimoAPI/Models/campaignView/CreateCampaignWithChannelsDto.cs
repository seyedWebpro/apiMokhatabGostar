using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models.campaignView
{
    public class CreateCampaignWithChannelsDto
    {
        public string Name { get; set; } // نام کمپین
        public long UserId { get; set; } // شناسه کاربر
        public DateTime StartDate { get; set; } // تاریخ شروع کمپین
        public List<int> SelectedChannelIds { get; set; } // لیست شناسه‌های کانال‌های انتخابی
    }

}