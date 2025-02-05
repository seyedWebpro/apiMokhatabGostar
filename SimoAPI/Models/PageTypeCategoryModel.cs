using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace allAPIs.SimoAPI.Models
{
    public class PageTypeCategoryModel
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; } = "";

        // رابطه یک به چند با PagesModel
         [JsonIgnore]
        public ICollection<PagesModel> Pages { get; set; } = new List<PagesModel>();
    }
}