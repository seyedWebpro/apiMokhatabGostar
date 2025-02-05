using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models.ReporterView
{
    public class EditPageDto
    {
        public string? TelegramID { get; set; }
        public string? WhatsappNumber { get; set; }
        public string? Sex { get; set; }
        public string? ShowName { get; set; }
        public string? PersianName { get; set; }
        public int? Followesrs { get; set; }
        public int? Following { get; set; }
        public string? ImgUrl { get; set; }
        public string? Description { get; set; }
        public int? PostViews { get; set; }
        public string? PostLink { get; set; }
        public int? PostLikes { get; set; }
        public int? PostComments { get; set; }
        public int? PostImpertion { get; set; }
        public int? StoryViews { get; set; }
        public int? StoryImpertion { get; set; }
        public double? Engagement { get; set; }
        public int? PageTypeCategoryId { get; set; }
        public int? PageTypeId { get; set; }
    }
}