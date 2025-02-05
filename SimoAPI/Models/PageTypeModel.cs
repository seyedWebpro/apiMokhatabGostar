using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace allAPIs.SimoAPI.Models
{
    public class PageTypeModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = "";

        // رابطه یک به چند با PagesModel
         [JsonIgnore]
        public ICollection<PagesModel> Pages { get; set; } = new List<PagesModel>();
    }
}