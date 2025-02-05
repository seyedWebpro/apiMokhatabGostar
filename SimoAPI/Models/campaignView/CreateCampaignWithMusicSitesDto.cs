using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models.campaignView
{
    public class CreateCampaignWithMusicSitesDto
    {
        public long UserId { get; set; }  // شناسه کاربر که کمپین را ایجاد می‌کند
        public string Name { get; set; }  // نام کمپین
        public DateTime StartDate { get; set; }  // تاریخ شروع کمپین
        public List<int> SelectedMusicSiteIds { get; set; }  // لیست شناسه‌های سایت‌های موزیک انتخابی برای کمپین
    }

}