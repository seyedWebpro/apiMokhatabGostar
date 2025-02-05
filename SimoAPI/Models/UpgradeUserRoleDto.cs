using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models
{
public class UpgradeUserRoleDto
{
    [Required(ErrorMessage = "شناسه کاربری الزامی است.")]
    [Display(Name = "شناسه کاربری")]
    public long UserId { get; set; }  // تغییر از int به long

    [Required(ErrorMessage = "نقش جدید الزامی است.")]
    [Display(Name = "نقش جدید")]
    public string NewRole { get; set; } = string.Empty;
}

}