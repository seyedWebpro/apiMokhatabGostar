using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models
{
    public class PageVersion
    {
        [Key]
    public int Id { get; set; }

    [Required]
    public int PageId { get; set; }
    public virtual PagesModel Page { get; set; }

    public string? sex {get; set;}

    public string? ShowName { get; set; }
    public string? PersianName { get; set; }

    public int? CampaignId { get; set; }
    public virtual CampaignModel Campaign { get; set; }

    public string? Description { get; set; }

    public int? PostLikes { get; set; }

    public string? PostLink { get; set; }

    public int? postComments { get; set; }

    public int? PostViews { get; set; }

    public int? PostImpertion { get; set; }

    public int? StoryViews { get; set; }

    public int? StoryImpertion { get; set; }

    public DateTime CreatedDateTime { get; set; }
    
    public bool? IsAccepted { get; set; } = false; // وضعیت پذیرش پیج
    }
}   