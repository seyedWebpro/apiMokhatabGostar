using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models.InstagramView
{
    public class UpdateInstagramPageDto
    {
    public string? PersianName { get; set; } // نام فارسی (اختیاری)
    public string? Description { get; set; } // توضیحات (اختیاری)
    public string? TelegramID { get; set; } // آیدی تلگرام (اختیاری)
    public string? WhatsappNumber { get; set; } // شماره واتس‌اپ (اختیاری)
    public string? Sex { get; set; } // جنسیت (اختیاری)
     public int? PageTypeId { get; set; } // اضافه شده
    public int? PageTypeCategoryId { get; set; } // اضافه شده
    }
}