using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models
{
public class FavoriteMusicSiteModel
{
    public int Id { get; set; }
    public long UserId { get; set; } // تغییر به long
    public int MusicSiteId { get; set; }

    public user User { get; set; }
    public MusicSiteModel MusicSite { get; set; }
}

}