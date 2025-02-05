using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models
{
    public class FavoriteTelegramChannelModel
    {
        public int Id { get; set; }
        public long UserId { get; set; } // نوع داده مطابق با user.id
        public int TelegramChannelId { get; set; }
        public user User { get; set; }
        public TelegramChannelModel TelegramChannel { get; set; }
    }

}