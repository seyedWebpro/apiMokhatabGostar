using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models.pageView
{
    public class PageDataView
    {
    public int? PageId {get;  set;}

    public string? sex {get; set;}
    public string? Description {get; set;}
    public int? PostLikes { get; set; }
    public string? PostLink {get ; set;}
    public int? postComments { get; set; }
    public int? PostViews { get; set; }
    public int? PostImpertion { get; set; }
    public int? StoryViews { get; set; }
    public int? StoryImpertion { get; set; }
    public string? PersianName { get; set; }
    }
}