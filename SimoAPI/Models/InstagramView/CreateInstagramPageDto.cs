using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models.InstagramView
{
    public class CreateInstagramPageDto
    {
        [Required]
    public string PageId { get; set; } // Username or ID of the Instagram page

    public string? TelegramID { get; set; }
    public string? WhatsappNumber { get; set; }
    public string? Sex { get; set; }
    public string? PersianName { get; set; }
    public string? Description { get; set; }
     public string PageTypeCategoryName { get; set; } 
    public string PageTypeName { get; set; } 
    }
}