using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models
{
    public class AssignCombinedTariffToTelegramChannelDto
    {
        [Required(ErrorMessage = "شناسه کانال الزامی است.")]
        public int TelegramChannelId { get; set; }

        [Required(ErrorMessage = "شناسه تعرفه ترکیبی الزامی است.")]
        public int CombinedTariffId { get; set; }

        [Required(ErrorMessage = "قیمت الزامی است.")]
        public decimal Price { get; set; }
    }
}