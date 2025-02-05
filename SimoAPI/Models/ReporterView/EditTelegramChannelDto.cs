using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models.ReporterView
{
   public class EditTelegramChannelDto
    {
        public string? Name { get; set; }
        public string? Topic { get; set; }
        public string? ChannelId { get; set; }
        public string? ManagerId { get; set; }
        public int? SubscribersCount { get; set; }
        public string? PhotoPath { get; set; }
    }
}