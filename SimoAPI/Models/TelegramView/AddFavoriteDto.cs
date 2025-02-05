using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models.TelegramView
{
    public class AddFavoriteDto
{
    public int UserId { get; set; } // شناسه کاربر
    public int TelegramChannelId { get; set; } // شناسه کانال
}

}