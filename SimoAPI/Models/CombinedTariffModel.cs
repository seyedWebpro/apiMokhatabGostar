using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models
{
    public class CombinedTariffModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "عنوان تعرفه ترکیبی الزامی است.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "اسم تعرفه‌ها الزامی است.")]
        public string TariffNames { get; set; } // لیست اسم تعرفه‌ها (مثلاً به صورت رشته‌ای جدا شده با کاما)

        [Required(ErrorMessage = "قیمت تعرفه الزامی است.")]
        public decimal? Price { get; set; }

        // ارتباط با TelegramChannelCombinedTariffModel
        public ICollection<TelegramChannelCombinedTariffModel> TelegramChannelCombinedTariffModels { get; set; }
    }
}