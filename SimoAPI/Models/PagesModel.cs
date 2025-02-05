using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace allAPIs.SimoAPI.Models
{
    public class PagesModel
    {
        [Key]
        public int Id { get; set; }
        public string PageId { get; set; } = "";
        public string? TelegramID { get; set; } = "";
        public string? WhatsappNumber { get; set; } = "";
        public string? sex { get; set; }
        public string? ShowName { get; set; }
        public string? PersianName { get; set; }
        public int? Followesrs { get; set; }
        public int? Following { get; set; }
        public string? ImgUrl { get; set; }
        public string? Description { get; set; }
        public int? PostViews { get; set; }
        public string? PostLink { get; set; } = "";
        public int? PostLikes { get; set; }
        public int? postComments { get; set; }
        public int? PostImpertion { get; set; }
        public int? StoryViews { get; set; }
        public int? StoryImpertion { get; set; }
        public double? Engagement { get; set; }
        public DateTime? PublishDate { get; set; } // تاریخ انتشار

        // رابطه با PageTypeCategoryModel
        public int? PageTypeCategoryId { get; set; }
        public PageTypeCategoryModel? PageTypeCategory { get; set; }

        // رابطه با PageTypeModel
        public int? PageTypeId { get; set; }
        
        public PageTypeModel? PageType { get; set; }

        // سایر روابط
        public ICollection<PageTariffModel> PageTariffModels { get; set; }
        public ICollection<FavoritePagesModel> FavoritePagesModels { get; set; }
        public ICollection<CampaignPage> CampaignPages { get; set; }
        public virtual ICollection<PageTag> PageTags { get; set; } = new List<PageTag>();
    }
}