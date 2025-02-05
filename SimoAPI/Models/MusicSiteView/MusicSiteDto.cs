using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models.MusicSiteView
{
    public class MusicSiteDto
    {
        [Required(ErrorMessage = "نام سایت اجباری است.")]
    public string SiteName { get; set; }

    [Required(ErrorMessage = "موضوع سایت اجباری است.")]
    public string SiteTopic { get; set; }

    [Required(ErrorMessage = "آیدی مدیر سایت اجباری است.")]
    public string AdminId { get; set; }

    [Required(ErrorMessage = "آدرس سایت اجباری است.")]
    [Url(ErrorMessage = "آدرس سایت معتبر نیست.")]
    public string SiteAddress { get; set; }

    public IFormFile? SiteIcon { get; set; } // آیکون سایت (اختیاری)

    }
}