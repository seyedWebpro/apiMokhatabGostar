using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models.TelegramView
{
    public class AssignTariffTelegramChannelDto
    {
        public int TelegramChannelId { get; set; }
        public int TariffId { get; set; }
        public decimal Price { get; set; }
    }
}