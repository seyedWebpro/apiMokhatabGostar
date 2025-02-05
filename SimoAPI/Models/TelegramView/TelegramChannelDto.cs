using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models.TelegramView
{
    public class TelegramChannelDto
    {
        [Required(ErrorMessage = "نام کانال الزامی است.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "موضوع کانال الزامی است.")]
        public string Topic { get; set; }

        [Required(ErrorMessage = "آیدی کانال الزامی است.")]
        public string ChannelId { get; set; }

        [Required(ErrorMessage = "آیدی مدیر کانال الزامی است.")]
        public string ManagerId { get; set; }

        [Required(ErrorMessage = "تعداد اعضای کانال الزامی است.")]
        public int SubscribersCount { get; set; } // تعداد ساب‌اسکرایب‌ها

    }
}