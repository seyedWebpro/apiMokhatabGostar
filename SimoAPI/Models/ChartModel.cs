using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models
{
    public class ChartModel
    {
        [Key]
        public int Id {get ; set;}
        public string? Name {get; set;} = "" ;
        public string? Vertical {get; set;} = "" ;
        public string? Horezntial {get; set;}= "" ;
        public int? CampaignId {get; set;}
    }
}